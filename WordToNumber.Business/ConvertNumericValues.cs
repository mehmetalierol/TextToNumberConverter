using System;

namespace WordToNumber.Business
{
    public class ConvertNumericValues
    {
        private readonly string[] Hundereds = new string[10];
        private readonly string[] Tens = new string[10];
        private readonly string[] FirstParts = new string[10];
        private readonly string[] Digits = new string[6];
        private readonly string[] Numbers = new string[6];

        public ConvertNumericValues()
        {
            Hundereds.SetValue("dokuzyüz", 9);
            Hundereds.SetValue("sekizyüz", 8);
            Hundereds.SetValue("yediyüz", 7);
            Hundereds.SetValue("altıyüz", 6);
            Hundereds.SetValue("beşyüz", 5);
            Hundereds.SetValue("dörtyüz", 4);
            Hundereds.SetValue("üçyüz", 3);
            Hundereds.SetValue("ikiyüz", 2);
            Hundereds.SetValue("yüz", 1);
            Hundereds.SetValue("", 0);

            Tens.SetValue("doksan", 9);
            Tens.SetValue("seksen", 8);
            Tens.SetValue("yetmiş", 7);
            Tens.SetValue("altmış", 6);
            Tens.SetValue("elli", 5);
            Tens.SetValue("kırk", 4);
            Tens.SetValue("otuz", 3);
            Tens.SetValue("yirmi", 2);
            Tens.SetValue("on", 1);
            Tens.SetValue("", 0);

            FirstParts.SetValue("dokuz", 9);
            FirstParts.SetValue("sekiz", 8);
            FirstParts.SetValue("yedi", 7);
            FirstParts.SetValue("altı", 6);
            FirstParts.SetValue("beş", 5);
            FirstParts.SetValue("dört", 4);
            FirstParts.SetValue("üç", 3);
            FirstParts.SetValue("iki", 2);
            FirstParts.SetValue("bir", 1);
            FirstParts.SetValue("", 0);

            Digits.SetValue("", 0);
            Digits.SetValue("", 1);
            Digits.SetValue("", 2);
            Digits.SetValue("", 3);
            Digits.SetValue("", 4);
            Digits.SetValue("", 5);
        }

        public string DoConversion(string text)
        {
            int NumberLenght = text.Length;
            if (NumberLenght > 18)
                return "Hata girilen değerin uzunluğu en fazla 15 olmalı";
            // uzunluk 15 karakterden fazla olmamalı. si
            try
            {
                long k = Convert.ToInt64(text);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            text = "000000000000000000" + text;
            text = text.Substring(NumberLenght, 18);
            Numbers.SetValue(text.Substring(0, 3), 0);
            Numbers.SetValue(text.Substring(3, 3), 1);
            Numbers.SetValue(text.Substring(6, 3), 2);
            Numbers.SetValue(text.Substring(9, 3), 3);
            Numbers.SetValue(text.Substring(12, 3), 4);
            Numbers.SetValue(text.Substring(15, 3), 5);
            if (Numbers[0] != "000")
                Digits.SetValue("kattrilyon ", 0);
            if (Numbers[1] != "000")
                Digits.SetValue("trilyon ", 1);
            if (Numbers[2] != "000")
                Digits.SetValue("milyar ", 2);
            if (Numbers[3] != "000")
                Digits.SetValue("milyon ", 3);
            if (Numbers[4] != "000")
                Digits.SetValue("bin ", 4);
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result = result + Hundereds[Convert.ToInt16(Numbers[i][0].ToString())] +
                FixFirstPart(Tens[Convert.ToInt16(Numbers[i][1].ToString())] + FirstParts[Convert.ToInt16(Numbers[i][2].ToString())] + Digits[i]);
            }
            return result;
        }

        private string FixFirstPart(string problem)
        {
            string solution = problem == "birbin " ? "bin " : problem;
            return solution;
        }
    }
}