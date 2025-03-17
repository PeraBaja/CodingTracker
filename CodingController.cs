using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

enum QueryStatus
{
    Failed = 0,
    Succeded = 1
}

class CodingController(IConfigurationRoot config)
{
    readonly SqliteConnection _connection = new(config.GetConnectionString("Database Connection"));
    public QueryStatus Add(DateTime? startTime, DateTime? endTime)
    {
        CodingSession codingSession = new();
        codingSession.StartTime = startTime ?? DateTime.MinValue;
        codingSession.EndTime = endTime ?? DateTime.MinValue;
        _connection.Open();
        int result = _connection.Execute("INSERT INTO coding_session (startTime, endTime) Values (@StartTime, @EndTime)", codingSession);
        _connection.Close();
        return (QueryStatus)result;
    }
    public QueryStatus CreateTable()
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS coding_session" +
            "(id INTEGER PRIMARY KEY AUTOINCREMENT, startTime DATE, endTime DATE)";
        int result = command.ExecuteNonQuery();
        _connection.Close();
        return (QueryStatus)result;
    }

    public QueryStatus Delete(ulong id)
    {
        _connection.Open();
        int result = _connection.Execute("DELETE FROM coding_session WHERE id = @Id", new { Id = id });
        _connection.Close();
        return (QueryStatus)result;
    }

    public QueryStatus Modify(ulong id, DateTime? startTime, DateTime? endTime)
    {
        CodingSession selectedSession = GetSessionById(id);
        selectedSession.StartTime = startTime ?? selectedSession.StartTime;

        selectedSession.EndTime = endTime ?? selectedSession.EndTime;
        _connection.Open();
        int result = _connection.Execute("INSERT INTO coding_session (startTime, endTime) Values (@StartTime, @EndTime)", selectedSession);
        _connection.Close();
        return (QueryStatus)result;
    }
    public CodingSession GetSessionById(ulong id)
    {
        _connection.Open();
        var codingSessions = _connection.QuerySingle<CodingSession>("SELECT * FROM coding_session WHERE id = @Id", new { Id = id });
        _connection.Close();
        return codingSessions;
    }
    public QueryStatus SessionExistsWithId(ulong id)
    {
        _connection.Open();
        var count = _connection.ExecuteScalar<int>("SELECT Count(*) FROM coding_session WHERE id = @Id ", new { Id = id });
        _connection.Close();
        return (QueryStatus)count;
    }

    public IEnumerable<CodingSession> List()
    {
        _connection.Open();
        var codingSessions = _connection.Query<CodingSession>("SELECT * FROM coding_session");
        _connection.Close();
        return codingSessions;
    }
}