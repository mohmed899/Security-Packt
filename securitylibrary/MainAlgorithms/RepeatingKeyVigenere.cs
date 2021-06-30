using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
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
                        tableArr[i,j] = temp;
                    }
                    else
                    {
                        temp = (i + 97) + j;

                        char c = (char)temp;
                        tableArr[i,j] = temp;
                    }
                } 
            } 
        }
        string Mappingkey(string txt , string key)
        {
            string keyMap = "";
            for (int i = 0, j = 0; i < txt.Length; i++)
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
                        j = 0;
                        keyMap += key[j];
                        j++;
                    }
                } 
            } 

            return keyMap;
        }
        int itrCount(int key, int ctxt)
        {
            if(ctxt<97 && ctxt>32)
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

            int sindex = 1;
            string keyText = "";
            string addkey = "";
            string realkey = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == 32 && plainText[i] == 32)
                {
                    keyText += " ";
                }
                else
                {
                    int temp = itrCount((int)plainText[i], (int)cipherText[i]);
                    keyText += (char)(97 + temp);
                }
            }
            for (int i = 0; i < (keyText.Length) - 1; i++)
            {
                sindex++;
                int j;
                if (sindex < keyText.Length)
                {
                    for (j = sindex; j < keyText.Length; j++)
                    {
                        if ((int)keyText[i] == (int)keyText[j])
                        {
                            addkey += keyText[j];
                            for (int v = 0; v < addkey.Length; v++)
                            {
                                if (!(addkey[v] == keyText[v]))
                                    addkey = "";
                            }
                            break;
                        }
                    }
                    sindex = j;
                }
            }
            for (int q = 0; q < (keyText.Length) - (addkey.Length); q++)
            {
                realkey += keyText[q];
            }
            return realkey;
        }

        public string Decrypt(string cipherText, string key)
        {
            string mappedkey = Mappingkey(cipherText, key);
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
                }
            }
            return decryptedText;
        }

        public string Encrypt(string plainText, string key)
        {

            createTable();
            string mappedkey = Mappingkey(plainText, key);
            string encryptedText = "";
            for(int i = 0; i < plainText.Length; i++)
            {
                if(plainText[i] == 32 && mappedkey[i] == 32)
                {
                    encryptedText += " ";
                }
                else
                {
                    int x = (int)plainText[i] - 97;
                    int y = (int)mappedkey[i] - 97;
                    encryptedText += (char)tableArr[x,y];
                }


            }
            return encryptedText;



        }
    }
}
