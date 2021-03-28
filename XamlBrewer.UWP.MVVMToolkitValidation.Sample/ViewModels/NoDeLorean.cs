using Microsoft.Toolkit.Mvvm.ComponentModel;
using Mvvm;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    // https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/observablevalidator

    public class NoDeLorean : ObservableValidator
    {
        private DateTime? _startDate = null;
        private DateTime? _endDate = null;
        
        public NoDeLorean()
        {
            ErrorsChanged += NoDelorean_ErrorsChanged;
            PropertyChanged += NoDelorean_PropertyChanged;
        }

        ~NoDeLorean()
        {
            ErrorsChanged -= NoDelorean_ErrorsChanged;
            PropertyChanged -= NoDelorean_PropertyChanged;
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set { SetProperty(ref _startDate, value, true);
                ValidateProperty(EndDate, nameof(EndDate));
            }
        }

        public DateTimeOffset? GetStartDate() => _startDate == null ? (DateTimeOffset?)null : new DateTimeOffset(_startDate.Value);

        public void SetStartDate(DateTimeOffset? dto)
        {
            if (dto == null)
            {
                StartDate = null;
            }

            StartDate = ConvertFromDateTimeOffset(dto.Value);
        }

        [GreaterThan (nameof(StartDate), "End date should come after start date.")]
        public DateTime? EndDate
        {
            get => _endDate;
            set { 
                SetProperty(ref _endDate, value, true);
            }
        }

        public DateTimeOffset? GetEndDate() => _endDate == null ? (DateTimeOffset?)null : new DateTimeOffset(_endDate.Value);

        public void SetEndDate(DateTimeOffset? dto)
        {
            if (dto == null)
            {
                EndDate = null;
            }

            EndDate = ConvertFromDateTimeOffset(dto.Value);
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        private void NoDelorean_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void NoDelorean_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }

        public DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
        }
    }
}
