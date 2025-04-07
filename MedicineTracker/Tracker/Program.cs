namespace Tracker;

using Spectre.Console;
using System.IO;  // include the System.IO namespace
using StreamWriter = System.IO.StreamWriter; // include the StreamWriter class
using System.Net;
using System.Net.Mail; 

class Program
{
    static void Main(string[] args)
    {
        // Set the console title
        Console.WriteLine("Hello!");
        AnsiConsole.WriteLine("\n");
        Console.WriteLine("Please input your name: ");
        string name = Console.ReadLine() ?? "User";
        AnsiConsole.Write(new FigletText($"Welcome, {name}, to the Medicine Tracker!")
            .Centered()
            .Color(Color.Purple_1));

        // Tell user about the application
        AnsiConsole.WriteLine("\n");
        AnsiConsole.WriteLine("This is a medicine tracker that allows you to track your medicines and their dosages.");
        AnsiConsole.WriteLine("\n");
        AnsiConsole.WriteLine("You can add, remove, and view your medicines and their dosages.");
        AnsiConsole.WriteLine("\n");
        AnsiConsole.WriteLine("You can also set reminders for your medicines and their dosages, and view your information in a table.");
        AnsiConsole.WriteLine("\n");

        // Prompt the user for their initial choice
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(new[] { "1. Add a medicine and dosage.", "2. Add a reminder for a medication.", "3. Remove or edit a medicine and dosage.", "4. View medicines and dosages.", "5. View reminders.", "6. View all information in a table.", "7. Exit" })
        );

        while (choice != "7. Exit") // Loop until user chooses to exit
        {
            // Switch statement to handle user choice
            switch (choice)
            {
                case "1. Add a medicine and dosage.":
                    AnsiConsole.WriteLine("You chose to add a medicine and dosage.");
                    var add_med_choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Who do you want to add a medication for?")
                            .AddChoices(new[] { "1. Myself.", "2. Family member or pet." })
                    );
                    AnsiConsole.WriteLine(add_med_choice);

                    // Create a blank file to store the medicine and dosage information
                    string filePath = "medicines.csv";
                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter textfile = File.CreateText(filePath))
                        {
                            textfile.WriteLine("Medicine and Dosage Information");
                        }
                    }

                    switch (add_med_choice)
                    {
                        case "1. Myself.":
                            AnsiConsole.WriteLine("You chose to add a medication for yourself.");
                            var medicine_name = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the medication: ")
                            );
                            var dosage = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the dosage of the medication: ")
                            );
                            var frequency = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the frequency of the medication: ")
                            );
                            using (StreamWriter textfile = File.AppendText(filePath))
                            {
                                textfile.WriteLine("\n");
                                textfile.WriteLine("Medicine and Dosage Information for " + name);
                                textfile.WriteLine($"Medicine: {medicine_name}");
                                textfile.WriteLine($"Dosage: {dosage}");
                                textfile.WriteLine($"Frequency: {frequency}");
                            }
                            break;

                        case "2. Family member or pet.":
                            AnsiConsole.WriteLine("You chose to add a medication for a family member or pet.");
                            var family_member = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the family member or pet: ")
                            );
                            var fm_medicine_name = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the medication: ")
                            );
                            var fm_dosage = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the dosage of the medication: ")
                            );
                            var fm_frequency = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the frequency of the medication: ")
                            );
                            using (StreamWriter textfile = File.AppendText(filePath))
                            {
                                textfile.WriteLine("\n");
                                textfile.WriteLine("Medicine and Dosage Information for " + family_member);
                                textfile.WriteLine($"Medicine: {fm_medicine_name}");
                                textfile.WriteLine($"Dosage: {fm_dosage}");
                                textfile.WriteLine($"Frequency: {fm_frequency}");
                            }
                            break;

                        default:
                            AnsiConsole.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    break;

                case "2. Add a reminder for a medication.":
                    AnsiConsole.WriteLine("You chose to add a reminder for a medication.");
                    string filePathreminders = "reminders.csv";
                    if (!File.Exists(filePathreminders))
                    {
                        using (StreamWriter textfile = File.CreateText(filePathreminders))
                        {
                            textfile.WriteLine("Medication Reminders");
                            textfile.WriteLine("====================");
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("File already exists.");
                    }

                    //Suggestion: Prompt user for choice from medications they have already entered
                    var med_name_reminder = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the name of medication you want to add a reminder for: ")
                    );
                    var reminder_time = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the time you want to be reminded (in HH:MM format): ")
                    );

                    using (StreamWriter textfile = File.AppendText(filePathreminders))
                    {
                        textfile.WriteLine("\n");
                        textfile.WriteLine("Medication Reminders for " + name);
                        textfile.WriteLine($"Medication: {med_name_reminder}");
                        textfile.WriteLine($"Reminder Time: {reminder_time}");
                    }
                    AnsiConsole.WriteLine($"Reminder for {med_name_reminder} at {reminder_time} has been added.");
                    break;
                // Suggestion: Add a feature to send email reminders
                

                case "3. Remove or edit a medicine and dosage.":
                    AnsiConsole.WriteLine("You chose to remove or edit a medicine and dosage.");
                    string filePathremove = "medicines.csv";
                    if (!File.Exists(filePathremove))
                    {
                        AnsiConsole.WriteLine("No medicines found to remove or edit.");
                    }
                    else
                    {
                        var remove_medicine_name = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the name of the medication you want to remove: ")
                        );
                        var lines = File.ReadAllLines(filePathremove);
                        using (StreamWriter textfile = new StreamWriter(filePathremove))
                        {
                            foreach (var line in lines)
                            {
                                if (!line.Contains(remove_medicine_name))
                                {
                                    textfile.WriteLine(line);
                                }
                            }
                        }
                        AnsiConsole.WriteLine($"Medicine {remove_medicine_name} has been removed.");
                    }
                    break;

                case "4. View medicines and dosages.":
                    AnsiConsole.WriteLine("You chose to view medicines and dosages.");
                    string filePathview = "medicines.csv";
                    if (File.Exists(filePathview))
                    {
                        var lines = File.ReadAllLines(filePathview);
                        AnsiConsole.WriteLine("Medicines and Dosages:");
                        foreach (var line in lines)
                        {
                            AnsiConsole.WriteLine(line);
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No medicines found.");
                    }
                    break;

                case "5. View reminders.":
                    AnsiConsole.WriteLine("You chose to view reminders.");
                    string filePathreminder = "reminders.csv";
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
                    break;

                case "6. View all information in a table.":
                    AnsiConsole.WriteLine("You chose to view all information in a table.");
                    var table = new Table();
                    table.Border(TableBorder.Rounded);
                    table.AddColumn("Name");
                    table.AddColumn("Medicine");
                    table.AddColumn("Dosage");
                    table.AddColumn("Frequency");
                    table.AddColumn("Reminder Time");

                    StreamReader sr = new StreamReader("medicines.csv");
                    var info = sr.ReadLine();
                    while (info != null)
                    {
                        info = sr.ReadLine();
                    }
                    sr.Close();

                    AnsiConsole.Write(table);
                    break;

                case "7. Exit":
                    AnsiConsole.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;

                default:
                    AnsiConsole.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            // Re-prompt the user for their choice to loop back to the main menu
            choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(new[] { "1. Add a medicine and dosage.", "2. Add a reminder for a medication.", "3. Remove or edit a medicine and dosage.", "4. View medicines and dosages.", "5. View reminders.", "6. View all information in a table.", "7. Exit" })
            );
        }
    }
}
