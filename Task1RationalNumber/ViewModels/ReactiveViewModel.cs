using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Task1RationalNumber.Models;

namespace Task1RationalNumber.ViewModels
{
    public class ReactiveViewModel : ViewModelBase
    {
        public ReactiveViewModel()
        {
            
        }
        public enum Status
        {
            Convert_to_string,
            Multiply,
            Add,
            Subtract,
            Simplify
        }

        private Status _SelectedStatus;
        public ObservableCollection<Status> Statuses { get; } = new(Enum.GetValues(typeof(Status)).Cast<Status>());
        public Status SelectedStatus
        {
            get
            {
                return _SelectedStatus;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedStatus, value);
            }
        }

        private RationalNumber firstNum = new RationalNumber();
        private string? _Numerator;
        private string? _Denominator;
        private bool _Enabled = false;
        private bool _Visible = false; 

        private void updateModel(string num, string denom)
        {
            firstNum.Numerator = Int32.Parse(num);
            firstNum.Denominator = Int32.Parse(denom);
        }

        [Required]
        public string? Numerator
        {
            get
            {
                return _Numerator;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Numerator, value);
                if ((_Numerator is not null && Regex.IsMatch(_Numerator, "^[0-9]+$")) 
                    && (_Denominator is not null && Regex.IsMatch(_Denominator, "^[0-9]+$")))
                {
                    _Enabled = true;
                    updateModel(_Numerator, _Denominator);
                    this.RaisePropertyChanged(nameof(Enabled));
                }
                else
                {
                    _Enabled = false;
                    this.RaisePropertyChanged(nameof(Enabled));
                }
            }
        }
        [Required]
        public string? Denominator
        {
            get
            {
                return _Denominator;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Denominator, value);
                if ((_Numerator is not null && Regex.IsMatch(_Numerator, "^[0-9]+$")) 
                    && (_Denominator is not null && Regex.IsMatch(_Denominator, "^[0-9]+$")))
                {
                    _Enabled = true;
                    updateModel(_Numerator, _Denominator);
                    this.RaisePropertyChanged(nameof(Enabled));
                }
                else
                {
                    _Enabled = false;
                    this.RaisePropertyChanged(nameof(Enabled));
                }

            }
        }

        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Enabled, value);
                _Enabled = true;
            }
        }

        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Visible, value);
                _Visible = true;
            }
        }
    }

}
