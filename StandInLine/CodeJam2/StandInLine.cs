using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam2
{
    class StandInLine
    {
        int[] Reconstruct(int[] left)
        {
            int[] FinalArrangement = new int[left.Length];
            int RemainingPosition, Position;
           
            for (int i = 0; i < left.Length; i++)
            {
                RemainingPosition = -1;
                Position = 0;
                while (RemainingPosition < left[i])
                {
                    if (FinalArrangement[Position++] == 0)
                        RemainingPosition++;
                }
                FinalArrangement[Position - 1] = i + 1;
            }
            return FinalArrangement;
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            StandInLine standInLine = new StandInLine();
            do
            {
                int[] left = Array.ConvertAll<string, int>(input.Split(','), delegate (string s) { return Int32.Parse(s); });
                Console.WriteLine(string.Join(",", Array.ConvertAll<int, string>(standInLine.Reconstruct(left), delegate (int s) { return s.ToString(); })));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}
