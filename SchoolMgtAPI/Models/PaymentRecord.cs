namespace Models
{
    public class PaymentRecord
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string StudentRegistrationNumber { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string Avatar { get;set; }
        public string StudentEmail { get; set; }
        public string StudentDepartment { get; set; }   
        public int StudentLevel { get; set; }
        public string PaymentType { get; set; }
        public int Amount { get; set; }
        public string TransactionReference { get; set; }
        public bool IsApproved { get; set; }
        public string DateCreated { get; set; }
    }
}
