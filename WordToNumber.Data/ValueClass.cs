using System.Collections.Generic;

namespace TextToNumber.Data
{
    public static class ValueClass
    {
        private static List<DataModel> dataListTR;

        public static List<DataModel> DataListTR
        {
            get
            {
                //daha önce oluşmamış ise değerler set ediliyor
                return dataListTR ?? (dataListTR = new List<DataModel>
                    {
                        new DataModel {  IsDigit = false, NumericValue = 0, TextValue = "sıfır", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 1, TextValue = "bir", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 2, TextValue = "iki", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 3, TextValue = "üç", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 4, TextValue = "dört", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 5, TextValue = "beş", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 6, TextValue = "altı", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 7, TextValue = "yedi", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 8, TextValue = "sekiz", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 9, TextValue = "dokuz", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 10, TextValue = "on", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 20, TextValue = "yirmi", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 30, TextValue = "otuz", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 40, TextValue = "kırk", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 50, TextValue = "elli", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 60, TextValue = "altmış", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 70, TextValue = "yetmiş", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 80, TextValue = "seksen", IsNumber = true },
                        new DataModel {  IsDigit = false, NumericValue = 90, TextValue = "doksan", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 100, TextValue = "yüz", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 1000, TextValue = "bin", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 1000000, TextValue = "milyon", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 1000000000, TextValue = "milyar", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 1000000000000, TextValue = "trilyon", IsNumber = true },
                        new DataModel {  IsDigit = true, NumericValue = 1000000000000000, TextValue = "katrilyon", IsNumber = true }
                    });
            }
            set
            {
                dataListTR = value;
            }
        }
    }
}