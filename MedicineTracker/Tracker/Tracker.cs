namespace Tracker;

using System.IO;
using Spectre.Console;

public class Tracker
{
    private readonly string _filePath;

    //constructor to initialize the file path
    public Tracker(string filePath)
    {
        _filePath = filePath;

        // Ensure the file exists
        if (!File.Exists(_filePath))
        {
            using (StreamWriter textfile = File.CreateText(_filePath))
            {
                textfile.WriteLine("Name, Medicine, Dosage, Frequency");
            }
        }
    }

    //method to remove a medication from the file

    public void RemoveMedicine(string name)
    {
        if (!File.Exists(_filePath))
        {
            AnsiConsole.WriteLine("No medicines found to remove.");
        }
        else
        {
            bool isTest = false; //declare and initialize isTest-CHANGE THIS TO TRUE FOR TESTING
            string remove_medicine_name;

            if (isTest)
            {
                remove_medicine_name = "Test Medicine";
            }
            else
            {
                remove_medicine_name = AnsiConsole.Prompt(
                    new TextPrompt<string>("Please enter the name of the medication you want to remove: ")
                ).ToLower();
            }

            var lines = File.ReadAllLines(_filePath);
            using (StreamWriter textfile = new StreamWriter(_filePath))
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
    }

    //method to edit a medication in the file
    public void EditMedicine(string name)
    {
        if (!File.Exists(_filePath))
        {
            AnsiConsole.WriteLine("No medicines found to edit.");
        }
        else //file exists
        {
            bool isTest = false; //declare and initialize isTest- CHANGE THIS TO TRUE FOR TESTING
            string editMedicineName;

            if (isTest) //hardcoded test medicine name
            {
                editMedicineName = "Medicine 1";

            }
            else //if not a test, prompt user for medicine name
            {
                editMedicineName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Please enter the name of the medication you want to edit: ")
                ).ToLower();
            }

            var lines = File.ReadAllLines(_filePath);
            using (StreamWriter textfile =new StreamWriter(_filePath))
            {
                foreach (var line in lines)
                {
                    if (!line.Contains(editMedicineName)) //if line not found, write line
                    {
                        textfile.WriteLine(line);
                    }
                    else //if line found, prompt user for new medicine name, dosage and frequency
                    {
                        string newMedicineName;
                        string newDosage;
                        string newFrequency;

                        if (isTest) //hardcoded test medicine name if a test
                        {
                            //hardcoded test medicine name
                            AnsiConsole.WriteLine("Editing Test Medicine...");
                            //hardcoded test medicine details
                            newMedicineName = "Medicine 2";
                            newDosage = "200mg";
                            newFrequency = "Once a day";
                        }
                        else
                        {
                            newMedicineName = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the new name of the medication: ")
                            ).ToLower();
                            newDosage = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the new dosage of the medication: ")
                            ).ToLower();
                            newFrequency = AnsiConsole.Prompt(
                                new TextPrompt<string>("Please enter the new frequency of the medication: ")
                            ).ToLower();
                        }

                        textfile.WriteLine($"{name}, {newMedicineName}, {newDosage}, {newFrequency}");
                        
                    }
                }
            }
            AnsiConsole.WriteLine($"Medicine {editMedicineName} has been edited.");
        }
    }

    // Method to add a medicine to the file
    public void AddMedicine(string name, string medicine, string dosage, string frequency)
    {
        using (StreamWriter textfile = File.AppendText(_filePath))
        {
            textfile.WriteLine($"{name}, {medicine}, {dosage}, {frequency}");
        }
    }

    // Method to view all medicines
    public void ViewMedicines()
    {
        AnsiConsole.MarkupLine("[bold magenta]Medicines:[/]");
        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);
            //if file is blank display blank
            if (lines.Length == 1)
            {
                AnsiConsole.WriteLine("No medicines found.");
                return;
            }
            foreach (var line in lines)
            {
                AnsiConsole.MarkupLine(line);
            }
        }
        else
        {
            Console.WriteLine("No medicines found.");
        }
        AnsiConsole.WriteLine("\n");
    }
}