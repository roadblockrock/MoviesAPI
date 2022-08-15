using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Validation
{
    public class RatingAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var intValue = (int?)value;

            return intValue == null || (intValue >= 0 && intValue <= 5);
        }
    }
}
