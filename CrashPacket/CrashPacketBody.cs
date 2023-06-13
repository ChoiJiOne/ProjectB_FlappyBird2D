using System;


/**
 * @brief 파일 전송을 요청했을 때의 크래시 패킷 바디입니다.
 */
class CrashPacketRequestBody : ISerialize
{
    /**
     * @brief 파일 전송을 요청했을 때의 크래시 패킷 바디 빈 생성자입니다.
     */
    public CrashPacketRequestBody() { }


    /**
     * @brief 파일 전송을 요청했을 때의 크래시 패킷 바디 생성자입니다.
     * 
     * @param bodyBytes 크래시 패킷 바디의 바이트 형식입니다.
     */
    public CrashPacketRequestBody(byte[] bodyBytes)
    {
        fileSize_ = BitConverter.ToInt64(bodyBytes, 0);
        fileName_ = new byte[bodyBytes.Length - sizeof(long)];
        Array.Copy(bodyBytes, sizeof(long), fileName_, 0, fileName_.Length);
    }


    /**
     * @brief 파일 전송을 요청했을 때의 크래시 패킷 바디 바이트 버퍼를 얻습니다.
     * 
     * @return 파일 전송을 요청했을 때의 크래시 패킷 바디 바이트 버퍼를 반환합니다.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(fileSize_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        Array.Copy(fileName_, 0, bytes, buffer.Length, fileName_.Length);

        return bytes;
    }


    /**
     * @brief 파일 전송을 요청했을 때의 크래시 패킷 바디 바이트 버퍼의 크기를 얻습니다.
     * 
     * @return 파일 전송을 요청했을 때의 크래시 패킷 바디 바이트 버퍼의 크기를 반환합니다.
     */
    public int GetByteBufferSize()
    {
        return sizeof(long) + fileName_.Length;
    }


    /**
     * @brief 파일의 크기입니다.
     */
    private long fileSize_;


    /**
     * @brief 파일의 이름입니다.
     */
    private byte[] fileName_;
}


/**
 * @brief 응답했을 때의 크래시 패킷 바디입니다.
 */
class CrashPacketResponseBody : ISerialize
{
    /**
     * @brief 응답했을 때의 크래시 패킷 바디의 빈 생성자입니다.
     */
    public CrashPacketResponseBody() { }


    /**
     * @brief 응답했을 때의 크래시 패킷 바디의 생성자입니다.
     * 
     * @param bodyBytes 크래시 패킷 바디의 바이트 형식입니다.
     */
    public CrashPacketResponseBody(byte[] bodyBytes)
    {
        packetID_ = BitConverter.ToUInt32(bodyBytes, 0);
        response_ = bodyBytes[4];
    }


    /**
     * @brief 응답 했을 때의 크래시 패킷 바디 바이트 버퍼를 얻습니다.
     * 
     * @return 응답 을 요청했을 때의 크래시 패킷 바디 바이트 버퍼를 반환합니다.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        bytes[buffer.Length] = response_;

        return bytes;
    }


    /**
     * @brief 응답 했을 때의 크래시 패킷 바디 바이트 버퍼의 크기를 얻습니다.
     * 
     * @return 응답 했을 때의 크래시 패킷 바디 바이트 버퍼의 크기를 반환합니다.
     */
    public int GetByteBufferSize()
    {
        return sizeof(uint) + sizeof(byte);
    }


    /**
     * @brief 패킷 아이디입니다.
     */
    public uint packetID_;


    /**
     * @brief 패킷 응답 여부입니다.
     */
    public byte response_;
}


/**
 * @brief 데이터가 포함된 크래시 패킷 바디입니다.
 */
class CrashPacketDataBody : ISerialize
{
    /**
     * @brief 데이터가 포함된 크래시 패킷 바디의 빈 생성자입니다.
     */
    public CrashPacketDataBody() { }


    /**
     * @brief 데이터가 포함된 크래시 패킷 바디의 생성자입니다.
     * 
     * @param bodyBytes 크래시 패킷 바디의 바이트 형식입니다.
     */
    public CrashPacketDataBody(byte[] bodyBytes)
    {
        data_ = new byte[bodyBytes.Length];
        bodyBytes.CopyTo(data_, 0);
    }


    /**
     * @brief 데이터가 포함된 크래시 패킷 바디 바이트 버퍼를 얻습니다.
     * 
     * @return 데이터가 포함된  크래시 패킷 바디 바이트 버퍼를 반환합니다.
     */
    public byte[] GetByteBuffer()
    {
        return data_;
    }


    /**
     * @brief 데이터가 포함된 크래시 패킷 바디 바이트 버퍼의 크기를 얻습니다.
     * 
     * @return 데이터가 포함된 크래시 패킷 바디 바이트 버퍼의 크기를 반환합니다.
     */
    public int GetByteBufferSize()
    {
        return data_.Length;
    }
    

    /**
     * @brief 패킷 바디 부분의 데이터입니다.
     */
    public byte[] data_;
}


/**
 * @brief 패킷 전송 결과를 포함한 크래시 패킷 바디입니다.
 */
class CrashPacketResultBody : ISerialize
{
    /**
     * @brief 패킷 전송 결과를 포함한 크래시 패킷 바디의 빈 생성자입니다.
     */
    public CrashPacketResultBody() { }


    /**
     * @brief 패킷 전송 결과를 포함한 크래시 패킷 바디의 생성자입니다.
     * 
     * @param bodyBytes 크래시 패킷 바디의 바이트 형식입니다.
     */
    public CrashPacketResultBody(byte[] bodyBytes)
    {
        packetID_ = BitConverter.ToUInt32(bodyBytes, 0);
        result_ = bodyBytes[4];
    }


    /**
     * @brief 패킷 전송 결과를 포함한 크래시 패킷 바디 바이트 버퍼를 얻습니다.
     * 
     * @return 패킷 전송 결과를 포함한 크래시 패킷 바디 바이트 버퍼를 반환합니다.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        bytes[buffer.Length] = result_;

        return bytes;
    }


    /**
     * @brief 패킷 전송 결과를 포함한 크래시 패킷 바디 바이트 버퍼의 크기를 얻습니다.
     * 
     * @return 패킷 전송 결과를 포함한 크래시 패킷 바디 바이트 버퍼의 크기를 반환합니다.
     */
    public int GetByteBufferSize()
    {
        return sizeof(uint) + sizeof(byte);
    }


    /**
     * @brief 패킷의 아이디입니다.
     */
    private uint packetID_;


    /**
     * @brief 패킷의 전송 결과입니다.
     */
    private byte result_;
}