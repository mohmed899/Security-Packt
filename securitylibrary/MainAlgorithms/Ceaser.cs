using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        private char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public string Encrypt(string plainText, int key)
        {
            char [] cipherText = new char[plainText.Length];
            int charIndex;
            plainText = plainText.ToLower();
            for (int i = 0; i<plainText.Length; i++)
            {
                charIndex = plainText[i]-97;
                cipherText[i] = ((char)(((charIndex + key) % 26) + 97));
            }
            string cipherTextString = new string (cipherText);
            return cipherTextString;
            
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] plainText = new char[cipherText.Length];
            int charIndex;
            cipherText = cipherText.ToLower();
            for (int i = 0; i < cipherText.Length; i++)
            {
                charIndex = cipherText[i] - 97;
                plainText[i] = ((char)(ReversMode(charIndex,key) + 97));
            }
            string plainTextString = new string(plainText);
            return plainTextString;

        }

        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            int plainTextIdx = plainText[0] -97;
            int cipherTextIdx = cipherText[0]-97;
            if(cipherTextIdx - plainTextIdx>=0)
              return cipherTextIdx - plainTextIdx;
            else
            {
                int k = 0;
                while ((plainTextIdx + k) % 26 != cipherTextIdx)
                    k++;
                return k;
            }

        }



    private int ReversMode(int mod , int key)
        {
            int x ;
            int i = 0;
            while (true)
            {
                x = (26 * i) + mod -key;
                if (x >= 0 & x <= 25)
                    break;
                i++;
            }
            
            return x;
        }
    }
}
