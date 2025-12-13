namespace ProductApi.DTO
{
    public class CustomerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
