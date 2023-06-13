/**
 * @brief 직렬화된 객체의 인터페이스입니다.
 */
public interface ISerialize
{
    /**
     * @brief 직렬화된 객체의 바이트 버퍼를 얻습니다.
     */
    public byte[] GetByteBuffer();


    /**
     * @brief 직렬화된 객체의 바이트 버퍼 크기를 얻습니다.
     */
    public int GetByteBufferSize();
}