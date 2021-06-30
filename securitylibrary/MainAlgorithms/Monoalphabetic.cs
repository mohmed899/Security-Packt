using System;
using System.Collections.Generic;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            //  throw new NotImplementedException();
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            char[] Key = new char[26];

            for (int i = 0; i < cipherText.Length; i++)
                Key[plainText[i] - 97] = cipherText[i];
            string KeyString = new string(Key);
            for (int i = 0; i < 26; i++)
            {
                int j = 97;
                if (Key[i] == 0)
                    for (int x = 0; x < 26; x++)
                    {
                        char c = (char)j;
                        if (!FindChar(c, Key))
                        {
                            Key[i] = c;
                            //KeyString.Insert(i, c.ToString());
                            break;
                        }
                        j++;
                    }



            }
            //defghijklmnopqrstuvwxyzabc
            // defghijklmnopqrstuvwxyzabc
            string KeyString2 = new string(Key);
            return KeyString2;

        }

        public string Decrypt(string cipherText, string key)
        {
            // throw new NotImplementedException();
            char[] plainText = new char[cipherText.Length];
            cipherText = cipherText.ToLower();
            for (int i = 0; i < cipherText.Length; i++)
                plainText[i] = (char)(key.IndexOf(cipherText[i]) + 97);
            string plainTextString = new string(plainText);
            return plainTextString;


        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            char[] cipherText = new char[plainText.Length];
            // int charIndex;
            plainText = plainText.ToLower();
            for (int i = 0; i < plainText.Length; i++)

                cipherText[i] = key[plainText[i] - 97];
            string cipherTextString = new string(cipherText);
            return cipherTextString;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            char[] Frequency = { 'e', 't', 'a', 'o','i','n','s','r','h','l','d','c','u','m','f','p','g','w','y','b','v','k','x','j','q','z' };
            cipher = cipher.ToLower();
            String plainText = "";
            List<Tuple<char, int>> cipherProprites = new List<Tuple<char, int>>();
            for (int i = 0; i < 26; i++)
                cipherProprites.Add(Tuple.Create(((char)(i + 97)), NumberOfRepeated(cipher, (char)(i + 97))));
                cipherProprites.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            
            for (int i =0; i< cipher.Length; i++)
            {
                plainText += Frequency[findIdex(cipherProprites, cipher[i])];
               
            }


            return plainText;
        }

        private bool FindChar(char c, char[] mat)
        {
            for (int i = 0; i < 26; i++)
                if (mat[i] == c)
                    return true;
            return false;
        }
        private int NumberOfRepeated(String str, char x)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == x)
                    count++;
            return count;
        }


        int findIdex(List<Tuple<char, int>> list,char c)
        {
            for( int i =0; i<26;i++)
            {
                if (list[i].Item1 == c)
                    return i;
            }
            return 0;
        }


    }

}
