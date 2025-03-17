using CodingTracker.Exceptions;
class CodingSession
{
    public ulong Id { get; set; }
    public DateTime StartTime { get; set; }

    DateTime _endTime;
    public DateTime EndTime
    {
        get => _endTime; set
        {
            if (value.CompareTo(StartTime) <= 0)
                throw new WrongEndTimeException("The end time should be later than the start time");

            _endTime = value;
        }
    }

    public TimeSpan DurationInSeconds { get => EndTime.Subtract(StartTime); }
}