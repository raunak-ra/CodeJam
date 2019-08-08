using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodejamCreatingPair
{
 class CreatePairs
    {
        public static int GetNegativeSum(List<int> negativeList, bool isZero)
        {
            negativeList.Sort();
            int sum = 0;
            if (negativeList.Count % 2 == 0)
            {
                for (int i = 0; i < negativeList.Count; i += 2)
                {
                    sum += (negativeList[i] * negativeList[i + 1]);
                }
            }
            else
            {
                for (int i = 0; i < negativeList.Count - 1; i += 2)
                {
                    sum += (negativeList[i] * negativeList[i + 1]);
                }
            }
            if (isZero || (negativeList.Count % 2 == 0))
            {
                return sum;
            }
            return sum + negativeList[negativeList.Count - 1];
        }
        public static int GetPositiveSum(List<int> positiveList)
        {
            positiveList.Sort();
            int sum = 0;
            if (positiveList.Count % 2 == 0)
            {
                for (int i = positiveList.Count - 1; i >= 0; i -= 2)
                {
                    sum += positiveList[i] * positiveList[i - 1];
                }
            }
            else
            {
                for (int i = positiveList.Count - 1; i >= 1; i -= 2)
                {
                    sum += positiveList[i] * positiveList[i - 1];
                }
                sum += positiveList[0];
            }
            return sum;
        }
        int MaximalSum(int[] data)
        {
            int sum = 0;
            bool isZero = false;
            var NegativeList = new List<int>();
            var PositiveList = new List<int>();

 

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 0)
                {
                    NegativeList.Add(data[i]);
                }
                else if (data[i] == 0) isZero = true;
                else if (data[i] == 1) sum++;
                else
                {
                    PositiveList.Add(data[i]);
                }
            }
            if (NegativeList.Count > 0) sum += GetNegativeSum(NegativeList, isZero);
            if (PositiveList.Count > 0) sum += GetPositiveSum(PositiveList);
            if (data.Length == 0)
                Console.WriteLine("Enter More than one Value");
            return sum;
        }
        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            CreatePairs createPairs = new CreatePairs();
            do
            {
                int[] data = Array.ConvertAll<string, int>(input.Split(','), delegate (string s) { return Int32.Parse(s); });
                Console.WriteLine(createPairs.MaximalSum(data));
                input = Console.ReadLine();
            } while (input != "*");
        }
        #endregion
    }
}
