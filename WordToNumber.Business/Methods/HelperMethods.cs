using System.Collections.Generic;
using TextToNumber.Data;

namespace WordToNumber.Business.Methods
{
    public static class HelperMethods
    {
        public static short GetNumberNth(long number)
        {
            switch (number.ToString().Length)
            {
                //onlar
                case 2:
                    return 1;
                //yüzler
                case 3:
                    return 2;
                //binler
                case 4:
                    return 3;
                //milyonlar
                case 7:
                    return 4;
                //milyarlar
                case 10:
                    return 5;
                //trilyonlar
                case 13:
                    return 6;

                case 16:
                    return 7;

                default:
                    return 0;
            }
        }

        public static List<DataModel> GetSubListAfterIndex(List<DataModel> list, int index)
        {
            int i = 0;
            var tempList = new List<DataModel>();
            foreach (var item in list)
            {
                if (i > index)
                {
                    tempList.Add(item);
                }
                i++;
            }
            return tempList;
        }

        public static List<DataModel> GetSubListBeforeIndex(List<DataModel> list, int index)
        {
            int i = 0;
            var tempList = new List<DataModel>();
            foreach (var item in list)
            {
                if (i < index)
                {
                    tempList.Add(item);
                }
                i++;
            }
            return tempList;
        }
    }
}