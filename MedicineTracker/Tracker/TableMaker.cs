using Spectre.Console;
using System.IO;

public class TableMaker
{
    private readonly string _filePath;

    //Constructor to initialize the file path
    public TableMaker(string filePath)
    {
        _filePath = filePath;
    }

    public Table CreateTable()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[bold magenta3_1]Name[/]");
        table.AddColumn("[bold orangered1]Medicine[/]");
        table.AddColumn("[bold yellow]Dosage[/]");
        table.AddColumn("[bold green]Frequency[/]");

        // Check if the file exists
        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);

            //skip the header row and add each line as a row in the table
            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(','); // Split the line into columns
                table.AddRow(columns); // Add the columns as a row in the table
            }
        }
        else
        {
            
            table.AddRow("No data found", "", "", "", "");
        }

        return table; //Return the populated table
    }

     public Table ReminderTable()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[bold cyan2]Medicine[/]");
        table.AddColumn("[bold blueviolet]Reminder Time[/]");

        // Check if the file exists
        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);

            //skip the header row and add each line as a row in the table
            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(','); // Split the line into columns
                table.AddRow(columns); // Add the columns as a row in the table
            }
        }
        else
        {
            
            table.AddRow("No data found", "", "", "", "");
        }

        return table; //Return the populated table
    }
}