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
        public enum OperationType
        {
            ToString,
            Multiply,
            Add,
            Subtract,
            Simplify
        }

        private OperationType _SelectedOperation;
        public ObservableCollection<OperationType> Operations { get; } = new(Enum.GetValues(typeof(OperationType)).Cast<OperationType>());
        public OperationType SelectedOperation
        {
            get
            {
                return _SelectedOperation;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedOperation, value);
                this.RaisePropertyChanged(nameof(VisibleToString));
            }
        }

        private RationalNumber baseNum = new RationalNumber();
        private string? _Numerator;
        private string? _Denominator;
        private const string RegexForInt = "^[-]*?[0-9]+$";

        private bool _Enabled = false;

        private void updateModel()
        {
            baseNum.Numerator = Int32.Parse(Numerator);
            baseNum.Denominator = Int32.Parse(Denominator);
            this.RaisePropertyChanged(nameof(ToStringText));
        }


        [Required]
        public string? Numerator
        {
            get => _Numerator;
            set
            {
                this.RaiseAndSetIfChanged(ref _Numerator, value);
                if ((!string.IsNullOrWhiteSpace(Numerator) && Regex.IsMatch(Numerator.Trim(), RegexForInt)) 
                    && (!string.IsNullOrWhiteSpace(Denominator) && Regex.IsMatch(Denominator.Trim(), RegexForInt)))
                {
                    updateModel();
                    IsOperationEnabled = true;
                }
                else
                {
                    IsOperationEnabled = false;
                }
            }
        }

        [Required]
        public string? Denominator
        {
            get => _Denominator;
            set
            {
                this.RaiseAndSetIfChanged(ref _Denominator, value);
                if ((!string.IsNullOrWhiteSpace(Numerator) && Regex.IsMatch(Numerator, RegexForInt)) 
                    && (!string.IsNullOrWhiteSpace(Denominator) && Regex.IsMatch(Denominator, RegexForInt)))
                {
                    updateModel();
                    IsOperationEnabled = true;
                }
                else
                {
                    IsOperationEnabled = false;
                }
            }
        }

        public bool IsOperationEnabled
        {
            get => _Enabled;
            set
            {
                _Enabled = value;
                this.RaisePropertyChanged(nameof(IsOperationEnabled));
                this.RaisePropertyChanged(nameof(VisibleToString));

            }
        }

        public bool VisibleToString
        {
            get => IsOperationEnabled && SelectedOperation ==  OperationType.ToString;
        }

        public string ToStringText
        {
            get => baseNum.ToString();
        }
    }

}
