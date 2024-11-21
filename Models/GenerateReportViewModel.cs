namespace PROG_PART_2.Models
{
    public class GenerateReportViewModel
    {
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportType { get; set; }
        public List<Report> ExistingReports { get; set; } = new List<Report>();
    }
}
