/**
 * @brief 직렬화된 패킷입니다.
 */
public class Packet : ISerialize
{
    /**
     * @brief 직렬화된 패킷의 Getter/Setter입니다.
     */
    public ISerialize Header
    {
        get => header_;
        set => header_ = value;
    }

    public ISerialize Body
    {
        get => body_;
        set => body_ = value;
    }


    /**
     * @brief 직렬화된 패킷 객체의 바이트 버퍼를 얻습니다.
     */
    public byte[] GetByteBuffer()
    {
        byte[] byteBuffer = new byte[GetByteBufferSize()];

        header_.GetByteBuffer().CopyTo(byteBuffer, 0);
        body_.GetByteBuffer().CopyTo(byteBuffer, header_.GetByteBufferSize());

        return byteBuffer;
    }


    /**
     * @brief 직렬화된 패킷 객체의 바이트 버퍼 크기를 얻습니다.
     */
    public int GetByteBufferSize()
    {
        return header_.GetByteBufferSize() + body_.GetByteBufferSize();
    }


    /**
     * @brief 패킷의 해더입니다.
     */
    protected ISerialize header_;


    /**
     * @brief 패킷의 바디입니다.
     */
    protected ISerialize body_;
}