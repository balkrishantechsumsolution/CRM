namespace LeedManagement.Areas.Customer.Models
{
    public class CustomerContact
    {
        public string Id { get; set; }
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobile { get; set; }
        public string ContactType { get; set; }
        public string ContactCategory { get; set; }
        public string IsActive { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CompanyAddress { get; set; }
        public string Remarks { get; set; }
        public string CompanyType { get; set; }
    }
}
