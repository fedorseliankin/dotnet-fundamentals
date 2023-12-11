using System;
using dotnet_fundamentals;

namespace Task2
{
    public class ExceptionNumberParser : IExceptionNumberParser
    {
        public int Parse(string stringValue)
        {
            int result = 0;
            for (int i = 0; i < stringValue.Length; i++)
            {
                if (Char.IsDigit(stringValue[i]))
                {
                    result = result * 10 + stringValue[i] - '0';
                }
                else
                {
                    throw new NotImplementedException("not a digit");
                }
            }
            return result;
        }
    }
}