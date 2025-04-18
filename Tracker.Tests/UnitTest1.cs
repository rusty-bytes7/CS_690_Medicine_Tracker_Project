using System.IO;
using Xunit;

namespace Tracker.Tests
{
    public class TrackerTests
    {
        [Fact]
        public void AddMedicine_ShouldWorkCorrectly()
        {
            
            var tracker = new Tracker("test_medicines.csv");
            tracker.AddMedicine("User","Test Medicine", "100mg", "Twice a day");

            //make sure medicine file exists
            Assert.True(File.Exists("test_medicines.csv"));

            // Check if the medicine was added correctly
            var lines = File.ReadAllLines("test_medicines.csv");
            Assert.Contains("User, Test Medicine, 100mg, Twice a day", lines);
            // Clean up
            File.Delete("test_medicines.csv");
        }

        [Fact]
        public void RemoveMedicine_ShouldWork()
        {
            //this is going to get hung up because it requires user input- change for final version
        }

        [Fact]
        public void EditMedicine_ShouldWork()
        {
            //this is going to get hung up because it requires user input- change for final version
        }

        [Fact]
        public void AddReminder_ShouldWork()
        {
            var reminder = new Reminder("Test Medicine", "1200");
            
            reminder.AddReminder("Test Medicine", "1200");
            //check if the reminder file exists
            Assert.True(File.Exists("reminderlist.csv"));
            //check that reminder was added
            var lines = File.ReadAllLines("reminderlist.csv");
            Assert.Contains("Test Medicine, 1200", lines);
            //clean up
            File.Delete("reminderlist.csv");
        }

        [Fact]
        public void MakeTable_ShouldWork()
        {
            //make test file of medicines
            var tracker = new Tracker("test_medicines.csv");
            tracker.AddMedicine("User","Test Medicine", "100mg", "Twice a day");
            tracker.AddMedicine("User","Test Medicine2", "200mg", "Once a day");
            tracker.AddMedicine("User","Test Medicine3", "300mg", "Three times a day");

            //make table
            var tableMaker = new TableMaker("test_medicines.csv");
            var table = tableMaker.CreateTable();
            //check if the table was created
            Assert.NotNull(table);
        }
        


        
    }
}
