using System;
using System.Collections.Generic;
using System.Linq;

namespace Translators
{
    public class NumberTranslator
    {
        public string Translate(string numberString)
        {
            int number;
            string returnValue = "";
            if (!int.TryParse(numberString, out number))
            {
                throw new ArgumentException("value entered cannot be converted to a number");
            };


            // var numOfGroups = filledArray.Length / 3

            var filledArray = FillInArray(numberString.ToCharArray());

            while(filledArray.Length != 0)
            {
                returnValue += HandleDigitGrouping(new char[] { filledArray[i], filledArray[i + 1], filledArray[i + 2]});
            }


            // for (var i = 1; i <= numOfGroups; i += 3)
            // {
            //     returnValue += HandleDigitGrouping(new char[] { filledArray[i], filledArray[i + 1], filledArray[i + 2]});
            //     returnValue += GetDigitGroupDescription(1);
            // }

            return returnValue;
        }

        public int GetNumberOfDigitGroups(int arrayLength)
        {
            if (arrayLength % 3 != 0)
            {
                throw new ArgumentException("This should be a modulo of 3");
            }

            // Remove 1 because hundreds ared handled in GetHundreds;
            int returnValue = arrayLength /3;
            
            if(returnValue == 1)
            {
                return returnValue;
            }
            return returnValue - 1;
        }

        public string HandleDigitGrouping(char[] array)
        {
            if (array.Length != 3)
            {
                throw new ArgumentException("This should be 3");
            }

            string returnValue = "";

            if (array[0] != '0')
            {
                returnValue += GetHundreds(array[0]) + " and ";
            }

            return returnValue += GetTens(new char[] { array[1], array[2] });
        }

        public char[] FillInArray(char[] array)
        {
            // Array Length + 3 makes this work with arrays containing less than 3 items
            int arrayLength = array.Length + 3;
            // Convert to list, because arrays blow to work with
            var returnValue = new List<char>();

            if (arrayLength % 3 == 2)
            {
                returnValue.Add('0');
                returnValue.AddRange(array.ToList());
            }
            else if (arrayLength % 3 == 1)
            {
                returnValue.Add('0');
                returnValue.Add('0');
                returnValue.AddRange(array.ToList());
            }
            else
            {
                returnValue.AddRange(array.ToList());
            }

            return returnValue.ToArray();
        }

        public string GetHundreds(char digit)
        {
            var number = int.Parse(digit.ToString());
            return TranslateSingles(number) + " Hundred";
        }

        public string GetTens(char[] digit)
        {

            if (digit.Length > 2)
            {
                throw new ArgumentException("Cannot convert to Tens from less than 2 digits");
            }

            var number = int.Parse(digit[0].ToString() + digit[1].ToString());
            var digit1 = int.Parse(digit[0].ToString());
            var digit2 = int.Parse(digit[1].ToString());
            string space = "";

            if (digit2 != 0) space = " ";

            if (number < 10)
            {
                return TranslateSingles(number);
            }

            if (number < 20)
            {
                return TranslateTeens(number);
            }

            return TranslateSingles(digit1, true) + space + TranslateSingles(digit2);
        }

        public string TranslateSingles(int number, bool tens = false)
        {
            switch (number)
            {
                case 1: return "One";
                case 2: return tens ? "Twenty" : "Two";
                case 3: return tens ? "Thirty" : "Three";
                case 4: return tens ? "Fourty" : "Four";
                case 5: return tens ? "Fifty" : "Five";
                case 6: return tens ? "Sixty" : "Six";
                case 7: return tens ? "Seventy" : "Seven";
                case 8: return tens ? "Eighty" : "Eight";
                case 9: return tens ? "Ninety" : "Nine";
                default: return "";
            }
        }

        public string TranslateTeens(int number)
        {
            switch (number)
            {
                case 10: return "Ten";
                case 11: return "Eleven";
                case 12: return "Twelve";
                case 13: return "Thirteen";
                case 14: return "Fourteen";
                case 15: return "Fifteen";
                case 16: return "Sixteen";
                case 17: return "Seventeen";
                case 18: return "Eighteen";
                case 19: return "Nineteen";
                default: return "";
            }
        }

        public string GetDigitGroupDescription(int number)
        {
            switch (number)
            {
                case 2: return "Thousand ";
                case 3: return "Million ";
                case 4: return "Billion ";
                case 5: return "Trillion ";
                default: return "";
            }
        }
    }
}
