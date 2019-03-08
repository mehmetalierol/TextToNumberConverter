using System;
using System.Collections.Generic;
using System.Linq;
using TextToNumber.Data;
using WordToNumber.Business.Methods;

namespace TextToNumber.Business
{
    public class TextConverter
    {
        private readonly List<DataModel> values;

        public TextConverter()
        {
            values = values ?? ValueClass.DataListTR;
        }

        public string DoConvertion(string sentence)
        {
            var preparedSentence = ControlMethods.CheckContainsNumber(ControlMethods.CheckContainsRealNumber(sentence));

            var preparedList = SumNonDigits(preparedSentence);

            var myList = new List<List<DataModel>>();

            var tempArray = preparedList.ToArray();

            var holder = new List<int>();

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i].IsNumber)
                {
                    if (!holder.Any(x => x == i))
                    {
                        var internalList = new List<DataModel>
                        {
                            new DataModel { DigitBase = tempArray[i].DigitBase, Index = tempArray[i].Index, IsDigit = tempArray[i].IsDigit, IsNumber = tempArray[i].IsNumber, NumericValue = tempArray[i].NumericValue, TextValue = tempArray[i].TextValue }
                        };
                        for (int j = i + 1; j < tempArray.Length; j++)
                        {
                            if (tempArray[j].IsNumber)
                            {
                                internalList.Add(new DataModel { DigitBase = tempArray[j].DigitBase, Index = tempArray[j].Index, IsDigit = tempArray[j].IsDigit, IsNumber = tempArray[j].IsNumber, NumericValue = tempArray[j].NumericValue, TextValue = tempArray[j].TextValue });
                                holder.Add(j);
                            }
                            else
                            {
                                break;
                            }
                        }
                        myList.Add(internalList);
                    }
                }
                else
                {
                    var textList = new List<DataModel>
                    {
                        new DataModel { DigitBase = tempArray[i].DigitBase, Index = tempArray[i].Index, IsDigit = tempArray[i].IsDigit, IsNumber = tempArray[i].IsNumber, NumericValue = tempArray[i].NumericValue, TextValue = tempArray[i].TextValue }
                    };
                    myList.Add(textList);
                }
            }

            var lastString = "";

            foreach (var item in myList)
            {
                var isNumberArray = item.Any(x => x.IsNumber);
                if (isNumberArray)
                {
                    lastString += FirstCalculator(item).ToString() + " ";
                }
                else
                {
                    lastString += item.FirstOrDefault()?.TextValue + " ";
                }
            }

            return lastString;
        }

        public static List<DataModel> SumNonDigits(List<DataModel> list)
        {
            var myNewList = new List<DataModel>();
            var myarray = list.ToArray();
            for (int i = 0; i < myarray.Length; i++)
            {
                if (myarray[i].IsNumber)
                {
                    if (!myarray[i].IsDigit)
                    {
                        if (myarray.Length > i + 1)
                        {
                            if (myarray[i + 1].IsNumber && !myarray[i + 1].IsDigit)
                            {
                                myarray[i + 1].NumericValue += myarray[i].NumericValue;
                                myarray[i + 1].TextValue = myarray[i].TextValue + myarray[i + 1].TextValue;
                            }
                            else
                            {
                                myNewList.Add(new DataModel { Index = myarray[i].Index, IsDigit = myarray[i].IsDigit, IsNumber = myarray[i].IsNumber, NumericValue = myarray[i].NumericValue, TextValue = myarray[i].TextValue });
                            }
                        }
                        else
                        {
                            myNewList.Add(new DataModel { Index = myarray[i].Index, IsDigit = myarray[i].IsDigit, IsNumber = myarray[i].IsNumber, NumericValue = myarray[i].NumericValue, TextValue = myarray[i].TextValue });
                        }
                    }
                    else
                    {
                        myNewList.Add(new DataModel { DigitBase = HelperMethods.GetNumberNth(myarray[i].NumericValue), Index = myarray[i].Index, IsDigit = myarray[i].IsDigit, IsNumber = myarray[i].IsNumber, NumericValue = myarray[i].NumericValue, TextValue = myarray[i].TextValue });
                    }
                }
                else
                {
                    myNewList.Add(new DataModel { Index = myarray[i].Index, IsDigit = myarray[i].IsDigit, IsNumber = myarray[i].IsNumber, NumericValue = myarray[i].NumericValue, TextValue = myarray[i].TextValue });
                }
            }

            return myNewList;
        }

        public long FirstCalculator(List<DataModel> list)
        {
            var arrayList = list.ToArray();
            long sumValue = 0;
            if (list.Any(x => x.DigitBase > 2)) //yüzler basamağından büyük bir basamak var mı?
            {
                if (list.Any(x => x.DigitBase > 3)) //binler basamağından büyük bir basamak var mı?
                {
                    if (list.Any(x => x.DigitBase > 4)) //milyonlar basamağından büyük bir basamak var mı?
                    {
                        if (list.Any(x => x.DigitBase > 5)) //milyarlar basamağından büyük bir basamak var mı?
                        {
                            if (list.Any(x => x.DigitBase > 6)) //trilyonlar basamağından büyük bir basamak var mı?
                            {
                                if (list.Any(x => x.DigitBase > 7)) //katrilyonlar basamağından büyük bir basamak var mı?
                                {
                                    Console.WriteLine("katrilyonlar basamağından büyük basamak var! Uygulama maksimum Katrilyon basamaklı sayıları dönüştürebilir");
                                }
                                else
                                {
                                    sumValue = CalcQuadrillions(arrayList, list);
                                    //Console.WriteLine(sumValue.ToString());
                                }
                            }
                            else
                            {
                                sumValue = CalcTrillions(arrayList, list);
                                //Console.WriteLine(sumValue.ToString());
                            }
                        }
                        else
                        {
                            sumValue = CalcBillions(arrayList, list);
                            //Console.WriteLine(sumValue.ToString());
                        }
                    }
                    else
                    {
                        sumValue = CalcMillions(arrayList, list);
                        //Console.WriteLine(sumValue.ToString());
                    }
                }
                else
                {
                    sumValue = CalcThousands(arrayList, list);
                    //Console.WriteLine(sumValue.ToString());
                }
            }
            else // en fazla yüzler basamağı var ise
            {
                sumValue = CalcHundereds(arrayList, list);
                //Console.WriteLine(sumValue.ToString());
            }
            return sumValue;
        }

        //Yüzler dahil yüzler basamağına kadar olan kısmı hesaplar
        public long CalcHundereds(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (list.Any(x => x.IsDigit)) // içinde yüzler basamağı var mı?
                {
                    if (i == 0 && arrayList[i].IsDigit) // yüz ile başlıyor ise
                    {
                        sumValue = list.Sum(x => x.NumericValue);
                        break;
                    }
                    else // yüzler basamağı 1 den farklı ise
                    {
                        if (i == 0) // bir sonraki adımda çarpılmak üzere pas geçilir
                        {
                            continue;
                        }
                        else
                        {
                            if (i == 1) // yüzler basamağı hesaplanır
                            {
                                sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                            }
                            else if (i == 2) // yüzler basamağından sonraki kısım hesaplanır
                            {
                                if (arrayList.Length > i + 1)
                                {
                                    sumValue += (arrayList[2].NumericValue + arrayList[3].NumericValue);
                                }
                                else
                                {
                                    sumValue += arrayList[2].NumericValue;
                                }
                            }
                        }
                    }
                }
                else // yüzler basamağı yok ise
                {
                    sumValue = list.Sum(x => x.NumericValue);
                    break;
                }
            }
            return sumValue;
        }

        public long CalcThousands(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (i == 0 && arrayList[i].DigitBase == 3) // bin ile başlıyor ise
                {
                    var tempList = list.Where(x => x.DigitBase < 3).ToList();
                    sumValue = 1000 + CalcHundereds(tempList.ToArray(), tempList);
                    break;
                }
                else // binler basamağı 1 den farklı ise
                {
                    if (arrayList[1].DigitBase == 3) //binler basamağı yüz den büyük mü?
                    {
                        var tempList = HelperMethods.GetSubListAfterIndex(list, 1);
                        sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                        sumValue += CalcHundereds(tempList.ToArray(), tempList);
                        break;
                    }
                    else //binler basamağı yüzden büyük ise
                    {
                        if (arrayList[2].DigitBase == 3)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 2);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 2);
                            sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            break;
                        }
                        else if (arrayList[3].DigitBase == 3)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 3);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 3);
                            sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            break;
                        }
                        else
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 4);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 4);
                            sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            break;
                        }
                    }
                }
            }
            return sumValue;
        }

        public long CalcMillions(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (arrayList.Length > 1)
                {
                    if (arrayList[1].DigitBase == 4)
                    {
                        var tempList = HelperMethods.GetSubListAfterIndex(list, 1);
                        sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                        if (list.Any(x => x.DigitBase == 3))
                        {
                            sumValue += CalcThousands(tempList.ToArray(), tempList);
                        }
                        else
                        {
                            sumValue += CalcHundereds(tempList.ToArray(), tempList);
                        }

                        break;
                    }
                    else
                    {
                        if (arrayList[2].DigitBase == 4)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 2);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 2);

                            if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else if (arrayList[3].DigitBase == 4)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 3);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 3);
                            if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 4);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 4);
                            if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                    }
                }
            }
            return sumValue;
        }

        public long CalcBillions(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (arrayList.Length > 1)
                {
                    if (arrayList[1].DigitBase == 5)
                    {
                        var tempList = HelperMethods.GetSubListAfterIndex(list, 1);
                        sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                        if (list.Any(x => x.DigitBase == 4))
                        {
                            sumValue += CalcMillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 3))
                        {
                            sumValue += CalcThousands(tempList.ToArray(), tempList);
                        }
                        else
                        {
                            sumValue += CalcHundereds(tempList.ToArray(), tempList);
                        }

                        break;
                    }
                    else
                    {
                        if (arrayList[2].DigitBase == 5)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 2);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 2);
                            if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else if (arrayList[3].DigitBase == 5)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 3);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 3);
                            if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 4);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 4);
                            if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                    }
                }
            }
            return sumValue;
        }

        public long CalcTrillions(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (arrayList.Length > 1)
                {
                    if (arrayList[1].DigitBase == 6)
                    {
                        var tempList = HelperMethods.GetSubListAfterIndex(list, 1);
                        sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                        if (list.Any(x => x.DigitBase == 5))
                        {
                            sumValue += CalcBillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 4))
                        {
                            sumValue += CalcMillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 3))
                        {
                            sumValue += CalcThousands(tempList.ToArray(), tempList);
                        }
                        else
                        {
                            sumValue += CalcHundereds(tempList.ToArray(), tempList);
                        }

                        break;
                    }
                    else
                    {
                        if (arrayList[2].DigitBase == 6)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 2);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 2);

                            if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else if (arrayList[3].DigitBase == 6)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 3);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 3);
                            if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 4);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 4);
                            if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                    }
                }
            }
            return sumValue;
        }

        public long CalcQuadrillions(DataModel[] arrayList, List<DataModel> list)
        {
            long sumValue = 0;
            for (int i = 0; i < arrayList.Length; i++)
            {
                if (arrayList.Length > 1)
                {
                    if (arrayList[1].DigitBase == 7)
                    {
                        var tempList = HelperMethods.GetSubListAfterIndex(list, 1);
                        sumValue = arrayList[0].NumericValue * arrayList[1].NumericValue;
                        if (list.Any(x => x.DigitBase == 6))
                        {
                            sumValue += CalcTrillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 5))
                        {
                            sumValue += CalcBillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 4))
                        {
                            sumValue += CalcMillions(tempList.ToArray(), tempList);
                        }
                        else if (list.Any(x => x.DigitBase == 3))
                        {
                            sumValue += CalcThousands(tempList.ToArray(), tempList);
                        }
                        else
                        {
                            sumValue += CalcHundereds(tempList.ToArray(), tempList);
                        }

                        break;
                    }
                    else
                    {
                        if (arrayList[2].DigitBase == 7)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 2);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 2);

                            if (list.Any(x => x.DigitBase == 6))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else if (arrayList[3].DigitBase == 7)
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 3);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 3);

                            if (list.Any(x => x.DigitBase == 6))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                        else
                        {
                            var beforeTempList = HelperMethods.GetSubListBeforeIndex(list, 4);
                            sumValue = CalcHundereds(beforeTempList.ToArray(), beforeTempList) * 1000000000000000;
                            var afterTempList = HelperMethods.GetSubListAfterIndex(list, 4);

                            if (list.Any(x => x.DigitBase == 6))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 5))
                            {
                                sumValue += CalcBillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 4))
                            {
                                sumValue += CalcMillions(afterTempList.ToArray(), afterTempList);
                            }
                            else if (list.Any(x => x.DigitBase == 3))
                            {
                                sumValue += CalcThousands(afterTempList.ToArray(), afterTempList);
                            }
                            else
                            {
                                sumValue += CalcHundereds(afterTempList.ToArray(), afterTempList);
                            }

                            break;
                        }
                    }
                }
            }
            return sumValue;
        }
    }
}

//        private static void Main(string[] args)
//        {
//            string testSentence1 = Console.ReadLine();
//            var sentence1 = CheckContainsNumber(CheckContainsRealNumber(testSentence1));
//            //var sentence2 = CheckContainsRealNumber(testSentence1);

//            var test = sumNonDigits(sentence1);

//            var myList = new List<List<ConvertModel>>();

//            var tempArray = test.ToArray();

//            var holder = new List<int>();

//            for (int i = 0; i < tempArray.Length; i++)
//            {
//                if (tempArray[i].IsNumber)
//                {
//                    if (!holder.Any(x => x == i))
//                    {
//                        var internalList = new List<ConvertModel>();
//                        internalList.Add(new ConvertModel { DigitBase = tempArray[i].DigitBase, Index = tempArray[i].Index, IsDigit = tempArray[i].IsDigit, IsNumber = tempArray[i].IsNumber, NumericValue = tempArray[i].NumericValue, TextValue = tempArray[i].TextValue });
//                        for (int j = i + 1; j < tempArray.Length; j++)
//                        {
//                            if (tempArray[j].IsNumber)
//                            {
//                                internalList.Add(new ConvertModel { DigitBase = tempArray[j].DigitBase, Index = tempArray[j].Index, IsDigit = tempArray[j].IsDigit, IsNumber = tempArray[j].IsNumber, NumericValue = tempArray[j].NumericValue, TextValue = tempArray[j].TextValue });
//                                holder.Add(j);
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                        myList.Add(internalList);
//                    }
//                }
//                else
//                {
//                    var textList = new List<ConvertModel>();
//                    textList.Add(new ConvertModel { DigitBase = tempArray[i].DigitBase, Index = tempArray[i].Index, IsDigit = tempArray[i].IsDigit, IsNumber = tempArray[i].IsNumber, NumericValue = tempArray[i].NumericValue, TextValue = tempArray[i].TextValue });
//                    myList.Add(textList);
//                }
//            }

//            var lastString = "";

//            foreach (var item in myList)
//            {
//                var isNumberArray = item.Any(x => x.IsNumber);
//                if (isNumberArray)
//                {
//                    lastString += firstCalculator(item).ToString() + " ";
//                }
//                else
//                {
//                    lastString += item.FirstOrDefault().TextValue + " ";
//                }
//            }

//            Console.WriteLine(lastString);
//        }
//    }
//}