namespace PoliceClearanceService.Models
{
    public class PoliceClearanceResult
    {
        public int Id { get; set; }
        public string NationalCode { get; set; } = default!;

        public bool HasCriminalRecord { get; set; }

        public string? Description { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
