using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PriceComparerApp.Helpers.Validatiors.Rules
{
    public class IsValidPasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexPassword { get; set; } = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])?[A-Za-z\d$@$!%*#?&]{8,30}$");

        public bool Check(T value)
        {
            return (RegexPassword.IsMatch($"{value}"));
        }
    }
}
