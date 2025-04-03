namespace Tracker;

using Spectre.Console;
using System.IO;  // include the System.IO namespace

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
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine("Medicine and Dosage Information");
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
                        using (StreamWriter sw = File.CreateText(filePath))
                        {
                            sw.WriteLine("Medicine and Dosage Information for "+ name);
                            sw.WriteLine($"Medicine: {medicine_name}");
                            sw.WriteLine($"Dosage: {dosage}");
                            sw.WriteLine($"Frequency: {frequency}");
                        }
                        break;

                    case "2. Family member or pet.":
                        AnsiConsole.WriteLine("You chose to add a medication for a family member or pet.");
                        var family_member = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the name of the family member or pet: ")
                        );
                        break;

                    default:
                        AnsiConsole.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                break;

            //Add reminders
            case "2. Add a reminder for a medication.":
                AnsiConsole.WriteLine("You chose to add a reminder for a medication.");
                // Add code to add a reminder for a medication
                break;

            //Remove or edit a medicine and dosage
            case "3. Remove or edit a medicine and dosage.":
                AnsiConsole.WriteLine("You chose to remove or edit a medicine and dosage.");
                // Add code to remove or edit a medicine and dosage
                break;

            //View medicines and dosages
            case "4. View medicines and dosages.":
                AnsiConsole.WriteLine("You chose to view medicines and dosages.");
                // Add code to view medicines and dosages
                break;

            //View reminders
            case "5. View reminders.":
                AnsiConsole.WriteLine("You chose to view reminders.");
                // Add code to view reminders
                break;

            //View all information in a table
            case "6. View all information in a table.":
                AnsiConsole.WriteLine("You chose to view all information in a table.");
                // Add code to view all information in a table
                break;

            case "7. Exit":
                AnsiConsole.WriteLine("Exiting program...");
                Environment.Exit(0);
                break;

            default:
                AnsiConsole.WriteLine("Invalid choice. Please try again.");
                break;
        }
        
        
    }
}
