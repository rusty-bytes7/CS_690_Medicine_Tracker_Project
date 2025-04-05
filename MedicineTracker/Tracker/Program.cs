namespace Tracker;

using Spectre.Console;
using System.IO;  // include the System.IO namespace
using StreamWriter = System.IO.StreamWriter; // include the StreamWriter class

class Program
{
    static void Main(string[] args)
    {
        // Set the console title
        Console.WriteLine("Hello!");
        Console.WriteLine("Please input your name: ");
        string name = Console.ReadLine() ?? "User";
        AnsiConsole.Write(new FigletText($"Welcome, {name}, to the Medicine Tracker!")
        .Centered()
        .Color(Color.Purple_1));

        // Tell user about the application
        AnsiConsole.WriteLine("This is a medicine tracker that allows you to track your medicines and their dosages.");
        AnsiConsole.WriteLine("You can add, remove, and view your medicines and their dosages.");
        AnsiConsole.WriteLine("You can also set reminders for your medicines and their dosages, and view your information in a table.");
        AnsiConsole.WriteLine("\n");

        //Show user lsit of options and stores in choice variable
        AnsiConsole.WriteLine("Please choose an option from the menu below:");
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?").AddChoices(new[] { "1. Add a medicine and dosage.", "2. Add a reminder for a medication.", "3. Remove or edit a medicine and dosage.", "4. View medicines and dosages.", "5. View reminders.", "6. View all information in a table.", "7. Exit" } )
        );
        AnsiConsole.WriteLine(choice);
        
        // Switch statement to handle user choice
        switch (choice)
        {
            case "1. Add a medicine and dosage.":
                AnsiConsole.WriteLine("You chose to add a medicine and dosage.");
                var add_med_choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Who do you want to add a medication for?").AddChoices(new[] { "1. Myself.", "2. Family member or pet." } )
                );
                AnsiConsole.WriteLine(add_med_choice);

                //create a blank file to store the medicine and dosage information
                string filePath = "medicines.txt";
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
                            textfile.WriteLine("Medicine and Dosage Information for "+ name);
                            textfile.WriteLine($"Medicine: {medicine_name}");
                            textfile.WriteLine($"Dosage: {dosage}");
                            textfile.WriteLine($"Frequency: {frequency}");
                        }

                        //loop back to main menu- add this functionality in the future
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
                            textfile.WriteLine("Medicine and Dosage Information for "+ family_member);
                            textfile.WriteLine($"Medicine: {fm_medicine_name}");
                            textfile.WriteLine($"Dosage: {fm_dosage}");
                            textfile.WriteLine($"Frequency: {fm_frequency}");
                        }

                        //loop back to main menu- add this functionality in the futurev
                        break;

                    default:
                        AnsiConsole.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                break;

            //Add reminders
            case "2. Add a reminder for a medication.":
                AnsiConsole.WriteLine("You chose to add a reminder for a medication.");
                //create a blank file to store the reminder information
                string filePathreminders = "reminders.txt";
                if (!File.Exists(filePathreminders))
                {
                    using (StreamWriter textfile = File.CreateText(filePathreminders))
                    {
                        textfile.WriteLine("Medication Reminders");
                        textfile.WriteLine("====================");
                    }
                }


                var med_name_reminder = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the name of medication you want to add a reminder for: "));
                var reminder_time = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the time you want to be reminded (in HH:MM format): "));
                
                //add reminders to the reminders file
                using (StreamWriter textfile = File.AppendText(filePathreminders))
                {
                    textfile.WriteLine("\n");
                    textfile.WriteLine("Medication Reminders for "+ name);
                    textfile.WriteLine($"Medication: {med_name_reminder}");
                    textfile.WriteLine($"Reminder Time: {reminder_time}");
                }
                AnsiConsole.WriteLine($"Reminder for {med_name_reminder} at {reminder_time} has been added.");

                //loop back to main menu- add this functionality in the futurev
                break;

            //Remove or edit a medicine and dosage
            case "3. Remove or edit a medicine and dosage.":
                AnsiConsole.WriteLine("You chose to remove or edit a medicine and dosage.");

                //Remove medicine from the medicines file
                string filePathremove = "medicines.txt";
                if (!File.Exists(filePathremove))
                {
                    AnsiConsole.WriteLine("No medicines found to remove or edit.");
                }
                else
                {
                    var remove_medicine_name = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the name of the medication you want to remove: "));
                    var lines = File.ReadAllLines(filePathremove);
                    using (StreamWriter textfile = new StreamWriter(filePathremove))
                    {
                        foreach (var line in lines)
                        {
                            if (!line.Contains(remove_medicine_name))
                            {
                                //this method only removves the name, not the dosage or frequency
                                //this is a bug that needs to be fixed in the future
                                textfile.WriteLine(line);
                            }
                        }
                    }
                    AnsiConsole.WriteLine($"Medicine {remove_medicine_name} has been removed.");
                }
                
                    //loop back to main menu- add this functionality in the future
                break;

            //View medicines and dosages
            case "4. View medicines and dosages.":
                AnsiConsole.WriteLine("You chose to view medicines and dosages.");
                // Read the medicines file and display the contents
                string filePathview = "medicines.txt";
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

            //View reminders
            case "5. View reminders.":
                AnsiConsole.WriteLine("You chose to view reminders.");
                string filePathreminder = "reminders.txt";
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

                //loop back to main menu- add this functionality in the future
                break;

            //View all information in a table
            case "6. View all information in a table.":
                AnsiConsole.WriteLine("You chose to view all information in a table.");
                var table = new Table();
                table.Border(TableBorder.Rounded);
                table.AddColumn("Name");
                table.AddColumn("Medicine");
                table.AddColumn("Dosage");
                table.AddColumn("Frequency");
                table.AddColumn("Reminder Time");
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("medicines.txt");
                //Read the first line of text
                var info = sr.ReadLine();
                //Continue to read until you reach end of file
                while (info != null)
                {
                    //Read the next line
                    info = sr.ReadLine();
                }
                //close the file
                sr.Close();

                //Add the information to the table
                AnsiConsole.Write(table);

                //loop back to main menu- add this functionality in the future
                break;

            case "7. Exit":
                AnsiConsole.WriteLine("Exiting program...");
                Environment.Exit(0);
                break;

            default:
                AnsiConsole.WriteLine("Invalid choice. Please try again.");
                return;
        }
        
        
    }
}
