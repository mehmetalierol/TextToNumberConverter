namespace WordToNumber.Business.Factory
{
    public static class ConvertNumericValuesFactory
    {
        public static ConvertNumericValues _convertNumericValues { get; set; }

        public static ConvertNumericValues MakeConvertNumericValues()
        {
            if (_convertNumericValues == null)
            {
                _convertNumericValues = new ConvertNumericValues();
            }

            return _convertNumericValues;
        }
    }
}