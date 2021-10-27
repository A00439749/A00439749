using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Assignment1
{

    public static class Global
    {
        public static int validRows = 0; // Unmodifiable
        public static int invalidRows = 0;
    }

    public class MainFile
    {
        public static void Main(String[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Get all the csv files from the given directory and subdirectories
            DirWalker fw = new DirWalker();
            ArrayList fileList = fw.Walk(@"C:\Users\prash\Desktop\Assingment 1\Sample Data");


            //writing headers to the output file
            string headers = "First Name, Last Name, Street Number, Street, City, Province, Country, Postal Code, Phone Number, Email Address, Date\n";
            File.WriteAllTextAsync("../../../../Output/Output.csv", headers);

            //Creating a parser object and passing each file path as parameter
            SimpleCSVParser parser = new SimpleCSVParser();
            foreach (string file in fileList)
            {
                parser.parse(@file);
            }

            //Once all files are processed, we are going to calculate the total time of execution
            stopwatch.Stop();
            System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "\n\nSkipped Rows :" + Global.invalidRows + "\n");
            System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "Valid Rows :" + Global.validRows + "\n");
            System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "Total time of exection(milliseconds) :" + stopwatch.ElapsedMilliseconds.ToString());
        }

    }
}
