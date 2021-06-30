using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        int[,] tableArr = new int[26, 26];
        //RepeatingkeyVigenere p = new RepeatingkeyVigenere();
        void createTable()
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    int temp;
                    if ((i + 97) + j > 122)
                    {
                        temp = ((i + 97) + j) - 26;
                        tableArr[i, j] = temp;
                    }
                    else
                    {
                        temp = (i + 97) + j;

                        char c = (char)temp;
                        tableArr[i, j] = temp;
                    }
                }
            }
        }
        string Mappingkey(string txt, string key)
        {
            string keyMap = "";
            for (int i = 0, j = 0 , k=0; i < txt.Length; i++)
            {
                if (txt[i] == 32)
                {
                    keyMap += 32;
                }
                else
                {
                    if (j < key.Length)
                    {
                        keyMap += key[j];
                        j++;
                    }
                    else
                    {
                        
                        keyMap += txt[k];
                        k++;
                    }
                }
            }

            return keyMap;
        }
        int itrCount(int key, int ctxt)
        {
            if (ctxt < 97 && ctxt > 32)
                ctxt = ctxt + 32;
            if (key < 97 && key > 32)
                key = key + 32;
            int counter = 0;
            string result = "";
            for (int i = 0; i < 26; i++)
            {
                if (key + i > 122)
                {
                    result += (char)(key + (i - 26));
                }
                else
                {
                    result += (char)(key + i);
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == ctxt)
                {
                    break;
                }
                else
                {
                    counter++;
                }
            }
            return counter;
        }
        public string Analyse(string plainText, string cipherText)
        {
            string keyStream = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == 32 && plainText[i] == 32)
                {
                    keyStream += " ";
                }
                else
                {
                    int temp = itrCount((int)plainText[i], (int)cipherText[i]);
                    keyStream += (char)(97 + temp);
                }
            }
            //string keyStream = Decrypt(cipherText, plainText);
            string keyword = "";
            for(int i =0 ; i< keyStream.Length ; i++)
            {
                if(keyword.Length!=0)
                {
                    if(keyStream[i]==plainText[0])
                    {
                        string sub_key = keyStream.Substring(i, keyStream.Length - i);
                        if (sub_key.Equals(plainText.Substring(0, sub_key.Length)))
                            break;
                    }
                }
                keyword += keyStream[i];
            }
            return keyword;
        }

        public string Decrypt(string cipherText, string key)
        {
            string mappedkey = key;
            string decryptedText = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText[i] == 32 && cipherText[i] == 32)
                {
                    decryptedText += " ";
                }
                else
                {
                    int temp = itrCount((int)mappedkey[i], (int)cipherText[i]);
                    decryptedText += (char)(97 + temp);
                    if (mappedkey.Length < cipherText.Length)
                        mappedkey += (char)(97 + temp);
                }
            }
            return decryptedText;
        }

        public string Encrypt(string plainText, string key)
        {
            createTable();
            string mappedkey = Mappingkey(plainText, key);
            string encryptedText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == 32 && mappedkey[i] == 32)
                {
                    encryptedText += " ";
                }
                else
                {
                    int x = (int)plainText[i] - 97;
                    int y = (int)mappedkey[i] - 97;
                    encryptedText += (char)tableArr[x, y];
                }
            }
            return encryptedText;
        }
    }
}
