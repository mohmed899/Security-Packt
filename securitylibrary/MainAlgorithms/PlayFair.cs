using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public  string Decrypt(string cipherText, string key)
        {
            // throw new NotImplementedException();
            char[,] mat = MakeMatrix(Unique(key));
            Tuple<int, int> FirstCharPostin;
            Tuple<int, int> SecondCharPostin;
            cipherText = cipherText.ToLower();
            String plainText = "";
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                Console.WriteLine(i);
                SecondCharPostin = FindPositon(cipherText[i + 1], mat);
                FirstCharPostin = FindPositon(cipherText[i], mat);

                Console.WriteLine(cipherText[i + 1] + " " + cipherText[i]);
                Console.WriteLine(FirstCharPostin + " " + SecondCharPostin);
                if (FirstCharPostin.Item1 == SecondCharPostin.Item1)
                {
                    int x = FirstCharPostin.Item2 - 1;
                    int y = SecondCharPostin.Item2 - 1;
                    if (x < 0)
                        x = 4;
                    if (y < 0)
                        y = 4;
                    plainText += mat[FirstCharPostin.Item1, x];
                    plainText += mat[SecondCharPostin.Item1, y];
                    // continue;
                }

                else if (FirstCharPostin.Item2 == SecondCharPostin.Item2)
                {
                    int x = FirstCharPostin.Item1 - 1;
                    int y = SecondCharPostin.Item1 - 1;
                    if (x < 0)
                        x = 4;
                    if (y < 0)
                        y = 4;
                    plainText += mat[x, FirstCharPostin.Item2];
                    plainText += mat[y, SecondCharPostin.Item2];

                }
                else
                {

                    Console.WriteLine("im in else'");
                    plainText += mat[FirstCharPostin.Item1, SecondCharPostin.Item2];
                    Console.WriteLine(FirstCharPostin.Item1 + " " + SecondCharPostin.Item2);
                    plainText += mat[SecondCharPostin.Item1, FirstCharPostin.Item2];
                    Console.WriteLine(SecondCharPostin.Item1 + " " + FirstCharPostin.Item2);
                }
            }

            for (int i = 0; i < plainText.Length-2 ; i += 2)
            {
                if (plainText[i+1] == 'x')
                    if ((plainText[i] == plainText[i + 2]))
                    {
                        plainText = plainText.Remove(i+1, 1);
                        i--;
                    }
            }

            if (plainText[plainText.Length-1]=='x')
                plainText = plainText.Remove(plainText.Length - 1, 1);

            return plainText;



        }

        public string Encrypt(string plainText, string key)
        {
          
            for (int i = 0; i < plainText.Length; i += 2)
            {
                if (i+1< plainText.Length&&plainText[i] == plainText[i + 1])
                    plainText = plainText.Insert(i + 1, "x");

            }
            if (plainText.Length % 2 != 0)
                plainText = plainText.Insert(plainText.Length, "x");

            //make playfair mat 
            char[,] mat = MakeMatrix(Unique(key));

            Tuple<int, int> FirstCharPostin;
            Tuple<int, int> SecondCharPostin;
            String CipherText = "";

            for (int i = 0; i < plainText.Length; i += 2)
            {
                if (plainText[i] == plainText[i + 1])
                {
                    SecondCharPostin = FindPositon('x', mat);
                    i--;
                }
                else
                    SecondCharPostin = FindPositon(plainText[i + 1], mat);
                   FirstCharPostin = FindPositon(plainText[i], mat);

                if (FirstCharPostin.Item1 == SecondCharPostin.Item1)
                {
                    CipherText += mat[FirstCharPostin.Item1, (FirstCharPostin.Item2 + 1) % 5];
                    CipherText += mat[SecondCharPostin.Item1, (SecondCharPostin.Item2 + 1) % 5];
                    // continue;
                }

                else if (FirstCharPostin.Item2 == SecondCharPostin.Item2)
                {
                    CipherText += mat[(FirstCharPostin.Item1 + 1) % 5, FirstCharPostin.Item2];
                    CipherText += mat[(SecondCharPostin.Item1 + 1) % 5, SecondCharPostin.Item2];

                }



                else
                {
                    CipherText += mat[FirstCharPostin.Item1, SecondCharPostin.Item2];
                    Console.WriteLine(FirstCharPostin.Item1 + " " + SecondCharPostin.Item2);
                    CipherText += mat[SecondCharPostin.Item1, FirstCharPostin.Item2];
                    Console.WriteLine(SecondCharPostin.Item1 + " " + FirstCharPostin.Item2);
                }
            }


            return CipherText.ToUpper();
        }

        private static String Unique(String s)
        {
            String str = "";
            int len = s.Length;
            char c;
            for (int i = 0; i < len; i++)
            {
                c = s[i];
                if (str.IndexOf(c) < 0)
                {
                    str += c;
                }
            }


            str = str.ToLower();
            for(int i =97; i<=122; i++)
            {
                c = (char)i;
                if (c == 'j')
                    continue;
                if (str.IndexOf(c) < 0)
                {
                    str += c;
                }
            }
            return str;
        }
        private static char[,] MakeMatrix(String s)
        {
            char[,] mat = new char[5,5];
            int x = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++) {
                    mat[i, j] = s[x];
                    x++;
                        }

            return mat;
        }

        private Tuple<int,int> FindPositon( char l ,char[,] mat)
        {
            if (l == 'j')
                l = 'i';
            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 5; j++)
                    if (mat[i, j] == l)
                    {
                        return Tuple.Create(i,j);
                    } 
            }
            return Tuple.Create(0, 0); 
        }  
    }
}
