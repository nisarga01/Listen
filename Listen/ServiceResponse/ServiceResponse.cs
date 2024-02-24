namespace Listen.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string ResultMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
