using System;

using System.IO;



namespace CipherEncrytDecrypt

{

    class Program

    {

        static bool checkKey(int key, int permute)

        {

            bool result = true;

            int[] ar = new int[key + 1];

            int x;

            if (("" + permute).Length != key)

            {

                result = false;

            }

            else

            {



                for (x = 0; x < ar.Length; x++)

                    ar[x] = 0;

                for (int bk = permute; bk != 0; bk /= 10)

                {

                    x = bk % 10;



                    ar[x]++;

                }



                for (x = 1; result && x < ar.Length; x++)

                {

                    // Console.WriteLine(" x : " + ar[x]); 

                    if (ar[x] != 1)

                        result = false;



                }

            }

            return result;

        }



        static int[] getPermuteArray(int key, int permute)

        {

            int[] ar = new int[key];

            int x, y;

            for (x = key - 1; permute != 0; x--)

            {

                ar[x] = permute % 10;

                permute /= 10;

            }

            return ar;



        }



        static string encrypt(string text, int N, int[] A)

        {

            string s1, res = "";

            int z;

            if (text.Length % N != 0)

            {

                int diff = (N - text.Length % N) % N;

                for (; diff > 0; diff--)

                {

                    text += "x";

                }

            }

            //text+="x";  // an extra x , for loop processing. 

            s1 = "";

            z = text.Length;

            for (int x = 0; x <= z; x++)

            {



                if (x > 0 && x % N == 0)

                {

                    //Console.write("s1 : "+s1); 

                    for (int y = 0; y < N; y++)

                        res += s1[A[y] - 1];

                    s1 = "";

                    if (x != z)

                        s1 += text[x];

                }

                else

                    s1 += text[x];

            }





            // Console.WriteLine("  Result : " + res); 

            return res;

        }

        static string decrypt(string text, int N, int[] A)

        {

            string res = "";

            int x, y; // integer values 



            int z = text.Length;

            char[] str = new char[N];





            for (x = 0; x < z; x += N)

            {

                for (y = 0; y < N; y++)

                    str[A[y] - 1] = text[x + y];

                for (y = 0; y < str.Length; y++)

                    res += str[y];

            }



            // Console.WriteLine(" RESULT 1 : " + res); 



            // for (x = 0; x < res.Length; x++) 

            //    Console.WriteLine("res[" + x + "] : " + res[x]); 

            while (res.EndsWith("x"))

            {

                res = res.Substring(0, res.LastIndexOf("x"));

            }



            //res = res.Substring(0, res.Length - x); 





            return res;

        }







        static void Main(string[] args)

        {

            string line = "";   // to hold lines one by one from the opned file  

            string fileName = "", option = "";

            string[] data = Console.ReadLine().Split();

            int key = 0, permute = 0;

            string plainText, cipherText;

            int[] permuteArray;



            fileName = data[0];

            option = data[1];

            data = Console.ReadLine().Split();





            key = int.Parse(data[0]);

            permute = int.Parse(data[1]);







            // Console.Write("\n Filename : " + fileName + "\n Option  : " + option); 

            // Console.Write("\n Key      : " + key +      "\n Permute : " + permute); 



            try                 // to prevent any unexpected Error/Exception   

            {

                StreamReader iFile = File.OpenText(fileName); // Opening file   

                while (iFile.EndOfStream == false)    // End of file mark not reached.                                        

                {

                    line += iFile.ReadLine();    // storing one line in the line variable.  

                }

                iFile.Close();



                // Console.Write(" DATA : " + line); 

                if (option != "E" && option != "D")

                {

                    Console.Write("\n Invalid option entered!");

                    return;

                }



                if (checkKey(key, permute) == false)

                {

                    Console.Write("\n Wrong key/ permutation entered!");

                    return;

                }

                permuteArray = getPermuteArray(key, permute);

                if (option == "E")

                {

                    cipherText = encrypt(line, key, permuteArray);

                    Console.WriteLine("\n Encrypted Text: " + cipherText);



                }

                else

                {

                    plainText = decrypt(line, key, permuteArray);

                    Console.WriteLine("\n Decrypted Text: " + plainText);

                }

            }

            catch (FileNotFoundException e)

            {

                Console.WriteLine("\n File not found!");

            }

            catch (Exception ex)                   // Exception handling code   

            {

                Console.WriteLine("\n ERROR:" + ex.Message); // displaying error message  

            }



            Console.WriteLine("");

        }

    }
}