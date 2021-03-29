using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    public class Suspect : ObservableValidator
    {
        private string _name;
        private string _socialSecurityNumber;

        public Suspect()
        {
            ErrorsChanged += Suspect_ErrorsChanged;
            PropertyChanged += Suspect_PropertyChanged;
        }

        ~Suspect()
        {
            ErrorsChanged -= Suspect_ErrorsChanged;
            PropertyChanged -= Suspect_PropertyChanged;
        }

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than one character")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, true);
        }

        [RegularExpression(@"^(?!000)(?!666)(?!9)\d{3}([- ]?)(?!00)\d{2}\1(?!0000)\d{4}$", ErrorMessage = "Invalid Social Security Number.")]
        public string SocialSecurityNumber
        {
            get => _socialSecurityNumber;
            set => SetProperty(ref _socialSecurityNumber, value, true);
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        private void Suspect_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void Suspect_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }
    }
}
