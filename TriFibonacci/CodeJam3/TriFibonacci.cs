using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam3
{
    class Fibonacci
    {
        
        public int GetMissingValue(int[] test, int index)
        {
            int MissingValue = 0;
            if(index<3)
            {
                MissingValue = test[3]- (test[0] + test[1] + test[2] + 1);
                test[index] = MissingValue;
                if (!IsTriFibonacci(test, index)) MissingValue = -1;
            }
            else
            {
               
                    MissingValue = test[index - 1] + test[index - 2] + test[index - 3];
                    test[index] = MissingValue;

                if (!IsTriFibonacci(test, index)) MissingValue = -1;
            }

            if (MissingValue <= 0) MissingValue = -1;
            return MissingValue;
        }

        private static bool IsTriFibonacci(int[] test, int index)
        {
              int currentSum = 0;
         
                for (int i = 3; i < test.Length; i++)
                {
                 currentSum = test[i - 1] + test[i - 2] + test[i - 3];
                 if (test[i] != currentSum)
                         return false;
                }          
            return true;
        }
    }
    class TriFibonacci
    {
        public int Complete(int[] test)
        {
            var m = new Fibonacci();
            int index = Array.IndexOf(test, -1);
            return m.GetMissingValue(test, index);
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            TriFibonacci triFibonacci = new TriFibonacci();
            do
            {
                string[] values = input.Split(',');
                int[] numbers = Array.ConvertAll<string, int>(values, delegate (string s) { return Int32.Parse(s); });
                int result = triFibonacci.Complete(numbers);
                Console.WriteLine(result);
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}
