using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ParticeCustomer3.Models
{
    internal class MobileFormatCheckAttribute : DataTypeAttribute
    {
        public MobileFormatCheckAttribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            const string regexPattern = @"\d{4}-\d{6}";
            var regex = new Regex(regexPattern);
            var retvalue = regex.IsMatch((string)value);
            return retvalue;
        }
    }
}