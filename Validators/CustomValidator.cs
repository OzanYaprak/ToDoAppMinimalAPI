using System.ComponentModel.DataAnnotations;

namespace ToDoAppMinimalAPI.Validators
{
    public class CustomValidator
    {
        public CustomValidator()
        {
        }

        public void Validate<T>(T item)
        {
            var validationResults = new List<ValidationResult>(); // ValidationResult, doğrulama sonuçlarını tutar.
            var context = new ValidationContext(item); // ValidationContext, doğrulama bağlamını tutar.
            var isValid = Validator.TryValidateObject(item, context, validationResults, true); // Validator, doğrulama işlemini yapar. // TryValidateObject, doğrulama işlemini yapar ve sonuçları validationResults listesine ekler. // true parametresi, tüm özelliklerin doğrulanmasını sağlar.

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(select => select.ErrorMessage)); // Doğrulama hatalarını birleştirir.
                throw new ValidationException(errors); // Doğrulama hatalarını içeren bir ValidationException fırlatır.
            }
        }
    }
}
