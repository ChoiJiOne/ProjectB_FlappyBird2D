using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


/**
 * @brief 크래시 덤프 파일과 로그 파일을 전송하는 애플리케이션을 실행합니다.
 */
class CrashReportSender
{
    /**
     * @brief 크래시 덤프 파일과 로그 파일을 전송하는 애플리케이션을 실행합니다.
     * 
     * @param args 명령행 인수입니다.
     */
    static void Main(string[] args)
    {
        Console.WriteLine("run crash report sender...");
        SetProperties();

        client_.Connect(serverAddress_);
        networkStream_ = client_.GetStream();

        { // 파일 전송 요청
            CrashPacket requestPacket = new CrashPacket();
            requestPacket.Body = new CrashPacketRequestBody()
            {
                FileSize = new FileInfo(sendFilePath_).Length,
                FileName = System.Text.Encoding.Default.GetBytes(sendFilePath_)
            };
            requestPacket.Header = new CrashPacketHeader()
            {
                PacketID = crashPacketID_++,
                PacketType = (uint)(CrashPacket.EType.REQ_FILE_SEND),
                BodySize = (uint)(requestPacket.Body.GetByteBufferSize()),
                Fragmented = (byte)(CrashPacket.EFragment.NO),
                LastPacket = (byte)(CrashPacket.ELast.YES),
                Seq = 0
            };
            CrashPacket.Send(networkStream_, requestPacket);

            CrashPacket responePacket = CrashPacket.Receive(networkStream_);
            CrashPacketHeader packetHeader = (CrashPacketHeader)(responePacket.Header);

            if(packetHeader.PacketType != (uint)(CrashPacket.EType.REP_FILE_SEND))
            {
                Console.WriteLine("invalid response from crash collector...");
                networkStream_.Close();
                client_.Close();
                return;
            }
        }
        
        { // 파일 전송
            Stream fileStream = new FileStream(sendFilePath_, FileMode.Open);
            byte[] readbytes = new byte[FILE_CHUNK_SIZE];
            long readValue = BitConverter.ToInt64(readbytes, 0);
            int totalRead = 0;
            ushort packetSeq = 0;
            byte fragmented = (byte)((fileStream.Length < FILE_CHUNK_SIZE) ? CrashPacket.EFragment.NO : CrashPacket.EFragment.YES);

            while (totalRead < fileStream.Length)
            {
                int read = fileStream.Read(readbytes, 0, FILE_CHUNK_SIZE);
                totalRead += read;
                CrashPacket filePacket = new CrashPacket();

                byte[] sendBytes = new byte[read];
                Array.Copy(readbytes, 0, sendBytes, 0, read);

                filePacket.Body = new CrashPacketDataBody(sendBytes);
                filePacket.Header = new CrashPacketHeader()
                {
                    PacketID = crashPacketID_,
                    PacketType = (uint)(CrashPacket.EType.FILE_SEND_DATA),
                    BodySize = (uint)(filePacket.Body.GetByteBufferSize()),
                    Fragmented = fragmented,
                    LastPacket = (byte)((totalRead < fileStream.Length) ? CrashPacket.ELast.NO : CrashPacket.ELast.YES),
                    Seq = packetSeq++
                };

                CrashPacket.Send(networkStream_, filePacket);
            }

            CrashPacket resultPacket = CrashPacket.Receive(networkStream_);
            CrashPacketResultBody resultPacketBody = (CrashPacketResultBody)(resultPacket.Body);

            if(resultPacketBody.Result == (byte)(CrashPacket.ESuccess.SUCCESS))
            {
                Console.WriteLine("successed sending crash report file...");
            }
            else
            {
                Console.WriteLine("failed sending crash report file...");
            }
        }
        
        networkStream_.Close();
        client_.Close();
    }


    /**
     * @brief 멤버 변수를 설정합니다.
     */
    private static void SetProperties()
    {
        serverIP_ = "127.0.0.1";
        serverPort_ = 5425;
        sendFilePath_ = "D:\\Work\\FlappyBird2D\\Crash\\2023-06-13-07-13-54.zip";

        clientAddress_ = new IPEndPoint(0, 0);
        serverAddress_ = new IPEndPoint(IPAddress.Parse(serverIP_), serverPort_);

        client_ = new TcpClient(clientAddress_);

        crashPacketID_ = 0;

        Console.WriteLine("IP = {0}", serverIP_);
        Console.WriteLine("PORT = {0}", serverPort_);
        Console.WriteLine("FILE = {0}", sendFilePath_);
    }


    /**
     * @brief 파일 청크 크기입니다.
     */
    private static readonly int FILE_CHUNK_SIZE = 4096;


    /**
     * @brief 크래시 수집 서버의 IP입니다.
     */
    private static string serverIP_;


    /**
     * @brief 크래시 수집 서버의 PORT입니다.
     */
    private static int serverPort_;


    /**
     * @brief 전송할 파일의 경로입니다.
     */
    private static string sendFilePath_;


    /**
     * @brief 크래시 패킷의 ID입니다.
     */
    private static uint crashPacketID_;


    /**
     * @brief 클라이언트 주소입니다.
     */
    private static IPEndPoint clientAddress_;


    /**
     * @brief 서버 주소입니다.
     */
    private static IPEndPoint serverAddress_;


    /**
     * @brief 클라이언트 네트워크 스트림입니다.
     */
    private static NetworkStream networkStream_;


    /**
     * @brief TCP 클라이언트 객체입니다.
     */
    private static TcpClient client_;
}