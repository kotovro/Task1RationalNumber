using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Task1RationalNumber.Models
{
    class RationalNumber : Object
    {

        public RationalNumber(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                //todo: tgink about passing exceptions to higer levels and how to display them
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            }
            Numerator = numerator;
            Denominator = denominator;
        }

        public RationalNumber(double number, int maxDenominator = 100000) 
        {
            int sign = Math.Sign(number);
            number = Math.Abs(number);
            int a = (int)number;
            double frac = number - a;

            if (frac == 0.0)
            {
                Numerator = sign * a;
                Denominator = 1;
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
                    Numerator = sign * num;
                    Denominator = den;
                }
            }
            Numerator = sign * num1;
            Denominator = den1;
        }

        public RationalNumber()
        {
            Numerator = 0;
            Denominator = 0;
        }
 
        public int Numerator { get; set; }

        public int Denominator { get; set; }

        public static RationalNumber operator +(RationalNumber number) => number;
        public static RationalNumber operator -(RationalNumber fraction) => new RationalNumber(-fraction.Numerator, fraction.Denominator);
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        => new RationalNumber(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        => a + (-b);
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        => new RationalNumber(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (b.Numerator == 0)
            {
                //todo: display exceptioms on higer levels
                throw new DivideByZeroException();
            }
            return new RationalNumber(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public void Reduce()
        {
            //if (this.Denominator == 0)
            //{
            //    throw new ArgumentException("Denominator cannot be zero.");
            //}

            int gcd = GCD(Math.Abs(this.Numerator), Math.Abs(this.Denominator));

            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        public double ToDouble() 
        {
            return Numerator / Denominator;
        }

        public double ToDouble(int precision)
        {
            return double.Round(Numerator / Denominator, precision);
        }

        public override string ToString()
        {
            return $"{Numerator} / {Denominator}"; 
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
