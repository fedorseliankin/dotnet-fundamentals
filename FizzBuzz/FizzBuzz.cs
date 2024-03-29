﻿using System;
namespace FizzBuzz
{
	public class FizzBuzz
	{
        public static string GetFizzBuzz(int number)
        {
            if (number < 1 || number > 100)
                throw new ArgumentOutOfRangeException("number", "Only numbers from 1 to 100 are allowed");

            if (number % 3 == 0 && number % 5 == 0)
                return "FizzBuzz";
            else if (number % 3 == 0)
                return "Fizz";
            else if (number % 5 == 0)
                return "Buzz";
            else
                return number.ToString();
        }
    }
}

