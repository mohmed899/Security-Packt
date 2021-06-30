using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            //  throw new NotImplementedException();
            if (plainText.Equals(cipherText))
                return 1;
            int j =0;
            for (int i = 1; i < plainText.Length; i++)
                if (cipherText[1] == plainText[i])
                    for ( j = i; j < plainText.Length; j++)
                    {
                        if (cipherText[i] == plainText[j + 1])
                            continue;
                        else
                            return j;

                    }
             
            return -1;
        }

        public string Decrypt(string cipherText, int key)
        {
            int n = cipherText.Length / key;
            if (cipherText.Length % key != 0)
                n++;
            Console.WriteLine(" n = " + n);
            String result = "";

            for (int i = cipherText.Length; i < n * key; i++)
                cipherText += ' ';


            String[] mat = new string[key];
            for (int i = 0; i < key; i++)
            {
                mat[i] = cipherText.Substring(i * n, n);
                //  Console.WriteLine( cipherText.Substring(i*n,n));
            }
            int j = 0;
            for (int i = 0; i < cipherText.Length; i++)
            {

                result += mat[i % key][j % mat[i % key].Length];
                if (i % key == key - 1)
                    j++;

            }

            for (int i = 0; i < cipherText.Length; i++)
                Console.WriteLine(result[i]);
            String re = result.Trim();
            return re.ToUpper();



        }

        public string Encrypt(string plainText, int key)
        {
            String[] mat = new string[key];
            String result = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                mat[i % key] += plainText[i];
            }
            for (int i = 0; i < key; i++)
            {
                result += mat[i];
            }
            return result.ToUpper();
            }


    }


}
