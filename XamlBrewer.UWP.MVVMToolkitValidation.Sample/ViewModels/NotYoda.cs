using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    public class NotYoda : ObservableValidator
    {
        private string _name;
        private string _socialSecurityNumber;
        private List<ValidationResult> _errors = new List<ValidationResult>();

        public NotYoda()
        {
        }

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than one character")]
        public string Name
        {
            get => _name;
            set
            {
                _errors.RemoveAll(v => v.MemberNames.Contains(nameof(Name)));

                TrySetProperty(ref _name, value, out IReadOnlyCollection<ValidationResult> errors);

                _errors.AddRange(errors);
                OnPropertyChanged(nameof(Errors));
                OnPropertyChanged(nameof(HasErrors2));
            }
        }

        [RegularExpression(@"^(?!000)(?!666)(?!9)\d{3}([- ]?)(?!00)\d{2}\1(?!0000)\d{4}$", ErrorMessage = "Invalid Social Security Number.")]
        public string SocialSecurityNumber
        {
            get => _socialSecurityNumber;
            set
            {
                _errors.RemoveAll(v => v.MemberNames.Contains(nameof(SocialSecurityNumber)));

                TrySetProperty(ref _socialSecurityNumber, value, out IReadOnlyCollection<ValidationResult> errors);

                _errors.AddRange(errors);
                OnPropertyChanged(nameof(Errors));
                OnPropertyChanged(nameof(HasErrors2));
            }
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in _errors select e.ErrorMessage);

        public bool HasErrors2 => Errors.Length > 0;
    }
}
