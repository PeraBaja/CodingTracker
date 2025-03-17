using System.Diagnostics;
class StopwatchView(CodingController codingController)
{

    readonly Stopwatch _stopwatch = new();
    public void Display()
    {
        Console.WriteLine("The stopwatch mode allows you to easily track your current coding session by a few comands: " +
        "\n\tOptions: start - reset - stop");
        string action = Console.ReadLine()?.ToLower() ?? "";
        switch (action)
        {
            case "start":
                if (_stopwatch.IsRunning)
                {
                    Console.WriteLine("The stopwatch is already running. Use 'reset' for reseting");
                    return;
                }
                Console.WriteLine("The stopwatch has been started");
                _stopwatch.Start();
                break;
            case "reset":
                if (_stopwatch.ElapsedMilliseconds == 0)
                {
                    Console.WriteLine("The stopwatch is already reseted.");
                    return;
                }
                _stopwatch.Reset();
                Console.WriteLine("The stopwatch has been reseted");
                break;
            case "stop":
                if (!_stopwatch.IsRunning)
                {
                    Console.WriteLine("the stopwatch didn't started");
                    return;
                }
                _stopwatch.Stop();
                var startTime = DateTime.Now.Subtract(_stopwatch.Elapsed);
                codingController.Add(startTime, DateTime.Now);
                Console.WriteLine("The stopwatch has been stoped and the session has been recorded");
                break;
            default:
                Console.WriteLine("The typed action doesn't exist. Please try again;");
                break;
        }

    }
}