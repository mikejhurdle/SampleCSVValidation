using CsvHelper;
using CsvHelper.Configuration;
using SampleCSVValidation.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace SampleCSVValidation.AppServices
{
    public class FileServices
    {
        public ProcessResultDTO ProcessFile(string originalFileName, string filePath, int columnCount, string format)
        {
            // Using the data transfer object as it allows me more flexibility going forward around what I return as a result
            // Also moves me towards saving the data if we had storage
            var result = new ProcessResultDTO()
            {
                Message = "",
                FileName = originalFileName,
                OriginalFilePath = "ArchivedFiles/" + originalFileName,
                ValidFilePath = "ValidRecords/" + originalFileName,
                InvalidFilePath = "InvalidRecords/" + originalFileName,
                NumberOfInvalidRows = 0,
                NumberOfValidRows = 0,
            };
            try
            {
                // Set config for the CSV Reader
                var config = new Configuration();
                // Default is comma seperated, so only need to change it if we are doing TAB
                if (format == "TAB")
                {
                    config.Delimiter = "\t";
                };

                config.HasHeaderRecord = false;
                // DTOs for writing back out to file once done
                var validRecords = new FileDTO();
                validRecords.Rows = new List<dynamic>();

                var invalidRecords = new FileDTO();
                invalidRecords.Rows = new List<dynamic>();

                // Streaming then utilizing the CSVHelper CSVReader
                using (var fileReader = new StreamReader(filePath))
                using (var readFromCSV = new CsvReader(fileReader, config))
                {
                    // get all records (includes the header)
                    var records = readFromCSV.GetRecords<dynamic>();
            
                    // index for filtering header out of results
                    int recordIdx = 0;

                    foreach (var record in records)
                    {
                        if (recordIdx == 0)
                        {
                            // Do nothing. The assumption is the files have header rows
                            // and we ignore those. Setting the Header = false in the CSV Helper config 
                            // does not handle this, as it then bases the length of each row solely 
                            // on the header values, which then means we can't track extra values easily
                        }
                        else
                        {
                            var newRow = new FileRowDTO();
                            newRow.Values = new List<string>();
                            foreach (var column in record)
                            {
                                if (column.Value != null)
                                {
                                    newRow.Values.Add(column.Value);
                                }
                            }

                            if (newRow.Values.Count == columnCount)
                            {
                                validRecords.Rows.Add(record);
                            }
                            else if (newRow.Values.Count != columnCount)
                            {
                                invalidRecords.Rows.Add(record);
                            }
                        }

                        recordIdx++;
                    }
                }

                // Only create file if there are records to write
                if (invalidRecords.Rows.Count > 0)
                {
                    //save count to result dto
                    result.NumberOfInvalidRows = invalidRecords.Rows.Count;
                    WriteRecordsToFile(invalidRecords, originalFileName, format, "InvalidRecords");
                }

                // Only create file if there are records to write
                if (validRecords.Rows.Count > 0)
                {
                    //save count to result dto
                    result.NumberOfValidRows = validRecords.Rows.Count;
                    WriteRecordsToFile(validRecords, originalFileName, format, "ValidRecords");
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public bool WriteRecordsToFile(FileDTO lstRows, string originalFileName, string format, string folder)
        {
            try
            {
                var config = new Configuration();
                if (format == "TAB")
                {
                    config.Delimiter = "\t";
                };
                config.HasHeaderRecord = false;
                string fileName = originalFileName;
                string validPath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + folder), fileName);

                using (var writer = new StreamWriter(validPath))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(lstRows.Rows);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string ArchiveFile(HttpPostedFileBase file)
        {
            try
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/ArchivedFiles"), fileName);
                file.SaveAs(path);
                return path;
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }

        }

        public void DoThings()
        {
            DocumentStore documentStore = new DocumentStore(0);
            documentStore.AddDocument("item");
            Console.WriteLine(documentStore);
        }

        public class DocumentStore
        {
            private List<string> _documents = new List<string>();
            private int _capacity;


            public DocumentStore(int capacity)
            {
                Capacity = capacity;
            }

            public int Capacity { get { return _capacity; } private set { _capacity = value; } }
            public List<string> Documents { get { return _documents; } private set { _documents = value; } }

            public void AddDocument(string document)
            {
                if ((Documents.Count + 1) > Capacity)
                    throw new InvalidOperationException();

                Documents.Add(document);
            }

            public override string ToString()
            {
                var count = Documents.Count.ToString();
                var capacity = Capacity;
                return $"Document store: " + count + "/" + capacity.ToString();
            }
        }
    }
}