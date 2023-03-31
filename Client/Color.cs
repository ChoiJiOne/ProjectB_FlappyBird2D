using System;


/**
 * @brief RGBA 색상을 나타내는 클래스입니다.
 */
struct Color
{
    /**
     * @brief 인자가 모두 float 형식인 RGBA 색상을 나타내는 클래스의 생성자입니다.
     * 
     * @note 0.0 ~ 1.0 사이의 값만 처리할 수 있습니다.
     * 
     * @param rr RGBA 색상 중 float 타입의 R값입니다.
     * @param gg RGBA 색상 중 float 타입의 G값입니다.
     * @param bb RGBA 색상 중 float 타입의 B값입니다.
     * @param aa RGBA 색상 중 float 타입의 A값입니다.
     */
    public Color(float rr, float gg, float bb, float aa)
    {
        r = Math.Min(Math.Max(0.0f, rr), 1.0f);
        g = Math.Min(Math.Max(0.0f, gg), 1.0f);
        b = Math.Min(Math.Max(0.0f, bb), 1.0f);
        a = Math.Min(Math.Max(0.0f, aa), 1.0f);
    }


    /**
     * @brief 인자가 모두 byte 형식인 RGBA 색상을 나타내는 클래스의 생성자입니다.
     * 
     * @param rr RGBA 색상 중 byte 타입의 R값입니다.
     * @param gg RGBA 색상 중 byte 타입의 G값입니다.
     * @param bb RGBA 색상 중 byte 타입의 B값입니다.
     * @param aa RGBA 색상 중 byte 타입의 A값입니다.
     */
    public Color(byte rr, byte gg, byte bb, byte aa)
    {
        r = (float)(rr) / 255.0f;
        g = (float)(gg) / 255.0f;
        b = (float)(bb) / 255.0f;
        a = (float)(aa) / 255.0f;
    }


    /**
     * @brief R, G, B, A값을 바이트 형식으로 변환합니다.
     * 
     * @param red[out] 바이트 형식으로 변환된 R 값입니다.
     * @param green[out] 바이트 형식으로 변환된 G 값입니다.
     * @param blue[out] 바이트 형식으로 변환된 B 값입니다.
     * @param alpha[out] 바이트 형식으로 변환된 A 값입니다.
     */
    public void ConvertToByte(out byte red, out byte green, out byte blue, out byte alpha)
    {
        red   = (byte)(Math.Min(Math.Max(0.0f, r), 1.0f) * 255.0f);
        green = (byte)(Math.Min(Math.Max(0.0f, g), 1.0f) * 255.0f);
        blue  = (byte)(Math.Min(Math.Max(0.0f, b), 1.0f) * 255.0f);
        alpha = (byte)(Math.Min(Math.Max(0.0f, a), 1.0f) * 255.0f);
    }


    /**
     * @brief 기본적으로 정의된 색상입니다.
     */
    public static Color BLACK   = new Color(0.0f, 0.0f, 0.0f, 1.0f); // 검정색
    public static Color WHITE   = new Color(1.0f, 1.0f, 1.0f, 1.0f); // 하얀색
    public static Color RED     = new Color(1.0f, 0.0f, 0.0f, 1.0f); // 빨간색
    public static Color GREEN   = new Color(0.0f, 1.0f, 0.0f, 1.0f); // 초록색
    public static Color BLUE    = new Color(0.0f, 0.0f, 1.0f, 1.0f); // 파란색
    public static Color YELLOW  = new Color(1.0f, 1.0f, 0.0f, 1.0f); // 노란색
    public static Color MAGENTA = new Color(1.0f, 0.0f, 1.0f, 1.0f); // 자홍색
    public static Color CYAN    = new Color(0.0f, 1.0f, 1.0f, 1.0f); // 청록색


    /**
     * @brief RGBA 색상 중 R값입니다.
     */
    public float r;


    /**
     * @brief RGBA 색상 중 G값입니다.
     */
    public float g;


    /**
     * @brief RGBA 색상 중 B값입니다.
     */
    public float b;


    /**
     * @brief RGBA 색상 중 A값입니다.
     */
    public float a;
}