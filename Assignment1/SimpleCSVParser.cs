using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using static Assignment1.Global;

namespace Assignment1
{
    public class SimpleCSVParser
    {
        public void parse(String fileName)
        {
            try 
            {   
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    string[] splitFileName = fileName.Split("\\");
                    
                    string date = String.Format("{0}/{1}/{2}", splitFileName[splitFileName.Length - 2], 
                            splitFileName[splitFileName.Length - 3], splitFileName[splitFileName.Length - 4]);
                    
                    string csvName = splitFileName[splitFileName.Length-1];
                
                    if (!parser.EndOfData)
                    {
                        parser.ReadLine(); // omits the header for each file
                    }

                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        bool flag = true;
                        List<string> data = new List<string>(11);
                    
                        //foreach (string field in fields)
                        for (int i=0; i<fields.Length; i++)
                        {
                            if (flag)
                            {
                                if (string.IsNullOrEmpty(fields[i]))
                                {
                                    string columnName = "";

                                    if (i == 0) {
                                        columnName = "First Name";
                                    }
                                    if (i == 1) {
                                        columnName = "Last Name";
                                    }
                                    if (i == 2) {
                                        columnName  = "Street Number";
                                    }
                                    if (i == 3) {
                                        columnName  = "Street";
                                    }
                                    if (i == 4) {
                                        columnName  = "City";
                                    }
                                    if (i == 5) {
                                        columnName  = "Province";
                                    }
                                    if (i == 6) { 
                                        columnName  = "Postal Code";
                                    }
                                    if (i == 7) { 
                                        columnName  = "Country";
                                    }
                                    if (i == 8) { 
                                        columnName  = "Phone Number";
                                    }
                                    if (i == 9) { 
                                        columnName  = "Email Address";
                                    }

                                    string errorMessage = String.Format("column: {0} value missing in file: {1} for date:{2} at line number: {3}\n", 
                                        columnName, csvName, date, parser.LineNumber);
                                    //System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", errorMessage);
                                    //Commented out logging error message because it increased execution time 1.5 folds
                                    Console.WriteLine(errorMessage);

                                    data.Clear();
                                    flag = false;
                                    Global.invalidRows = Global.invalidRows+1;
                                }
                                else{
                                    data.Add(fields[i]);
                                    if(data.Count == 10)
                                    {
                                        Global.validRows = Global.validRows + 1;
                                        data.Add(date);
                                        string rowData = String.Join(",", data.ToArray());
                                        using (StreamWriter sw = new StreamWriter("../../../../Output/Output.csv", true))
                                        {
                                            sw.WriteLine(rowData);
                                        }
                                    }
                                }
                            }
                        }
                          
                    }

                }
        
            }
            catch (FileNotFoundException)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "The file or directory cannot be found.\n");
            }
            catch (DirectoryNotFoundException)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "The file or directory cannot be found.\n");

            }
            catch (DriveNotFoundException)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "The drive specified in 'path' is invalid.\n");

            }
            catch (PathTooLongException)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "'path' exceeds the maxium supported path length.\n");

            }
            catch (UnauthorizedAccessException)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "You do not have permission to create this file.\n");

            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "There is a sharing violation.\n");

            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", "The file already exists.\n");

            }
            catch (IOException e)
            {
                System.IO.File.AppendAllText(@"../../../../Logs/logs.txt", $"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}\n");
            }
        }

    }
}
