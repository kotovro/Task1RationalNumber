using ReactiveUI;
using System;

namespace Task1RationalNumber.ViewModels
{
    public class ReactiveViewModel : ViewModelBase
    {
        public ReactiveViewModel()
        {
            //this.WhenAnyValue(o => o.Numerator)
            //    .Subscribe(o => this.RaisePropertyChanged(nameof(Numerator)));
        }

        private string? _Numerator;

        public string? Numerator 
        {
            get
            {
                return _Numerator;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Numerator, value);
            }
        }
    }
}
