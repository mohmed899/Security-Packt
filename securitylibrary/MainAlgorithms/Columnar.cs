using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
             int len = 1;
            List<int> newlist = new List<int>();
            int[] array = new int[500];

            List<int> data = new List<int>();
            List<int> num = new List<int>();
            int i = 0;
            while (i < 100)
            {

                int j;
                for (int bb = 0; bb < len; bb++)
                {

                    array[bb] = bb + 1;
                }

                data = prnPermut(array, 0, len - 1, num);
                int counter = 0;
                int rr = 1;

                for (int iv = 1; iv <= len; iv++)
                {
                    rr = rr * iv;
                }

                for (int v = 0; v < rr; v++)
                {


                    for (int cc = 0; cc < len; cc++)
                    {
                        newlist.Add(data[cc + counter]);

                    }
                    counter += len;

                    string res = "";
                    res = Encrypt(plainText, newlist);

                    string res2 = "";
                    if (cipherText.Length % 2 == 1)
                    {
                        for (int ffi = 0; ffi < res.Length; ffi++)
                        {
                            if (res[ffi] != 'x')
                            {
                                res2 += res[ffi];
                            }

                        }
                        res = res2;
                    }
                    if (res.ToUpper() == cipherText.ToUpper())
                    {
                        return newlist;
                    }
                    newlist.Clear();

                }



                len++;
                i++;
                data.Clear();
            }

            return newlist;
            // throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, List<int> key)
        {
          //  String[] mat = new String[key.Count];
          //  List<int> Sortedkey = new List<int>(key);
          ////  Sortedkey.Sort();
          //  int step = NextMultipleOfKey(key.Count, cipherText.Length) / key.Count;
          //  for (int i2 = 0, k3 = 0; i2 < key.Count; i2++)
          //  {
          //      for (int j2 = 0; j2 < step; j2++)
          //      {
          //          mat[Sortedkey[i2] - 1] += cipherText[k3++];

          //      }
          //  }
            int divided2 = NextMultipleOfKey(key.Count, cipherText.Length) / key.Count;
            int dd = (cipherText.Length / key.Max());
            List<char> listChar = new List<char>();
            int rows = dd ;
            if (cipherText.Length % key.Count != 0)
                rows = dd+1;
            int c = 0;
            int x = 0;
            int i = 0;
           for(;i<rows; i++,x++)
            {
                c = x;
                int j = 0;
                while (j < key.Count)
                {

                    if (x <= key.Max())
                    {
                        if (c < cipherText.Length)
                        {
                            listChar.Add(cipherText[c]);
                            c += rows;
                        }
                    }
                    j++;
                }
                
            }
            string res = "";

            i = 0;

            while (i < cipherText.Length)
            {
                int k = 0;
                for (; k < key.Count;)
                {
                    if ((key[k] - 1 + i) > cipherText.Length - 1)
                    
                        break;
                    
                    res += listChar[key[k] - 1 + i];
                    k++;
                }
                i += key.Count;
            }
            return res;


        }

        public string Encrypt(string plainText, List<int> key)
        {
            String[] mat = new String[key.Count];
            String result = "";
            if (plainText.Length % key.Count != 0)
            {
                int newMatSize = NextMultipleOfKey(key.Count, plainText.Length);
                for (int i = plainText.Length; i < newMatSize; i++)
                    plainText += ' ';
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                mat[key[i % key.Count]-1] += plainText[i];
            }
            for (int i = 0; i < key.Count; i++)
            {
                result += mat[i].Trim().ToUpper();
            }
            return result;
          //  throw new NotImplementedException();
        }
        int NextMultipleOfKey(int keySize, int n)
        {
            int c = 1;
            while (n > keySize) {
                keySize *= c;
                c++;
                    }
            return keySize;
        }

        public List<int> prnPermut(int[] list, int h, int n, List<int> num)
        {

            if (h == n)
            {
                for (int i = 0; i <= n; i++)
                {
                    num.Add(list[i]);
                }
            }
            else
                for (int i = h; i <= n; i++)
                {
                    int temp = list[h];
                    list[h] = list[i];
                    list[i] = temp;
                    prnPermut(list, h + 1, n, num);
                    temp = list[h];
                    list[h] = list[i];
                    list[i] = temp;
                }
            return num;
        }
    }


}
