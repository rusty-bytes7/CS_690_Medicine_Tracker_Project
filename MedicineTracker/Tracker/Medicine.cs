namespace Tracker;
public class Medicine{
    public string Name { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }

    public Medicine(string medicine_name, string dosage, string frequency)
    {
        Name = medicine_name;
        Dosage = dosage;
        Frequency = frequency;
    }

}