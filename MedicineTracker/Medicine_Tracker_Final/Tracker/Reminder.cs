using System;
using Spectre.Console;
public class Reminder{
    public string Name { get; set; }
    public string Time { get; set; }

    public Reminder(string medicine_name, string time)
    {
        Name = medicine_name;
        Time = time;
    }

    //Method to add a reminder to the file
    public void AddReminder(string name, string time)
    {
        string filePath = "reminderlist.csv";

        //ensure the file exists
        if (!File.Exists(filePath))
        {
            using (StreamWriter textfile = File.AppendText(filePath))
            {
                textfile.WriteLine("Medication Reminders");
            }
        }

        //Append the reminder
        using (StreamWriter textfile = File.AppendText(filePath))
        {
            textfile.WriteLine($"{name}, {time}");
        }
    }

    public void ViewReminders()
    {
       string filePathreminder = "reminderlist.csv";
                    if (File.Exists(filePathreminder))
                    {
                        var lines = File.ReadAllLines(filePathreminder);
                        //if file is blank display blank
            if (lines.Length == 0)
            {
                AnsiConsole.WriteLine("No reminders found.");
                return;
            }
                        AnsiConsole.MarkupLine("[bold cyan2]Reminders:[/]");
                        foreach (var line in lines)
                        {
                            AnsiConsole.WriteLine(line);
                        }
                        AnsiConsole.WriteLine("\n");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No reminders found.");
                    }
    }


}