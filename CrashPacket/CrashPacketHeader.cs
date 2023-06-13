using System;


/**
 * @brief 크래시 패킷의 헤더입니다.
 */
public class CrashPacketHeader : ISerialize
{
    /**
     * @brief 크래시 패킷 해더에 대한 Getter/Setter입니다.
     */
    public uint PacketID
    {
        get => packetID_;
        set => packetID_ = value;
    }

    public uint PacketType
    {
        get => packetType_;
        set => packetType_ = value;
    }

    public uint BodySize
    {
        get => bodySize_;
        set => bodySize_ = value;
    }

    public byte Fragmented
    {
        get => fragmented_;
        set => fragmented_ = value;
    }

    public byte LastPacket
    {
        get => lastPacket_;
        set => lastPacket_ = value;
    }

    public ushort Seq
    {
        get => seq_;
        set => seq_ = value;
    }


    /**
     * @brief 크래시 패킷 헤더의 빈 생성자입니다.
     */
    public CrashPacketHeader() { }


    /**
     * @brief 크래시 패킷 헤더의 생성자입니다.
     * 
     * @param headerBytes 크래시 패킷 헤더의 바이트 형식입니다.
     */
    public CrashPacketHeader(byte[] headerBytes)
    {
        packetID_ = BitConverter.ToUInt32(headerBytes, 0);
        packetType_ = BitConverter.ToUInt32(headerBytes, 4);
        bodySize_ = BitConverter.ToUInt32(headerBytes, 8);
        fragmented_ = headerBytes[12];
        lastPacket_ = headerBytes[13];
        seq_ = BitConverter.ToUInt16(headerBytes, 14);
    }


    /**
     * @brief 크래시 패킷 헤더의 바이트 버퍼를 얻습니다.
     * 
     * @return 크래시 패킷 헤더의 바이트 버퍼를 반환합니다.
     */
    public byte[] GetByteBuffer()
    {
        byte[] byteBuffer = new byte[PACKET_SIZE];

        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, byteBuffer, 0, buffer.Length);

        buffer = BitConverter.GetBytes(packetType_);
        Array.Copy(buffer, 0, byteBuffer, 4, buffer.Length);

        buffer = BitConverter.GetBytes(bodySize_);
        Array.Copy(buffer, 0, byteBuffer, 8, buffer.Length);

        byteBuffer[12] = fragmented_;
        byteBuffer[13] = lastPacket_;

        buffer = BitConverter.GetBytes(seq_);
        Array.Copy(buffer, 0, byteBuffer, 14, buffer.Length);

        return byteBuffer;
    }


    /**
     * @brief 크래시 패킷 헤더의 바이트 버퍼 크기를 얻습니다.
     * 
     * @return 크래시 패킷 해더의 바이트 버퍼 크기를 반환합니다.
     */
    public int GetByteBufferSize()
    {
        return PACKET_HEADER_SIZE;
    }


    /**
     * @brief 크래시 패킷의 크기입니다.
     */
    public static readonly int PACKET_HEADER_SIZE = 16;


    /**
     * @brief 패킷을 식별할 ID입니다.
     */
    private uint packetID_;


    /**
     * @brief 패킷의 종류입니다.
     */
    private uint packetType_;


    /**
     * @brief 패킷 바디의 크기입니다.
     */
    private uint bodySize_;


    /**
     * @brief 패킷 분할 여부입니다.
     */
    private byte fragmented_;


    /**
     * @brief 분할된 패킷이 마지막 패킷인지 확인합니다.
     */
    private byte lastPacket_;


    /**
     * @brief 패킷의 파편 번호입니다.
     */
    private ushort seq_;
}