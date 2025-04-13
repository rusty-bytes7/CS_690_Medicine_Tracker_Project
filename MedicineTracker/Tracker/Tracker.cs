namespace Tracker;

using System.IO;
using Spectre.Console;
public class Tracker
{
    private readonly string _filePath;

    //Constructor to initialize the file path
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
    //Method to remove a medication from file
    public void RemoveMedicine(string name)
    {
        string filePathremove = "medicinelist.csv";
        if (!File.Exists(filePathremove))
                    {
                        AnsiConsole.WriteLine("No medicines found to remove or edit.");
                    }
                    else
                    {
                        var remove_medicine_name = AnsiConsole.Prompt(
                            new TextPrompt<string>("Please enter the name of the medication you want to remove (note: editing is not implemented at this time): ")
                        ).ToLower();
                
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
    }
    //Method to add a medicine to the file
    public void AddMedicine(string name, string medicine, string dosage, string frequency)
    {
        using (StreamWriter textfile = File.AppendText(_filePath))
        {
            textfile.WriteLine($"{name}, {medicine}, {dosage}, {frequency}");
        }
    }

    //Method to view all medicines
    public void ViewMedicines()
    {
        if (File.Exists(_filePath))
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("No medicines found.");
        }
    }
}