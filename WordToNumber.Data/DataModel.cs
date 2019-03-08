namespace TextToNumber.Data
{
    public class DataModel
    {
        //Sayısal değer
        public long NumericValue { get; set; }

        //sözel değer
        public string TextValue { get; set; }

        //basamak mı? (yüz, bin, milyon, trilyon, katrilyon)
        public bool IsDigit { get; set; }

        //içerik sayısal mı değil mi?
        public bool IsNumber { get; set; }

        //yüz ise 10^2, bin ise 10^3 ... basamak olan değerler için 10'un kaçıncı kuvveti olduğu
        public short DigitBase { get; set; }

        //metin içinde hangi indexte olduğu
        public int Index { get; set; }
    }
}