using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin_OS
{
    public class MathClass
    {
        public int probAns = 0;

        public string squared(string input)
        {
            string num = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) || input[i] == '.')
                {
                    num += input[i];
                }
            }
            return ((Convert.ToInt32(num) * Convert.ToInt32(num)).ToString());
        }

        public string squareroot(string input)
        {
            string num = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) || input[i] == '.')
                {
                    num += input[i];
                }
            }

            if (num != "")
            {
                return ((Math.Round(Math.Sqrt(Convert.ToDouble(num)), 3)).ToString());
            }else
            {
                return ("No number to find the square root of!");
            }
        }

        public Tuple<string, string> mathProb()
        {
            Random rand = new Random();
            int op = rand.Next(0, 3);
            int firstNum = 0;
            int secondNum = 0;
            string output = "";
            if (op == 0)
            {
                firstNum = rand.Next(0, 100);
                secondNum = rand.Next(0, 100);
                probAns = firstNum + secondNum;
                output = "What is " + firstNum.ToString() + " + " + secondNum.ToString() + "?";
                return Tuple.Create(output, probAns.ToString());
            }
            else if (op == 1)
            {
                firstNum = rand.Next(0, 100);
                secondNum = rand.Next(0, 100);
                probAns = firstNum - secondNum;
                output = "What is " + firstNum.ToString() + " - " + secondNum.ToString() + "?";
                return Tuple.Create(output, probAns.ToString());
            }
            else if (op == 2)
            {
                firstNum = rand.Next(0, 12);
                secondNum = rand.Next(0, 12);
                probAns = firstNum * secondNum;
                output = "What is " + firstNum.ToString() + " * " + secondNum.ToString() + "?";
                return Tuple.Create(output, probAns.ToString());
            }
            else
            {
                while (true)
                {
                    firstNum = rand.Next(0, 12);
                    secondNum = rand.Next(0, 12);
                    if (firstNum % secondNum == 0)
                    {
                        break;
                    }                    
                }
                probAns = firstNum / secondNum;
                output = "What is " + firstNum.ToString() + " / " + secondNum.ToString() + "?";
                return Tuple.Create(output, probAns.ToString());
            }
        }
    }
}