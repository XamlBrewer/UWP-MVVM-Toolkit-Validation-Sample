using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    public class Countdown : ObservableValidator, IValidatableObject
    {
        public Countdown()
        {
            ErrorsChanged += Countdown_ErrorsChanged;
            PropertyChanged += Countdown_PropertyChanged;
        }

        ~Countdown()
        {
            ErrorsChanged -= Countdown_ErrorsChanged;
            PropertyChanged -= Countdown_PropertyChanged;
        }

        private int _value = 10;
        private int _previousValue;

        [CustomValidation(typeof(Countdown), nameof(ValidateValue))]
        public int Value
        {
            get => _value;
            set
            {
                _previousValue = _value;
                SetProperty(ref _value, value, true);
            }
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        private void Countdown_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void Countdown_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }

        public static ValidationResult ValidateValue(int value, ValidationContext context)
        {
            var instance = (Countdown)context.ObjectInstance;
            var isValid = value < instance._previousValue;

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("We're not supposed to count up.");
        }
    }
}
