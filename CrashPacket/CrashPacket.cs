using System;
using System.IO;


/**
 * @brief 직렬화된 크래시 패킷입니다.
 */
class CrashPacket : Packet
{
    /**
     * @brief 패킷의 종류입니다.
     */
    public enum EType : uint
    {
        REQ_FILE_SEND = 0x01,
        REP_FILE_SEND = 0x02,
        FILE_SEND_DATA = 0x03,
        FILE_SEND_RES = 0x04,
    }


    /**
     * @brief 패킷의 파편화 여부입니다.
     */
    public enum EFragment : byte
    {
        NO = 0x00,
        YES = 0x01,
    }


    /**
     * @brief 패킷이 마지막 패킷인지 여부입니다.
     */
    public enum ELast : byte
    {
        NO = 0x00,
        YES = 0x01,
    }


    /**
     * @brief 응답 요청 여부입니다.
     */
    public enum ERespone : byte
    {
        ACCEPTED = 0x00,
        DENIED = 0x01,
    }


    /**
     * @brief 패킷의 성공 여부입니다.
     */
    public enum ESuccess : byte
    {
        FAIL = 0x00,
        SUCCESS = 0x01
    }


    /**
     * @brief 크래시 패킷을 전송합니다.
     * 
     * @param writer 패킷을 전송할 스트림입니다.
     * @param packet 전송할 크래시 패킷입니다.
     */
    public static void Send(Stream writer, CrashPacket packet)
    {
        writer.Write(packet.GetByteBuffer(), 0, packet.GetByteBufferSize());
    }


    /**
     * @brief 크래시 패킷을 받습니다.
     * 
     * @param reader 패킷을 받을 스트림입니다.
     * 
     * @return 받은 크래시 패킷 객체를 반환합니다.
     */
    public static CrashPacket Receive(Stream reader)
    {
        int totalRecv = 0;
        int readSize = CrashPacketHeader.PACKET_HEADER_SIZE;
        byte[] headerBuffer = new byte[readSize];

        while (readSize > 0)
        {
            byte[] buffer = new byte[readSize];
            int recv = reader.Read(buffer, 0, readSize);

            if (recv == 0)
            {
                return null;
            }

            buffer.CopyTo(headerBuffer, totalRecv);
            totalRecv += recv;
            readSize -= recv;
        }

        CrashPacketHeader packetHeader = new CrashPacketHeader(headerBuffer);

        totalRecv = 0;
        byte[] bodyBuffer = new byte[packetHeader.BodySize];
        readSize = (int)(packetHeader.BodySize);

        while (readSize > 0)
        {
            byte[] buffer = new byte[readSize];
            int recv = reader.Read(buffer, 0, readSize);

            if (recv == 0)
            {
                return null;
            }

            buffer.CopyTo(bodyBuffer, totalRecv);
            totalRecv += recv;
            readSize -= recv;
        }

        ISerialize packetBody = null;
        CrashPacket.EType packetType = (CrashPacket.EType)(packetHeader.PacketType);

        switch (packetType)
        {
            case CrashPacket.EType.REQ_FILE_SEND:
                packetBody = new CrashPacketRequestBody(bodyBuffer);
                break;
            case CrashPacket.EType.REP_FILE_SEND:
                packetBody = new CrashPacketResponseBody(bodyBuffer);
                break;
            case CrashPacket.EType.FILE_SEND_DATA:
                packetBody = new CrashPacketDataBody(bodyBuffer);
                break;
            case CrashPacket.EType.FILE_SEND_RES:
                packetBody = new CrashPacketResultBody(bodyBuffer);
                break;
        }

        return new CrashPacket() { Header = packetHeader, Body = packetBody };
    }
}

