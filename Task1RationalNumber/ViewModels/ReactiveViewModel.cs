using Avalonia.Data.Converters;
using DynamicData.Diagnostics;
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
                this.RaisePropertyChanged(nameof(IsToStringVisible));
                this.RaisePropertyChanged(nameof(IsMultiplyVisible));
                this.RaisePropertyChanged(nameof(Multiply));
                this.RaisePropertyChanged(nameof(IsAdditionVisible));
                this.RaisePropertyChanged(nameof(Addition));
                this.RaisePropertyChanged(nameof(IsOperandVisible));
            }
        }

        private RationalNumber baseNum = new RationalNumber();
        private RationalNumber operandNum = new RationalNumber();
        private string? _Numerator;
        private string? _Denominator;
        private string? _OperandNumerator;
        private string? _OperandDenominator;

        private const string RegexForInt = "^[-]*?[0-9]+$";

        private bool _Enabled = false;

        private void updateBaseModel()
        {
            baseNum.Numerator = Int32.Parse(Numerator);
            baseNum.Denominator = Int32.Parse(Denominator);
            this.RaisePropertyChanged(nameof(ToStringText));
            if (IsMultiplyVisible)
            {
                this.RaisePropertyChanged(nameof(Multiply));
                
            }
            if (IsAdditionVisible)
            {
                this.RaisePropertyChanged(nameof(Addition));
            }
        }

        private void updateOperandModel()
        {
            operandNum.Numerator = Int32.Parse(OperandNumerator);
            operandNum.Denominator = Int32.Parse(OperandDenominator);
            if (IsMultiplyVisible)
            {
                this.RaisePropertyChanged(nameof(Multiply));
            }
            if (IsAdditionVisible)
            {
                this.RaisePropertyChanged(nameof(Addition));
            }
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
                    updateBaseModel();
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
                    updateBaseModel();
                    IsOperationEnabled = true;
                }
                else
                {
                    IsOperationEnabled = false;
                }
            }
        }

        [Required]
        public string? OperandNumerator
        {
            get => _OperandNumerator;
            set
            {
                this.RaiseAndSetIfChanged(ref _OperandNumerator, value);
                if ((!string.IsNullOrWhiteSpace(OperandNumerator) && Regex.IsMatch(OperandNumerator.Trim(), RegexForInt))
                    && (!string.IsNullOrWhiteSpace(OperandDenominator) && Regex.IsMatch(OperandDenominator.Trim(), RegexForInt)))
                {
                    updateOperandModel();
                }
            }
        }

        [Required]
        public string? OperandDenominator
        {
            get => _OperandDenominator;
            set
            {
                this.RaiseAndSetIfChanged(ref _OperandDenominator, value);
                if ((!string.IsNullOrWhiteSpace(OperandNumerator) && Regex.IsMatch(OperandNumerator, RegexForInt))
                    && (!string.IsNullOrWhiteSpace(OperandDenominator) && Regex.IsMatch(OperandDenominator, RegexForInt)))
                {
                    updateOperandModel();
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
                this.RaisePropertyChanged(nameof(IsToStringVisible));
            }
        }

        public bool IsToStringVisible
        {
            get => IsOperationEnabled && SelectedOperation ==  OperationType.ToString;
        }

        public string ToStringText
        {
            get => baseNum.ToString();
        }

        public bool IsOperandVisible
        {
            get => IsMultiplyVisible || IsAdditionVisible;
        }
        public bool IsMultiplyVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.Multiply;
        }

        public string Multiply
        {
            get => (baseNum * operandNum).ToString();
        }

        public bool IsAdditionVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.Add;
        }
        public string Addition
        {
            get => (baseNum + operandNum).ToString();
        }
    }

}
