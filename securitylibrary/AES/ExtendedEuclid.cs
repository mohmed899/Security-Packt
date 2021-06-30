using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
              int[] arr = { 0, 1, 0, baseN };
              int[] arr2 = { 0, 0, 1, number };
            while (true)
            {

                switch (arr2[3])
                {
                    case 1:
                        arr2[2] %= baseN;
                        arr2[2] += baseN;
                        arr2[2] %= baseN;
                        return arr2[2];
                    case 0:
                        return -1;
                }
                double q = arr[3] / arr2[3];
                double[] D = { 0, (arr[1] - (q * arr2[1])), (arr[2] - (q * arr2[2])), (arr[3] - (q * arr2[3])) };
                for (int i = 1; i < 4; i++)
                {
                    arr[i] = arr2[i];
                    arr2[i] = (int)D[i];
                }
            }
        }
    }
}
