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

    // Method to remove a medication from the file
    public void RemoveMedicine(string name)
    {
        if (!File.Exists(_filePath))
        {
            AnsiConsole.WriteLine("No medicines found to remove.");
        }
        else
        {
            var remove_medicine_name = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the name of the medication you want to remove: ")
            ).ToLower();

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

    // Method to edit a medication in the file
    public void EditMedicine(string name)
    {
        if (!File.Exists(_filePath))
        {
            AnsiConsole.WriteLine("No medicines found to edit.");
        }
        else
        {
            var edit_medicine_name = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the name of the medication you want to edit: ")
            ).ToLower();

            var lines = File.ReadAllLines(_filePath);
            using (StreamWriter textfile = new StreamWriter(_filePath))
            {
                foreach (var line in lines)
                {
                    if (!line.Contains(edit_medicine_name))
                    {
                        textfile.WriteLine(line);
                    }
                    else
                    {
                        var new_medicine_name = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the new name of the medication: ")
                        ).ToLower();
                        var new_dosage = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the new dosage of the medication: ")
                        ).ToLower();
                        var new_frequency = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the new frequency of the medication: ")
                        ).ToLower();

                        textfile.WriteLine($"{name}, {new_medicine_name}, {new_dosage}, {new_frequency}");
                    }
                }
            }
            AnsiConsole.WriteLine($"Medicine {edit_medicine_name} has been edited.");
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
        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);
            //if file is blank display blank
            if (lines.Length == 0)
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
    }
}