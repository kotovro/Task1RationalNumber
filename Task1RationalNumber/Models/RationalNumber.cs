using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task1RationalNumber.Models
{
    class RationalNumber
    {

        public RationalNumber(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                //todo: tgink about passing exceptions to higer levels and how to display them
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            }
            _Numerator = numerator;
            _Denominator = denominator;
        }

        public RationalNumber(double number, int maxDenominator = 100000) 
        {
            int sign = Math.Sign(number);
            number = Math.Abs(number);
            int a = (int)number;
            double frac = number - a;

            if (frac == 0.0)
            {
                _Numerator = sign * a;
                _Denominator = 1;
            }

            int num1 = 1, num2 = 0;
            int den1 = 0, den2 = 1;
            int n = a;

            while (true)
            {
                int num = n * num1 + num2;
                int den = n * den1 + den2;

                if (den > maxDenominator)
                {
                    break;
                }

                num2 = num1;
                den2 = den1;
                num1 = num;
                den1 = den;

                frac = 1.0 / (frac - n);
                n = (int)frac;

                if (frac == Math.Floor(frac))
                {
                    num = n * num1 + num2;
                    den = n * den1 + den2;
                    if (den > maxDenominator)
                    {
                        break;
                    }
                    _Numerator = sign * num;
                    _Denominator = den;
                }
            }
            _Numerator = sign * num1;
            _Denominator = den1;
        }

        public RationalNumber()
        {
            this._Numerator = 0;
            this._Denominator = 0;
        }

        private int _Numerator; 
        public int Numerator
        {   
            get { return this.Numerator;  }
            set { this._Numerator = value; } 
        }

        private int _Denominator;
        public int Denominator
        {
            get { return this.Denominator; }
            set { this._Denominator = value; }
        }

        public static RationalNumber operator +(RationalNumber number) => number;
        public static RationalNumber operator -(RationalNumber fraction) => new RationalNumber(-fraction._Numerator, fraction._Denominator);
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        => new RationalNumber(a._Numerator * b._Denominator + b._Numerator * a._Denominator, a._Denominator * b._Denominator);
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        => a + (-b);
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        => new RationalNumber(a._Numerator * b._Numerator, a._Denominator * b._Denominator);

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (b._Numerator == 0)
            {
                //todo: display exceptioms on higer levels
                throw new DivideByZeroException();
            }
            return new RationalNumber(a._Numerator * b._Denominator, a._Denominator * b._Numerator);
        }

        public void Reduce()
        {
            //if (this.Denominator == 0)
            //{
            //    throw new ArgumentException("Denominator cannot be zero.");
            //}

            int gcd = GCD(Math.Abs(this._Numerator), Math.Abs(this._Denominator));

            this._Numerator /= gcd;
            this._Denominator /= gcd;

            if (this._Denominator < 0)
            {
                this._Numerator = -this._Numerator;
                this._Denominator = -this._Denominator;
            }
        }

        public static double toDouble(RationalNumber number) 
        {
            return number._Numerator / number._Denominator;
        }

        public static double toDouble(RationalNumber number, int precision)
        {
            return double.Round(number._Numerator / number._Denominator, precision);
        }

        public override string ToString()
        {
            return _Numerator.ToString() + " / " + _Denominator.ToString(); 
        }

        //todo: maybe refactor and place it in other module
        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
