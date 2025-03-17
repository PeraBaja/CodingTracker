static class UserInput
{
    public static ulong GetId()
    {
        Console.WriteLine("Type the id to select the desire session:");
        ulong id;
        while (!ulong.TryParse(Console.ReadLine() ?? "", out id) || id == 0)
        {
            Console.WriteLine("Not a valid id. Please try again");
        }
        return id;
    }
    public static string GetOption()
    {
        Console.WriteLine("Select an option");
        return Console.ReadLine() ?? "";
    }
    static public DateTime? GetDateTime()
    {
        Console.WriteLine("Enter the date and the time in the following format (d-M-yyyy)\n" +
        "Type \"now\" to set to current time - Type \"go back\" to return to menu");
        DateTime date;
        string userInput = Console.ReadLine() ?? "";
        while (!DateTime.TryParse(userInput, out date))
        {

            if (userInput.Equals("go back")) return null;
            else if (userInput.Equals("now")) return DateTime.Now;
            Console.WriteLine("Not a valid date or time. Please try again");
            userInput = Console.ReadLine() ?? "";
        }
        return date;
    }
}