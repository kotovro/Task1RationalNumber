using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.Contracts;


namespace Task1RationalNumber.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public ReactiveViewModel ReactiveViewModel { get; } = new ReactiveViewModel();
    }
}
