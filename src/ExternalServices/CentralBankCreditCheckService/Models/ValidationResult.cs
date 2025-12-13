namespace CentralBankCreditCheckService.Models
{
    public class ValidationResult
    {
        public int Id { get; set; }
        public string NationalCode { get; set; } = default!;

        public bool IsValid { get; set; }

        public string? Reason { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
