# CodingTracker

Console based CRUD application to track time spent coding.
Developed using C# and SQLite.


# Given Requirements:

[x] This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.


[x] To show the data on the console, you should use the "Spectre.Console" library.


[x] You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)


[x] You should tell the user the specific format you want the date and time to be logged and not allow any other format.


[x] You'll need to create a configuration file that you'll contain your database path and connection strings.


[x] You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration


[x] The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.


[x] The user should be able to input the start and end times manually.


[x] You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)


[x] When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.


[x] Follow the DRY Principle, and avoid code repetition.

# Features

* SQLite database connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where users can navigate by keywords.


* CRUD DB functions

	- From the main menu users can Create, Read, Update or Delete entries for whichever date they want, entered in d-M-yyyy format. Duplicate days will be acepted. 
	- Time and Dates inputted are checked to make sure they are in the correct and realistic format. 

* Basic list of days tracked.

	

## Challenges

- Looking back, I realize I used some patterns that were unnecessarily overengineered for the scope of the project.

## Things to Improve

- The current UI can be hard to understand. Some commands are too verbose, which slows down the session-tracking process.

## Lessons Learned

- I learned how to implement a library to improve the console's visual appearance.

## Areas for Improvement

- **Flow and control structure:**  
  While the code is mostly readable, the nesting is a bit excessive. I'd like to explore patterns or techniques to reduce nesting and improve control flow clarity.

# Resources Used
[The README.md used as a guideline for writing my own](https://github.com/thags/ConsoleTimeLogger)
