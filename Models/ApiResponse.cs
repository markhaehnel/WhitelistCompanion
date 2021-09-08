namespace WhitelistCompanion.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get { return string.IsNullOrEmpty(Error); } }
        public string Error { get; init; }
        public T Data { get; init; }
    }
}
