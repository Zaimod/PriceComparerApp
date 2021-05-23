using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PriceComparerApp.Helpers.Validatiors
{
    public class ValidatableObject<T> : IValidatable<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<IValidationRule<T>> Validations => new List<IValidationRule<T>>();
        public List<string> Errors { get; set; } = new List<string>();
        public bool CleanOnChange { get; set; } = true;

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                if (CleanOnChange)
                    IsValid = true;
            }
        }

        public bool IsValid { get; set; } = true;
    
        public virtual bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
