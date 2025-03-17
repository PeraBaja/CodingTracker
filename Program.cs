using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("./appsettings.json")
    .Build();

CodingController codingController = new(config);
StopwatchView stopwatchView = new(codingController);
codingController.CreateTable();

Menu menu = new(codingController, stopwatchView);

menu.Display();
