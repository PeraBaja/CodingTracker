using Spectre.Console;

static class TableData
{
    public static void ShowSessions(IEnumerable<CodingSession> codingSessions)
    {
        Table table = new();
        table.Expand();
        table.AddColumns([new TableColumn("Id").Centered(),
                new TableColumn("[green]Start Time Date[/]").Centered(),
                new TableColumn("[red]End Time Date[/]").Centered()
            ]);
        foreach (var codingSession in codingSessions)
        {
            table.AddRow($"[grey]{codingSession.Id}[/]",
                $"[grey]{codingSession.StartTime}[/]",
                $"[grey]{codingSession.EndTime}[/]"
            );
        }
        AnsiConsole.Write(table);
    }
}