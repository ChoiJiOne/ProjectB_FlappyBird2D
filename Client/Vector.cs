/**
 * @brief 2차원 벡터입니다.
 */
struct Vector2<T>
{
    /**
     * @brief 2차원 벡터의 생성자입니다.
     * 
     * @param xx 초기화 할 2차원 벡터의 첫 번째 인자입니다.
     * @param yy 초기화 할 2차원 벡터의 두 번째 인자입니다.
     */
    public Vector2(T xx, T yy)
    {
        X = xx;
        Y = yy;
    }


    /**
     * @brief 2차원 벡터의 X성분입니다.
     */
    public T X;


    /**
     * @brief 2차원 벡터의 Y성분입니다.
     */
    public T Y;
}


/**
 * @brief 3차원 벡터입니다.
 */
struct Vector3<T>
{
    /**
     * @brief 3차원 벡터의 생성자입니다.
     * 
     * @param xx 초기화 할 3차원 벡터의 첫 번째 인자입니다.
     * @param yy 초기화 할 3차원 벡터의 두 번째 인자입니다.
     * @param zz 초기화 할 3차원 벡터의 세 번째 인자입니다.
     */
    public Vector3(T xx, T yy, T zz)
    {
        X = xx;
        Y = yy;
        Z = zz;
    }


    /**
     * @brief 3차원 벡터의 X성분입니다.
     */
    public T X;


    /**
     * @brief 3차원 벡터의 Y성분입니다.
     */
    public T Y;


    /**
     * @brief 3차원 벡터의 Z성분입니다.
     */
    public T Z;
}


/**
 * @brief 4차원 벡터입니다.
 */
struct Vector4<T>
{
    /**
     * @brief 4차원 벡터의 생성자입니다.
     * 
     * @param xx 초기화 할 4차원 벡터의 첫 번째 인자입니다.
     * @param yy 초기화 할 4차원 벡터의 두 번째 인자입니다.
     * @param zz 초기화 할 4차원 벡터의 세 번째 인자입니다.
     * @param ww 초기화 할 4차원 벡터의 네 번째 인자입니다.
     */
    public Vector4(T xx, T yy, T zz, T ww)
    {
        X = xx;
        Y = yy;
        Z = zz;
        W = ww;
    }


    /**
     * @brief 4차원 벡터의 X성분입니다.
     */
    public T X;


    /**
     * @brief 4차원 벡터의 Y성분입니다.
     */
    public T Y;


    /**
     * @brief 4차원 벡터의 Z성분입니다.
     */
    public T Z;


    /**
     * @brief 4차원 벡터의 W성분입니다.
     */
    public T W;
}