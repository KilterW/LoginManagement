using LoginManagement.DataAccess.DataEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace LoginManagement.Common
{
    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Course course = (value as BindingGroup).Items[0] as Course;
            if (course.StartDate>course.EndDate)
            {
                return new ValidationResult(false, "Start Date must be earlier than End Date.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
