using Avalonia.Controls;
using System;

namespace Task1RationalNumber.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var numeratorTextBox = this.FindControl<TextBox>("NumeratorTextBox");
        var denominatorTextBox = this.FindControl<TextBox>("DenominatorTextBox");
        int numerator = 0;
        int denominator = 0;

        if (!Int32.TryParse(numeratorTextBox.Text, out numerator)) { 
           
        }


    }
}