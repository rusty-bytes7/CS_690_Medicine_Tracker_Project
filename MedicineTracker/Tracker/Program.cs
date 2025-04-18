namespace Tracker;

using Spectre.Console;
using System.IO; 
using StreamWriter = System.IO.StreamWriter; 

class Program
{
    static void Main(string[] args)
    {
        //Set the console title
        AnsiConsole.MarkupLine("[italic bold cyan]Hello![/]");
        AnsiConsole.MarkupLine("[bold purple_1]Please input your name: [/]");
        string name = Console.ReadLine() ?? "User";
        AnsiConsole.Write(
            new FigletText("Welcome,")
                .Centered()
                .Color(Color.Purple_1)
        );

        AnsiConsole.Write(
            new FigletText(name)
                .Centered()
                .Color(Color.Green)
        );

        AnsiConsole.Write(
            new FigletText("to the Medicine Tracker!")
                .Centered()
                .Color(Color.Magenta2)
        );

        //Tell user about the application
        AnsiConsole.MarkupLine("[bold Magenta3_2]This is a medicine tracker that allows you to track your medicines and their dosages.[/]");
        AnsiConsole.WriteLine("\n");
        AnsiConsole.MarkupLine("[bold magenta3_2]You can add, remove, and view your medicines and their dosages.[/]");
        AnsiConsole.WriteLine("\n");
        AnsiConsole.MarkupLine("[bold magenta3_2]You can also set reminders for your medicines and their dosages, and view your information in tables.[/]");
        AnsiConsole.WriteLine("\n");

        //prompt the user for their initial choice
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(new[] 
                { 
                    "1. Add a medicine and dosage.", 
                    "2. Add a reminder for a medication.", 
                    "3. Remove or edit a medicine and dosage.", 
                    "4. View medicines and dosages.", 
                    "5. View reminders.", 
                    "6. View all information in tables.", 
                    "7. Exit" 
                })
        );

        string medicineFilePath = "medicinelist.csv";
        string reminderFilePath = "reminderlist.csv";

        // Initialize the tracker
        var tracker = new Tracker(medicineFilePath);

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

                    // Declare addtracker outside the switch statement
                    var addtracker = new Tracker(medicineFilePath);

                    // Add medication
                    switch (add_med_choice)
                    {
                        case "1. Myself.":
                            AnsiConsole.WriteLine("You chose to add a medication for yourself.");
                            var medicine_name = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the medication: ")
                            ).ToLower();
                            var dosage = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the dosage of the medication: ")
                            ).ToLower();
                            var frequency = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the frequency of the medication: ")
                            ).ToLower();

                            // Create a new medicine object
                            var medicine = new Medicine(medicine_name, dosage, frequency);
                            tracker.AddMedicine(name, medicine.Name, medicine.Dosage, medicine.Frequency);

                            AnsiConsole.WriteLine($"Medicine {medicine.Name} has been added for {name}.");
                            break;

                        case "2. Family member or pet.":
                            AnsiConsole.WriteLine("You chose to add a medication for a family member or pet.");
                            var family_member = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the family member or pet: ")
                            );
                            var fm_medicine_name = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the name of the medication: ")
                            ).ToLower();
                            var fm_dosage = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the dosage of the medication: ")
                            ).ToLower();
                            var fm_frequency = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the frequency of the medication: ")
                            ).ToLower();

                            // Create a new medicine object
                            var fam_medicine = new Medicine(fm_medicine_name, fm_dosage, fm_frequency);
                            addtracker.AddMedicine(family_member, fam_medicine.Name, fam_medicine.Dosage, fam_medicine.Frequency);

                            AnsiConsole.WriteLine($"Medicine {fam_medicine.Name} has been added for {family_member}.");
                            break;

                        default:
                            AnsiConsole.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    break;

                case "2. Add a reminder for a medication.":
                    AnsiConsole.WriteLine("You chose to add a reminder for a medication.");
                    if (!File.Exists(reminderFilePath))
                    {
                        using (StreamWriter textfile = File.CreateText(reminderFilePath))
                        {
                            textfile.WriteLine("Medication Reminders");
                        }
                    }

                    var med_name_reminder = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the name of medication you want to add a reminder for: ")
                    );
                    var reminder_time = AnsiConsole.Prompt(
                        new TextPrompt<string>("Please enter the time you want to be reminded (in HHMM format): ")
                    );

                    var addreminder = new Reminder(med_name_reminder, reminder_time);
                    addreminder.AddReminder(med_name_reminder, reminder_time);

                    AnsiConsole.WriteLine($"Reminder for {med_name_reminder} at {reminder_time} has been added.");
                    break;

                case "3. Remove or edit a medicine and dosage.":
                    AnsiConsole.WriteLine("You chose to remove or edit a medicine and dosage.");
                    var remove_edit_choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What do you want to do?")
                            .AddChoices(new[] { "1. Remove a medicine.", "2. Edit a medicine." })
                    );

                    switch (remove_edit_choice)
                    {
                        case "1. Remove a medicine.":
                            tracker.RemoveMedicine(name);
                            break;

                        case "2. Edit a medicine.":
                            tracker.EditMedicine(name);
                            break;

                        default:
                            AnsiConsole.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    break;

                case "4. View medicines and dosages.":
                    AnsiConsole.WriteLine("You chose to view medicines and dosages.");
                    tracker.ViewMedicines();
                    break;

                case "5. View reminders.":
                    AnsiConsole.WriteLine("You chose to view reminders.");
                    var reminder = new Reminder("med_name", "reminder_time");
                    reminder.ViewReminders();
                    break;

                case "6. View all information in tables.":
                    AnsiConsole.WriteLine("You chose to view all information in a tables.");
                    var tableMaker = new TableMaker(medicineFilePath);
                    var table = tableMaker.CreateTable(); 
                    AnsiConsole.Write(table); 
                    var tableMakerReminder = new TableMaker(reminderFilePath);
                    var reminderTable = tableMakerReminder.ReminderTable();
                    AnsiConsole.Write(reminderTable);
                    break;

                case "7. Exit":
                    AnsiConsole.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;

                default:
                    AnsiConsole.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            // Re-prompt the user for their choice
            choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(new[] 
                    { 
                        "1. Add a medicine and dosage.", 
                        "2. Add a reminder for a medication.", 
                        "3. Remove or edit a medicine and dosage.", 
                        "4. View medicines and dosages.", 
                        "5. View reminders.", 
                        "6. View all information in tables.", 
                        "7. Exit" 
                    })
            );
        }
    }
}
