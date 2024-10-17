namespace PROG_PART_2.Models
{
    public class ErrorViewModel
    {
        // Nullable property to store the request ID for the error
        public string? RequestId { get; set; }

        // Property that returns true if RequestId is not null or empty, used to decide if the RequestId should be displayed
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
