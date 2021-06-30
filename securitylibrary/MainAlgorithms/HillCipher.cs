using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<int> key = new List<int>();
            int[,] subCipher2D = new int[2, 2];

            List<int> subPlain = new List<int>();
            List<int> subCipher = new List<int>();
            bool vaild = false;
            int i2=0;
            for ( int i = 0; i < plainText.Count; i += 2)
            {
                for (int j = i + 2; j < plainText.Count; j += 2)
                {


                    subPlain.Add(plainText[i]);
                    subPlain.Add(plainText[i + 1]);
                    subPlain.Add(plainText[j]);
                    subPlain.Add(plainText[j + 1]);


                    subCipher.Add(cipherText[i]);
                    subCipher.Add(cipherText[i + 1]);
                    subCipher.Add(cipherText[j]);
                    subCipher.Add(cipherText[j + 1]);


                    for (int k = 0; k < subPlain.Count; k++)
                        Console.WriteLine(subPlain[k]);
                    Console.WriteLine("----------------------1");
                  //  Console.WriteLine("valid key  " + IsValidKey(subPlain));

                    if (IsValidKey(subPlain,2))
                    {
                        vaild = true;
                        break;
                    }
                    SwapNum(0, 2, ref subPlain);
                    SwapNum(1, 3, ref subPlain);

                    SwapNum(0, 2, ref subCipher);
                    SwapNum(1, 3, ref subCipher);

                    for (int k = 0; k < subPlain.Count; k++)
                        Console.WriteLine(subPlain[k]);
                    Console.WriteLine("----------------------2");
                  //  Console.WriteLine("valid key  " + IsValidKey(subPlain));

                    if (IsValidKey(subPlain,2))
                    {
                        vaild = true;
                        break;
                    }


                    subPlain.Clear();
                    subCipher.Clear();

                }
                i2 = i;
                if (vaild)
                    break;

            }
            if (subCipher.Count==0)
                throw new InvalidAnlysisException();


            for (int i = 0; i < subPlain.Count; i++)
            {
                Console.WriteLine(" sub cipher  " + subCipher[i]);
            }
            List<int> res = new List<int>();
          //  res = Mat2Dinverse(subPlain);
            for (int i = 0; i < res.Count; i++)
                Console.WriteLine(" inverst " + res[i]);
            //return MatMalti(res,transpose(2,FindSubDiterment(subCipher)));
            // res = Mat2Dinverse(subPlain);

            int det = Mod26((subPlain[0] * subPlain[3] - subPlain[1] * subPlain[2]));
            int d = 1;
            while (d * det % 26 != 1)
            {
                d++;


            }
            SwapNum(0, 3, ref subPlain);
            //Console.WriteLine(" det" + det);
            for (int i = 0; i < subPlain.Count; i++)
            {
                if (i == 1 || i == 2)
                    subPlain[i] *= -1;
                subPlain[i] = subPlain[i] * d;
                //  Console.WriteLine(" " + key[i]);
            }




            //for (int i = 0; i < res.Count; i++)
            //    Console.WriteLine(" inverst " + res[i]);
            subCipher2D = Make2D(subCipher, 2);
            subCipher.Clear();
            subCipher = transpose(2, subCipher2D);
             key= MatMalti( subPlain, subCipher);
            return key;

        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            // throw new NotImplementedException();
          
            List<int> plainText = new List<int>();
            if (key.Count ==  4)
            {
                if (!IsValidKey(key, 2))
                    throw new InvalidAnlysisException();
                int det = Mod26((key[0] * key[3] - key[1] * key[2]));
                int d = 1;
                //while (((d * det) % 26) != 1)
                //{
                //    d++;


                //}
                for( int i =0;i <26; i++)
                {
                    if (!(((d * det) % 26) != 1))
                        break;
                       
                        d++;
                }



              //  int det = 1 / (key[0] * key[3] - key[1] * key[2]);
               // int sum = 0;
                SwapNum(0, 3, ref key);
                //Console.WriteLine(" det" + det);
                for (int i = 0; i < key.Count; i++)
                {
                    if (i == 1 || i == 2)
                        key[i] *= -1;
                    key[i] = key[i] * d;
                    Console.WriteLine(" " + key[i]);
                }
                plainText = MatMalti(key, cipherText);

            }
            else if (key.Count == 9){
                int[,] key2d = new int[3, 3];
                int[,] subdeterment = new int[3, 3];
                List<int> reverKesy = new List<int>();
                key2d = Make2D(key,3);
                for (int row = 0; row < 3; row++)
                    for (int col = 0; col < 3; col++)
                        subdeterment[row, col] = getCofactor3dMat(key2d, row, col);
                //transpose 

                reverKesy = transpose(3, subdeterment);

                plainText = MatMalti(reverKesy, cipherText);


            }


            return plainText;
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<int> cipherText = new List<int>();
            //int m =0 ,z ,sum =0;
            //if (key.Count == 4) 
            //    m = 2;
            //else if (key.Count == 9)
            //    m = 3;
            //while (plainText.Count % m != 0)
            //    plainText.Add(0);
            //z = plainText.Count;
            //for (int i = 0; i < z; i += m)
            //{
            //    for (int j = 0; j < m; j++)
            //    {
            //        sum = 0;
            //        for (int k = 0; k < m; k++)
            //        {
            //            sum += plainText[i + k] * key[k + j * m];

            //        }
            //        cipherText.Add(sum % 26);
            //    }
            //}

            cipherText = MatMalti(key, plainText);

            return cipherText;

        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            ///  throw new NotImplementedException();
            if (!IsValidKey(cipherText, 3))
                  throw new InvalidAnlysisException();
            int[,] key2d = new int[3, 3];
            int[,] subdeterment = new int[3, 3];
            int[,] chiperTraspoes = new int[3, 3];
            List<int> reverKesy = new List<int>();
            key2d = Make2D(plainText,3);

            chiperTraspoes = Make2D(cipherText,3);
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    subdeterment[row, col] = getCofactor3dMat(key2d, row, col);
            //transpose 

            reverKesy = transpose(3, subdeterment);
            cipherText.Clear();
            cipherText = transpose(3, chiperTraspoes);
            return MatMalti(reverKesy, cipherText);

        }


        public List<int> Multi(List<int> mat1, int row1, int col1, List<int> mat2, int row2, int col2)
        {


            List<int> result = new List<int>();
            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < col2; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < col1; k++)
                        sum = sum + mat1[i * col1 + k] * mat2[k * col2 + j];
                    result[i * col2 + j] = sum % 26;
                }
            }

            return result;
        }

        private int Mod26(int n )
        {
            if (n < 0)
            {
                while (n  < 0)
                    n += 26;
                return n;
            }
            return n % 26;
        }
        private void SwapNum(int x, int y, ref List<int> key)
        {

            int tempswap = key[x];
            key[x] = key[y];
            key[y] = tempswap;
        }




        int [,] Make2D(List<int> key, int size)
        {

            int[,] twoDKey = new int[size, size];
            int count = 0;
            for( int i =0;i<size; i++)
                for(int j=0; j<size; j++)
                {
                    twoDKey[i, j] = key[count];
                    count++;
                }

            return twoDKey;
        }

         int getCofactor3dMat(int[,] mat,int r, int q)
        {

            // call caulat det 
            int det = Mod26(mat[0, 0] * (mat[1, 1] * mat[2, 2] - mat[1, 2] * mat[2, 1]) -
                            mat[0, 1] * (mat[1, 0] * mat[2, 2] - mat[1, 2] * mat[2, 0]) +
                            mat[0, 2] * (mat[1, 0] * mat[2, 1] - mat[1, 1] * mat[2, 0]));
            Console.WriteLine(" " + det);
            //Calculate b = det(k) -1  ( b x det(k) mod 26 =1 )
            int d = 1;
            while (d * det % 26 != 1)
                d++;
            Console.WriteLine("d " + d);
            int i = 0, j = 0;
            int[,] subDet = new int[2, 2];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (row != r && col != q)
                    {
                        subDet[i, j++] = mat[row, col];
                        if (j == 3 - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
            return  Mod26(  d*(int)Math.Pow(-1, (r + q)) * Mod26 ((subDet[0, 0] * subDet[1, 1] - subDet[0, 1] * subDet[1, 0]))      );
        }
       

        List<int> transpose(int size, int[,]mat)
        {
            List<int> result = new List<int>();
            int[,] subarr = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    result.Add(mat[j, i]);
                    Console.WriteLine(mat[j, i]);
                }
            return result;
        }

        List<int> MatMalti(List<int> mat1, List<int> mat2)
        {
            List<int> result = new List<int>();
            int m = 0, z, sum = 0;
            if (mat1.Count == 4)
                m = 2;
            else if (mat1.Count == 9)
                m = 3;
            while (mat2.Count % m != 0)
                mat2.Add(0);
            z = mat2.Count;
            for (int i = 0; i < z; i += m)
            {
                for (int j = 0; j < m; j++)
                {
                    sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += mat2[i + k] * mat1[k + j * m];

                    }
                    result.Add(Mod26(sum) );
                }
            }
            return result;
        }



        bool IsValidKey(List<int> key, int size)
        {
            int det = 0;
            for (int i = 0; i < key.Count; i++)
                if (key[i] < 0)
                    return false;
            if(size == 2)
            det= Mod26 (key[0] * key[3] - key[1] * key[2]);
            else if(size == 3)
            {
                int[,] mat = new int[3, 3];
                mat = Make2D(key, 3);
                det = Mod26(mat[0, 0] * (mat[1, 1] * mat[2, 2] - mat[1, 2] * mat[2, 1]) -
                            mat[0, 1] * (mat[1, 0] * mat[2, 2] - mat[1, 2] * mat[2, 0]) +
                            mat[0, 2] * (mat[1, 0] * mat[2, 1] - mat[1, 1] * mat[2, 0]));
            }

            if (gcd(26, det) != 1)
                return false;

            if (det == 0)
                return false;
            return true;


        }

        //List<int>FindRevers(List< int> key)
        //{
        //    int det = 1 / (key[0] * key[3] - key[1] * key[2]);
        //    SwapNum(0, 3, ref key);
        //    for (int i = 0; i < key.Count; i++)
        //    {
        //        if (i == 1 || i == 2)
        //            key[i] *= -1;
        //        key[i] = Mod26(key[i] * det);
        //        Console.WriteLine(" " + key[i]);
        //    }

        //}



         int gcd(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }


         List<int> Mat2Dinverse(List<int> key)
        {
            int det = Mod26((key[0] * key[3] - key[1] * key[2]));
            int d = 1;
            while (d * det % 26 != 1)
                d++;
            SwapNum(0, 3, ref key);
            //Console.WriteLine(" det" + det);
            for (int i = 0; i < key.Count; i++)
            {
                if (i == 1 || i == 2)
                    key[i] *= -1;
                key[i] = key[i] * d;
                //  Console.WriteLine(" " + key[i]);
            }

            return key;

        }

    }

  
}
