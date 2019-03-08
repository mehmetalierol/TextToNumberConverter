using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextToNumber.Data;
using WordToNumber.Business.Factory;

namespace WordToNumber.Business.Methods
{
    public static class ControlMethods
    {
        public static DataModel IsStartWithNumber(string text)
        {
            DataModel result = null;
            foreach (var item in ValueClass.DataListTR)
            {
                if (text.ToLower().StartsWith(item.TextValue))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public static DataModel IsStartWithRealNumber(string text)
        {
            var intValues = ValueClass.DataListTR.Where(x => x.NumericValue < 10).ToList();
            DataModel result = null;
            foreach (var item in intValues)
            {
                if (text.ToLower().StartsWith(item.NumericValue.ToString()))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public static string CheckContainsRealNumber(string text)
        {
            long tempLong = 0;
            StringBuilder sb = new StringBuilder();
            var splittedSentence = text.Split(' ');
            for (int i = 0; i < splittedSentence.Length; i++)
            {
                if (ContainsRealDigit(splittedSentence[i]))
                {
                    if (long.TryParse(splittedSentence[i], out tempLong))
                    {
                        var textValue = ConvertNumericValuesFactory.MakeConvertNumericValues().DoConversion(splittedSentence[i]);
                        sb.Append(textValue).Append(" ");
                    }
                }
                else
                {
                    sb.Append(splittedSentence[i]).Append(" ");
                }
            }

            return sb.ToString();
        }

        public static List<DataModel> CheckContainsNumber(string text)
        {
            var numericValues = new List<DataModel>();
            var splittedSentence = text.Split(' ');
            for (int i = 0; i < splittedSentence.Length; i++)
            {
                if (ContainsDigit(splittedSentence[i]))
                {
                    while (ContainsDigit(splittedSentence[i]))
                    {
                        var startWithResult = IsStartWithNumber(splittedSentence[i]);
                        if (startWithResult == null)
                        {
                            var middleText = "";
                            while (IsStartWithNumber(splittedSentence[i]) == null)
                            {
                                middleText += (splittedSentence[i].Substring(0, 1));
                                splittedSentence[i] = (splittedSentence[i].Substring(1, splittedSentence[i].Length - 1));
                            }

                            if (!string.IsNullOrEmpty(middleText))
                            {
                                numericValues.Add(new DataModel { Index = i, IsDigit = false, TextValue = middleText, NumericValue = 0, IsNumber = false });
                            }
                        }
                        else
                        {
                            numericValues.Add(new DataModel { Index = i, IsDigit = startWithResult.IsDigit, TextValue = startWithResult.TextValue, NumericValue = startWithResult.NumericValue, IsNumber = true });
                            var textLen = startWithResult.TextValue.Length;
                            splittedSentence[i] = splittedSentence[i].Substring(textLen, splittedSentence[i].Length - textLen);
                        }
                    }
                    if (splittedSentence[i] != "") numericValues.Add(new DataModel { Index = i, IsDigit = false, TextValue = splittedSentence[i], NumericValue = 0, IsNumber = false });
                }
                else
                {
                    numericValues.Add(new DataModel { Index = i, IsDigit = false, TextValue = splittedSentence[i], NumericValue = 0, IsNumber = false });
                }
            }

            return numericValues;
        }

        public static bool ContainsDigit(string text)
        {
            var result = false;
            foreach (var item in ValueClass.DataListTR)
            {
                if (text.ToLower().Contains(item.TextValue))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static bool ContainsRealDigit(string text)
        {
            var intValues = ValueClass.DataListTR.Where(x => x.NumericValue < 10).ToList();
            var result = false;
            foreach (var item in intValues)
            {
                if (text.ToLower().Contains(item.NumericValue.ToString()))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}