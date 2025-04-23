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
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            // throw new NotImplementedException();

            string plainText = "";
            int cipherTextLength = cipherText.Length;
            int steps = cipherTextLength / key.Count;
            int remainder = cipherTextLength % key.Count;
            for (int i = 0; i < steps; i++)
            {
                for (int k = 0; k < key.Count; k++)
                {
                    int column = key[k] - 1;
                    int extraSteps = 0;
                    if (column < remainder)
                        extraSteps = column;
                    int cipherIndex = column * steps + extraSteps + i;
                    plainText += cipherText[cipherIndex];
                    if (column < remainder && i == steps - 1)
                    {
                        plainText += cipherText[cipherIndex + 1];
                        i++;
                    }
                }
            }
            return plainText;

        }

        public string Encrypt(string plainText, List<int> key)
        {
            //throw new NotImplementedException();

            double rows = key.Count();
            //double columns = (double)plainText.Length / rows;
            double columns = Math.Ceiling((double)plainText.Length / rows);

            char[,] matrix = new char[(int)columns, (int)rows];

            double x = plainText.Length / rows;

            int c_index = 0;
            for (int R = 0; R <columns; R++)
            {
                
             
                for (int c = 0; c < rows; c++)
                {
                    matrix[R, c] = plainText[R];
                    if (c_index < plainText.Length)
                    { 
                       
                        matrix[R, c] = plainText[c_index];
                        c_index++;
                    }
                    else
                    {
                       

                        matrix[R, c] = 'x';
                    }
                }



            }
            ///////////////////////////////////////////


            int[] keyArray = new int[key.Count];
            for (int i = 0; i < key.Count; i++)
            {
                keyArray[key[i] - 1] = i;
            }

            string myciphertext = "";

            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    myciphertext += matrix[j, keyArray[i]];
                }
            }

            Console.WriteLine(myciphertext.ToUpper());
            return myciphertext.ToUpper();


        }











    }
    }

