using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CalendarWithNotes
{

    internal class Program
    {
        public string location = "C:\\Users\\brost\\Desktop\\CalendarNotes\\CalendarNotes.txt";
        public void MonthCal(string month)
        {
            DateTime date = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.CurrentCulture);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            Console.WriteLine(date.ToString("MMMM yyyy") + "\n");
            Console.WriteLine("Su Mo Tu We Th Fr Sa");

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime currentDate = new DateTime(date.Year, date.Month, i);
                if (i == 1)
                {
                    Console.Write(new string(' ', (int)currentDate.DayOfWeek * 3));
                }
                Console.Write($"{i,2} ");
                if (currentDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    Console.WriteLine();
                }
            }
        }
        public void SaveNote(string month, int day, string noteDescription)
        {
            var calendarNote = $"{month}-{day}: {noteDescription}";
            File.AppendAllText(location, calendarNote + Environment.NewLine);
        }
        public void GetNote(string month, int day)
        {
            var calendarNote = $"{month}-{day}";
            if (File.Exists(location))
            {
                var fileContent = File.ReadAllText(location);
                var indexOfNote = fileContent.IndexOf(calendarNote);
                if (indexOfNote >= 0)
                {
                    var fileInfo = fileContent.Substring(indexOfNote);
                    Console.WriteLine(fileInfo);
                }
                else
                {
                    Console.WriteLine("There was no note found!");
                }
            }
        }
        public void ShowMarkedCal(string month)
        {
            DateTime date = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.CurrentCulture);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            Console.WriteLine(date.ToString("MMMM yyyy") + "\n");
            Console.WriteLine("Su Mo Tu We Th Fr Sa");

            for (int day = 1; day <= daysInMonth; day++)
            {
                var containsNote = false;
                DateTime currentDate = new DateTime(date.Year, date.Month, day);

                foreach (string line in File.ReadAllLines(location))
                {
                    if (line.StartsWith($"{month}-{day}:"))
                    {
                        containsNote = true;
                        break;
                    }
                }
                if (day == 1)
                {
                    Console.Write(new string(' ', (int)currentDate.DayOfWeek * 3));
                }
                if (containsNote)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }

                Console.Write($"{day,2} ");
                Console.BackgroundColor = ConsoleColor.Black;

                if (currentDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    Console.WriteLine();
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args)
        {
            var obj = new Program();

            while (true)
            {
                Console.WriteLine("Press 1 for calendar / 2 for taking a note / 3 for finding a note / 4 for calendar with highlighted days");
                var choice = Int32.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter the month name: ");
                    string monthChoice = Console.ReadLine();
                    Console.WriteLine("--------------------");
                    obj.MonthCal(monthChoice);
                    Console.WriteLine("\n");
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Save a note for a specific date: ");
                    Console.WriteLine("Enter the month: ");
                    string month = Console.ReadLine();
                    Console.WriteLine("Enter the day: ");
                    int day = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter your note: ");
                    var noteDescription = Console.ReadLine();
                    obj.SaveNote(month, day, noteDescription);
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter the month: ");
                    string monthNote = Console.ReadLine();
                    Console.WriteLine("Enter the day: ");
                    int dayNote = Int32.Parse(Console.ReadLine());
                    obj.GetNote(monthNote, dayNote);
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Enter the month name: ");
                    string monthChoice = Console.ReadLine();
                    Console.WriteLine("--------------------");
                    obj.ShowMarkedCal(monthChoice);
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("Wrong choice, please try again.");
                }
            }
        }
    }
}
