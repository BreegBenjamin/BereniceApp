namespace Berenice.Core.Dtos
{
    public class ApiResponse<T>
    { 
        public T? Response { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}
