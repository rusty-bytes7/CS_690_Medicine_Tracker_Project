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
        using (StreamWriter textfile = File.AppendText("reminderlist.csv"))
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
                        AnsiConsole.WriteLine("Reminders:");
                        foreach (var line in lines)
                        {
                            AnsiConsole.WriteLine(line);
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No reminders found.");
                    }
    }


}