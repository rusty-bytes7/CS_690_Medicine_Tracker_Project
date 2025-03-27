namespace Tracker;

using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello!");
        Console.WriteLine("Please input your name: ");
        string name = Console.ReadLine() ?? "User";
        AnsiConsole.Write(new FigletText($"Welcome, {name}, to the Medicine Tracker!")
        .Centered()
        .Color(Color.Purple_1));

        AnsiConsole.WriteLine("This is a medicine tracker that allows you to track your medicines and their dosages.");
        AnsiConsole.WriteLine("You can add, remove, and view your medicines and their dosages.");
        AnsiConsole.WriteLine("You can also set reminders for your medicines and their dosages, and view your information in a table.");
        AnsiConsole.WriteLine("\n");

        AnsiConsole.WriteLine("What would you like to do?");
        AnsiConsole.WriteLine("1. Add a medicine and dosage\n2. Remove or edit a medicine and dosage\n3. View medicines and dosages\n4. View reminders\n5. View information in a table\n6. Exit");

    }
}
