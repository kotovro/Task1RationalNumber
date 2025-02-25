using Avalonia.Data.Converters;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
            ToString,
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
                if ((!string.IsNullOrWhiteSpace(Numerator) && Regex.IsMatch(Numerator, "^[0-9]+$")) 
                    && (!string.IsNullOrWhiteSpace(Denominator) && Regex.IsMatch(Denominator, "^[0-9]+$")))
                {
                    updateModel(Numerator, Denominator);
                    Enabled = true;
                    Visible = true;
                    this.RaisePropertyChanged(nameof(Enabled));
                    //this.RaisePropertyChanged(nameof(Visible));
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
                if ((!string.IsNullOrWhiteSpace(Numerator) && Regex.IsMatch(Numerator, "^[0-9]+$")) 
                    && (!string.IsNullOrWhiteSpace(Denominator) && Regex.IsMatch(Denominator, "^[0-9]+$")))
                {
                    _Enabled = true;
                    updateModel(Numerator, Denominator);
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
                _Enabled = value;
                Visible = value;
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
                _Visible = value;
                this.RaisePropertyChanged(nameof(Visible));
            }
        }
    }

}
