namespace Application.ViewModels
{
    public class ApplicationUserViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Fullname => $"{FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public string StreetNameAndNumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; } // Added PhoneNumber
    public EmergencyDetailsViewModel EmergencyDetails { get; set; }
    public InsuranceDetailsViewModel InsuranceDetails { get; set; }
}
}
