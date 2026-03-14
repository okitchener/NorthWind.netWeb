using System.ComponentModel.DataAnnotations;

public class UniqueCompanyNameAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    string companyName = value as string;
    if (string.IsNullOrWhiteSpace(companyName))
    {
      return ValidationResult.Success;
    }

    DataContext db = validationContext.GetService(typeof(DataContext)) as DataContext;
    if (db is null)
    {
      return ValidationResult.Success;
    }

    string normalizedName = companyName.Trim();
    bool companyExists = db.Customers.Any(c => c.CompanyName == normalizedName);

    return companyExists
      ? new ValidationResult("Company name must be unique.")
      : ValidationResult.Success;
  }
}
