namespace Utilities.Dtos
{
    public class ReadStudentResponseDto
    {
        public string RegistrationNumber { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string BirthDate { get; set; }
        public string DateCreated { get; }
        public int Class { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
