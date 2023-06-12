using System.Data;
using System.Data.SQLite;


/**
 * @brief 데이터베이스를 로딩하고 관리하는 클래스입니다.
 */
class Database : IContent
{
    /**
     * @brief 데이터베이스를 로딩하고 관리하는 클래스의 생성자입니다.
     * 
     * @param path 데이터베이스의 경로입니다.
     * 
     * @throws 데이터베이스가 유효하지 않으면 예외를 던집니다.
     */
    public Database(string path)
    {
        string connectionString = string.Format("Data Source={0}", path);

        sqlConnection_ = new SQLiteConnection(connectionString);
        sqlConnection_.Open();
    }


    /**
     * @brief 데이터베이스 리소스의 데이터를 명시적으로 정리합니다.
     */
    public void Release()
    {
        sqlConnection_.Close();
    }


    /**
     * @brief SQLite 데이터베이스에 대한 연결을 나타냅니다.
     * 
     * @see https://learn.microsoft.com/ko-kr/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-7.0.0
     */
    SQLiteConnection sqlConnection_;
}