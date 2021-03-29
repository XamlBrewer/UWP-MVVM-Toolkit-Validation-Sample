using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    public class SuspectWithDelayedValidation : ObservableValidator
    {
        private string _name;
        private string _socialSecurityNumber;

        public SuspectWithDelayedValidation()
        {
            ErrorsChanged += Suspect_ErrorsChanged;
            PropertyChanged += Suspect_PropertyChanged;
        }

        ~SuspectWithDelayedValidation()
        {
            ErrorsChanged -= Suspect_ErrorsChanged;
            PropertyChanged -= Suspect_PropertyChanged;
        }

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than one character")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, false);
        }

        [RegularExpression(@"^(?!000)(?!666)(?!9)\d{3}([- ]?)(?!00)\d{2}\1(?!0000)\d{4}$", ErrorMessage = "Invalid Social Security Number.")]
        public string SocialSecurityNumber
        {
            get => _socialSecurityNumber;
            set => SetProperty(ref _socialSecurityNumber, value, false);
        }

        public ICommand ValidateCommand => new RelayCommand(() => ValidateAllProperties());

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
