using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Validation
{
    public class PositiveNumberAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var intValue = (int?) value;
            
            return intValue == null || intValue >= 0;
        }
    }
}
