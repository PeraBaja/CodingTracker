using Spectre.Console;
using CodingTracker.Exceptions;
class Menu(CodingController codingController, StopwatchView stopwatchView)
{
    public void Display()
    {
        while (true)
    {
        Markup markup = new("[gray]Welcome to the coding tracker! [/]" +
        "\n These are the aviable options: list - add - delete - update - stopwatch" +
        "\n Or type \"exit\" to close the program", new Style(foreground: Color.Aqua));
        AnsiConsole.Write(markup);
            string userOption = UserInput.GetOption();
            if (userOption.Equals("exit")) Environment.Exit(0);
            else SelectOption(userOption);
        }
    }
    void SelectOption(string userOption)
    {
        switch (userOption)
        {
            case "stopwatch":
                stopwatchView.Display();
                break;
            case "add":
                Add();
                break;
            case "delete":
                Delete();
                break;
            case "update":
                Update();
                break;
            case "list":
                List();
                break;
        }
    }

    void List()
    {
        var codingSessions = codingController.List();


        while (true)
        {
            Console.WriteLine("You can filter by typing: 'last week' - last month - 'last year' " +
            "and you can sort by typing: 'ascending' - 'descending'");
            var option = UserInput.GetOption();
            if (option.Equals("go back")) return;
            IEnumerable<CodingSession> filteredCodingSessions = [];
            switch (option)
            {

                case "last week":
                    {
                        filteredCodingSessions = codingSessions.Where(s =>
                        {
                            Console.WriteLine(DateTime.Now.Subtract(s.EndTime).Days);
                            return DateTime.Now.Subtract(s.EndTime).Days < 7 && DateTime.Now.Subtract(s.EndTime).Days > 0;
                        });
                        break;
                    }
                case "last month":
                    {
                        filteredCodingSessions = codingSessions.Where(s => s.EndTime.Month == DateTime.Today.Month && s.EndTime.Year == DateTime.Today.Year);
                        break;
                    }
                case "last year":
                    {
                        filteredCodingSessions = codingSessions.Where(s => s.EndTime.Year == DateTime.Today.Year);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error. The typed filter option doesn't exist");
                        break;
                    }

            }
            TableData.ShowSessions(filteredCodingSessions);
        }
    }

    void Add()
    {
        var startTime = UserInput.GetDateTime();
        if (startTime is null) return; // to menu
        var endTime = UserInput.GetDateTime();
        if (endTime is null) return; // to menu
        QueryStatus result;
        try
        {
            result = codingController.Add(startTime, endTime);
        }
        catch (WrongEndTimeException)
        {
            Console.WriteLine("Is not posible to establish the end time because "
            + "is earlier than the start time");
            return;
        }

        if (result is QueryStatus.Succeded) Console.WriteLine("The session has been recorded");
        else Console.WriteLine("Something went wrong. The session hasn't been recorded");


    }
    void Update()
    {
        ulong selectedId = UserInput.GetId();
        if (codingController.SessionExistsWithId(selectedId) is QueryStatus.Failed)
        {
            Console.WriteLine($"There are no session with id {selectedId}. Please, try again.");
            return;
        }
        DateTime? startTime = UserInput.GetDateTime();
        if (startTime is null) return;
        DateTime? endTime = UserInput.GetDateTime();
        if (endTime is null) return;
        try
        {
            codingController.Modify(selectedId, startTime, endTime);
        }
        catch (WrongEndTimeException)
        {
            Console.WriteLine("Is not posible to establish the end time because "
            + "is earlier than the start time");
        }
    }
    void Delete()
    {
        ulong selectedId = UserInput.GetId();
        if (codingController.SessionExistsWithId(selectedId) is QueryStatus.Failed)
        {
            Console.WriteLine($"There are no session with id {selectedId}. Please, try again.");
            return;
        }
        var result = codingController.Delete(selectedId);
        if (result is QueryStatus.Succeded) Console.WriteLine("The selected session was succesfully deleted");
    }
}