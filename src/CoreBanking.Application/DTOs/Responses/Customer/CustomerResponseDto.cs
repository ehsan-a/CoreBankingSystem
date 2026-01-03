namespace CoreBanking.Application.DTOs.Responses.Customer
{
    public class CustomerResponseDto
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
