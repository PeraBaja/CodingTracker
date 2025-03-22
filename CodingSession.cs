using CodingTracker.Exceptions;
class CodingSession
{
    public ulong Id { get; set; }
    DateTime _startTime;
    public DateTime StartTime
    {
        get => _startTime; set
        {
            if (value.CompareTo(DateTime.Now) > 0)
                throw new ArgumentException("The end time must be set before now.");
            _startTime = value;
        }
    }

    DateTime _endTime;
    public DateTime EndTime
    {
        get => _endTime; set
        {
            if (value.CompareTo(DateTime.Now) > 0)
                throw new ArgumentException("The end time must be set before now.");
            if (value.CompareTo(StartTime) <= 0)
                throw new WrongEndTimeException("The end time should be later than the start time");
            _endTime = value;
        }
    }

    public TimeSpan DurationInSeconds { get => EndTime.Subtract(StartTime); }
}