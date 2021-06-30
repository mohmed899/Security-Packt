using System;

namespace SecurityLibrary.AES
{
    public class AES : CryptographicTechnique
    {
       
        private string[,] Sbox = new string[16, 16] {
     {"63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76"},
      {"ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0"},
     {"b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15"},
      {"04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75"},
     {"09", "83", "2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84"},
     {"53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf"},
     {"d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8"},
      {"51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2"},
      {"cd", "0c", "13", "ec", "5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73"},
      {"60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db"},
      {"e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79"},
      {"e7", "c8", "37", "6d", "8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08"},
      {"ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a"},
      {"70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e"},
     {"e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df"},
      {"8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16"} };
    private string[,] iSbox = new string[16, 16] {
     {"52", "09", "6a", "d5", "30", "36", "a5", "38", "bf", "40", "a3", "9e", "81", "f3", "d7", "fb"},
     {"7c", "e3", "39", "82", "9b", "2f", "ff", "87", "34", "8e", "43", "44", "c4", "de", "e9", "cb"},
     {"54", "7b", "94", "32", "a6", "c2", "23", "3d", "ee", "4c", "95", "0b", "42", "fa", "c3", "4e"},
      {"08", "2e", "a1", "66", "28", "d9", "24", "b2", "76", "5b", "a2", "49", "6d", "8b", "d1", "25"},
      {"72", "f8", "f6", "64", "86", "68", "98", "16", "d4", "a4", "5c", "cc", "5d", "65", "b6", "92"},
      {"6c", "70", "48", "50", "fd", "ed", "b9", "da", "5e", "15", "46", "57", "a7", "8d", "9d", "84"},
      {"90", "d8", "ab", "00", "8c", "bc", "d3", "0a", "f7", "e4", "58", "05", "b8", "b3", "45", "06"},
      {"d0", "2c", "1e", "8f", "ca", "3f", "0f", "02", "c1", "af", "bd", "03", "01", "13", "8a", "6b"},
      {"3a", "91", "11", "41", "4f", "67", "dc", "ea", "97", "f2", "cf", "ce", "f0", "b4", "e6", "73"},
      {"96", "ac", "74", "22", "e7", "ad", "35", "85", "e2", "f9", "37", "e8", "1c", "75", "df", "6e"},
      {"47", "f1", "1a", "71", "1d", "29", "c5", "89", "6f", "b7", "62", "0e", "aa", "18", "be", "1b"},
      {"fc", "56", "3e", "4b", "c6", "d2", "79", "20", "9a", "db", "c0", "fe", "78", "cd", "5a", "f4"},
      {"1f", "dd", "a8", "33", "88", "07", "c7", "31", "b1", "12", "10", "59", "27", "80", "ec", "5f"},
      {"60", "51", "7f", "a9", "19", "b5", "4a", "0d", "2d", "e5", "7a", "9f", "93", "c9", "9c", "ef"},
      {"a0", "e0", "3b", "4d", "ae", "2a", "f5", "b0", "c8", "eb", "bb", "3c", "83", "53", "99", "61"},
      {"17", "2b", "04", "7e", "ba", "77", "d6", "26", "e1", "69", "14", "63", "55", "21", "0c", "7d"} };


    private int ro = 1;
      
        
       
       
       
      
        public override string Decrypt(string cipherText, string key)
        {
            key = key.ToUpper();
            cipherText = cipherText.ToUpper();
            string[] Keys = new string[11];
            Keys[0] = key;
            for (int i = 1; i < Keys.Length; i++)
                Keys[i] = nk(Keys[i - 1]);
            cipherText = adrk(cipherText, Keys[Keys.Length - 1]);
            cipherText = isr(cipherText);
            cipherText = SubWhole(cipherText);
            for (int i = Keys.Length - 2; i > 0; i--)
            {
                cipherText = adrk(cipherText, Keys[i]);
                string[,] Mat = mat(cipherText);
                Mat = imcc(Mat);
                cipherText = mhh(Mat);
                cipherText = isr(cipherText);
                cipherText = SubWhole(cipherText);
            }
            cipherText = adrk(cipherText, Keys[0]);
            return cipherText;
        }

        public override string Encrypt(string plainText, string key)
        {
            key = key.ToUpper();
            plainText = plainText.ToUpper();
            int c = 10;
          
            plainText = adrk(plainText, key);
            for (int tp = 0; tp < c; tp++)
            {
                string result = "0x";
                for (int i = 2; i < plainText.Length; i += 2)
                    result += sb(plainText[i], plainText[i + 1]);
                result = shiftt(result);
                string[,] Mat = mat(result);
                if (tp != 9)
                    Mat = MixColumns(Mat);
                key = nk(key);
                plainText = adrk(mhh(Mat), key);
            }
            return plainText;
        }
        public string[,] imcc(string[,] Mat)
        {
            string[,] res = new string[4, 4];
            int[] arr = new int[4] { 14, 11, 13, 9 };
            for (int i = 0; i < 4; i++)
            {
                int[] t = new int[4];
                for (int k = 0, idx = 0; k > -4; k--, idx++)
                {
                    int cnt = 0;
                    for (int j = 0; j < 4; j++)
                        cnt ^= imc(Mat[j, i], arr[(k + j + 4) % 4]);
                    t[idx] = cnt;
                }
                for (int k = 0; k < 4; k++)
                    res[k, i] = StrIntHex(t[k]);
            }
            return res;
        }




        public int ConvertHexToInt(char x)
        {
            if (x <= '9')
                return Convert.ToInt32(x - '0');
            else
                return Convert.ToInt32(x - 'A' + 10);
        }
        public char ConvetIntToHex(int x)
        {
            if (x <= 9)
                return Convert.ToChar(x + '0');
            else
                return Convert.ToChar(x + 'A' - 10);
        }
        public int ww(string a, int it)
        {

            switch (it)
            {
                case 1:
                    return Convert.ToInt32(a, 16);
                case 2:
                    {
                        int temp = ConvertHexToInt(a[0]);
                        temp <<= 4;
                        temp += ConvertHexToInt(a[1]);
                        temp = shl(temp);
                        return temp;
                    }

                default:
                    {
                        int temp = ConvertHexToInt(a[0]);
                        temp <<= 4;
                        temp += ConvertHexToInt(a[1]);
                        int t2 = temp;
                        temp = shl(temp);
                        temp ^= t2;
                        return temp;
                    }
            }
        }
        public string StrIntHex(int x)
        {
            string ans = "";
            ans += ConvetIntToHex(x / 16);
            ans += ConvetIntToHex(x % 16);
            return ans;
        }
        public string[,] MixColumns(string[,] Mat)
        {
            string[,] res = new string[4, 4];
            int[] arr = new int[4] { 2, 3, 1, 1 };
            for (int i = 0; i < 4; i++)
            {
                int[] t = new int[4];
                for (int k = 0, idx = 0; k > -4; k--, idx++)
                {
                    int cnt = 0;
                    for (int j = 0; j < 4; j++)
                        cnt ^= ww(Mat[j, i], arr[(k + j + 4) % 4]);
                    t[idx] = cnt;
                }
                for (int k = 0; k < 4; k++)
                    res[k, i] = StrIntHex(t[k]);
            }
            return res;
        }
        public string nk(string key)
        {

            string pl = key.Substring(28, 6);
            pl += key.Substring(26, 2);
            string h = "", dd = "";
            for (int i = 0; i < pl.Length; i += 2)
                dd += sb(pl[i], pl[i + 1]);
            pl = dd;
            for (int i = 2; i < key.Length; i += 8)
            {
                string tmp = key.Substring(i, 8);
                string f = StrIntHex(ro);
                f += "000000";
                if (i == 2)
                    tmp = xor(tmp, f);
                tmp = xor(tmp, pl);
                pl = tmp;
                h += pl;
            }
            ro <<= 1;
            if (ro == 256)
                ro = 0x1b;
            return "0x" + h;
        }
        public string SubWhole(string f)
        {
            string res = "0x";
            for (int i = 2; i < f.Length; i += 2)
                res += isb(f[i], f[i + 1]);
            return res;
        }
        public string xor(string a, string b)
        {
            string res = "";
            for (int i = 0; i < a.Length; i++)
                res += xor(a[i], b[i]);
            return res;
        }
        public string adrk(string text, string key)
        {
            string ans = "0x";
            for (int i = 2; i < text.Length; i++)
                ans += xor(text[i], key[i]);
            return ans;
        }
        public char xor(char a, char b)
        {
            int A = ConvertHexToInt(a);
            int B = ConvertHexToInt(b);
            A = A ^ B;
            return ConvetIntToHex(A);
        }

        public string sb(char a, char b)
        {
            int x = ConvertHexToInt(a);
            int y = ConvertHexToInt(b);
            return Sbox[ConvertHexToInt(a), ConvertHexToInt(b)].ToUpper();
        }
        public string isb(char a, char b)
        {
            int x = ConvertHexToInt(a);
            int y = ConvertHexToInt(b);
            return iSbox[ConvertHexToInt(a), ConvertHexToInt(b)].ToUpper();
        }
        public void swap(ref char a, ref char b)
        {
            char tmp = a;
            a = b;
            b = tmp;
        }
        public string shiftt(string ss)
        {
            string result = ss.Substring(0, 4);
            result += ss.Substring(12, 2);
            result += ss.Substring(22, 2);
            result += ss.Substring(32, 2);
            result += ss.Substring(10, 2);
            result += ss.Substring(20, 2);
            result += ss.Substring(30, 2);
            result += ss.Substring(8, 2);
            result += ss.Substring(18, 2);
            result += ss.Substring(28, 2);
            result += ss.Substring(6, 2);
            result += ss.Substring(16, 2);
            result += ss.Substring(26, 2);
            result += ss.Substring(4, 2);
            result += ss.Substring(14, 2);
            result += ss.Substring(24, 2);
            return result;
        }
        public int imc(string a, int it)
        {
            int v = ConvertHexToInt(a[0]);
            v <<= 4;
            v += ConvertHexToInt(a[1]);
            switch (it)
            {
                case 9:
                    {
                        int temp = v;
                        temp = shl(temp);
                        temp = shl(temp);
                        temp = shl(temp);
                        temp ^= v;
                        return temp;
                    }

                case 11:
                    {
                        int temp = v;
                        temp = shl(temp);
                        temp = shl(temp);
                        temp ^= v;
                        temp = shl(temp);
                        temp ^= v;
                        return temp;
                    }

                case 13:
                    {
                        int temp = v;
                        temp = shl(temp);
                        temp ^= v;
                        temp = shl(temp);
                        temp = shl(temp);
                        temp ^= v;
                        return temp;
                    }

                default:
                    {
                        int temp = v;
                        temp = shl(temp);
                        temp ^= v;
                        temp = shl(temp);
                        temp ^= v;
                        temp = shl(temp);
                        return temp;
                    }
            }
        }
        public string isr(string ss)
        {
            string result = ss.Substring(0, 4);
            result += ss.Substring(28, 2);
            result += ss.Substring(22, 2);
            result += ss.Substring(16, 2);
            result += ss.Substring(10, 2);
            result += ss.Substring(4, 2);
            result += ss.Substring(30, 2);
            result += ss.Substring(24, 2);
            result += ss.Substring(18, 2);
            result += ss.Substring(12, 2);
            result += ss.Substring(6, 2);
            result += ss.Substring(32, 2);
            result += ss.Substring(26, 2);
            result += ss.Substring(20, 2);
            result += ss.Substring(14, 2);
            result += ss.Substring(8, 2);
            return result;
        }
        public string[,] mat(string ss)
        {
            ss = ss.Substring(2);
            string[,] dd = new string[4, 4];
            int cnt = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    dd[j, i] = ss.Substring(cnt, 2);
                    cnt += 2;
                }
            return dd;
        }
        public string mhh(string[,] mat)
        {
            //   int i = 0, j = 0, x = 4;
            string res = "0x";
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    res += mat[j, i];
            return res;
        }
        public int shl(int x)
        {
            int ans = x;
            ans <<= 1;
            if ((ans & (1 << 8)) > 0)
                ans ^= (0x1B ^ (1 << 8));
            return ans;
        }
    }
}