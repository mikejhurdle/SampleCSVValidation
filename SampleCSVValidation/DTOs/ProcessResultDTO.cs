namespace SampleCSVValidation.DTOs
{
    public class ProcessResultDTO
    {
        public string Message { get; set; }
        public string FileName { get; set; }
        public string OriginalFilePath { get; set; }
        public string ValidFilePath { get; set; }
        public string InvalidFilePath { get; set; }
        public int NumberOfValidRows { get; set; }
        public int NumberOfInvalidRows { get; set; }
    }
}