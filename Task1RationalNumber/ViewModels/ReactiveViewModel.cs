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
            Simplify,
            FromDouble
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
                TriggerAllOperationsBindings();
            }
        }


        private string _FromDouble;
        private RationalNumber baseOperand = new RationalNumber();
        private RationalNumber secondOperand = new RationalNumber();
        private string? _BaseOperandNumerator;
        private string? _BaseOperandDenominator;
        private string? _SecondOperandNumerator;
        private string? _SecondOperandDenominator;
        private bool _Enabled = false;

        private const string RegexForInt = "^[-]*?[0-9]+$";
        private const string RegexForDouble = "^^-?\\d+\\.\\d+$";

        

        private void UpdateBaseOperandModel()
        {
            baseOperand.Numerator = Int32.Parse(BaseOperandNumerator);
            baseOperand.Denominator = Int32.Parse(BaseOperandDenominator);
        }
        
        private void UpdateSecondOperandModel()
        {
            secondOperand.Numerator = Int32.Parse(SecondOperandNumerator);
            secondOperand.Denominator = Int32.Parse(SecondOperandDenominator);
        }

        private void TriggerAllOperationsBindings()
        {
            TriggerUnaryOperatorsBindings();
            TriggerBinaryOperationsBindings();
        }

        private void TriggerUnaryOperatorsBindings()
        {
            this.RaisePropertyChanged(nameof(IsToStringVisible));
            this.RaisePropertyChanged(nameof(ToStringText));
            this.RaisePropertyChanged(nameof(IsBaseOperandVisible));
            this.RaisePropertyChanged(nameof(IsFromDoubleVisible));
            this.RaisePropertyChanged(nameof(FromDouble));
        }

        private void TriggerBinaryOperationsBindings()
        {
            this.RaisePropertyChanged(nameof(IsSecondOperandVisible));
            this.RaisePropertyChanged(nameof(IsMultiplyVisible));
            this.RaisePropertyChanged(nameof(Multiply));
            this.RaisePropertyChanged(nameof(IsAddVisible));
            this.RaisePropertyChanged(nameof(Add));
            this.RaisePropertyChanged(nameof(IsSubtractVisible));
            this.RaisePropertyChanged(nameof(Subtract));
        }

        [Required]
        public string? BaseOperandNumerator
        {
            get => _BaseOperandNumerator;
            set
            {
                this.RaiseAndSetIfChanged(ref _BaseOperandNumerator, value);
                if ((!string.IsNullOrWhiteSpace(BaseOperandNumerator) && Regex.IsMatch(BaseOperandNumerator.Trim(), RegexForInt)) 
                    && (!string.IsNullOrWhiteSpace(BaseOperandDenominator) && Regex.IsMatch(BaseOperandDenominator.Trim(), RegexForInt)))
                {
                    IsOperationEnabled = true;
                    UpdateBaseOperandModel();
                }
                else
                {
                    IsOperationEnabled = false;
                }
                TriggerAllOperationsBindings();
            }
        }

        [Required]
        public string? BaseOperandDenominator
        {
            get => _BaseOperandDenominator;
            set
            {
                this.RaiseAndSetIfChanged(ref _BaseOperandDenominator, value);
                if ((!string.IsNullOrWhiteSpace(BaseOperandNumerator) && Regex.IsMatch(BaseOperandNumerator, RegexForInt)) 
                    && (!string.IsNullOrWhiteSpace(BaseOperandDenominator) && Regex.IsMatch(BaseOperandDenominator, RegexForInt)))
                {
                    IsOperationEnabled = true;
                    UpdateBaseOperandModel();
                }
                else
                {
                    IsOperationEnabled = false;
                }
                TriggerAllOperationsBindings();
            }
        }

        [Required]
        public string? SecondOperandNumerator
        {
            get => _SecondOperandNumerator;
            set
            {
                this.RaiseAndSetIfChanged(ref _SecondOperandNumerator, value);
                if ((!string.IsNullOrWhiteSpace(SecondOperandNumerator) && Regex.IsMatch(SecondOperandNumerator.Trim(), RegexForInt))
                    && (!string.IsNullOrWhiteSpace(SecondOperandDenominator) && Regex.IsMatch(SecondOperandDenominator.Trim(), RegexForInt)))
                {
                    UpdateSecondOperandModel();
                }
                TriggerBinaryOperationsBindings();
            }
        }

        [Required]
        public string? SecondOperandDenominator
        {
            get => _SecondOperandDenominator;
            set
            {
                this.RaiseAndSetIfChanged(ref _SecondOperandDenominator, value);
                if ((!string.IsNullOrWhiteSpace(SecondOperandNumerator) && Regex.IsMatch(SecondOperandNumerator, RegexForInt))
                    && (!string.IsNullOrWhiteSpace(SecondOperandDenominator) && Regex.IsMatch(SecondOperandDenominator, RegexForInt)))
                {
                    UpdateSecondOperandModel();
                    TriggerBinaryOperationsBindings();
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
            }
        }

        public bool IsToStringVisible
        {
            get => IsOperationEnabled && SelectedOperation ==  OperationType.ToString;
        }

        public string ToStringText
        {
            get => baseOperand.ToString();
        }

        public bool IsSecondOperandVisible
        {
            get => IsMultiplyVisible || IsAddVisible || IsSubtractVisible;
        }
        public bool IsMultiplyVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.Multiply;
        }

        public string Multiply
        {
            get => (baseOperand * secondOperand).ToString();
        }

        public bool IsAddVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.Add;
        }
        public string Add
        {
            get => (baseOperand + secondOperand).ToString();
        }

        public bool IsSubtractVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.Subtract;
        }

        public string Subtract
        {
            get => (baseOperand - secondOperand).ToString();
        }

        public bool IsBaseOperandVisible
        {
            get => !(SelectedOperation == OperationType.FromDouble);
        }

        public bool IsFromDoubleVisible
        {
            get => IsOperationEnabled && SelectedOperation == OperationType.FromDouble;
        }

        public string FromDouble
        {
            get => _FromDouble;
            set
            {
                this.RaiseAndSetIfChanged(ref _FromDouble, value);
                if (!string.IsNullOrWhiteSpace(FromDouble) && Regex.IsMatch(FromDouble.Trim(), RegexForDouble))    
                {
                    _FromDouble = RationalNumber.FromDouble(value);
                }
            }
            
        }   
        
    }

}
