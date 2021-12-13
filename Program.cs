using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;
using System.Collections;


namespace D3T2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";

            int numRows = CountRows(filename);
            string[] arrayRows = new string[numRows];
            GetRows(filename, arrayRows);
            int numCols = CountCols(filename, arrayRows);
            int[,] passedArray = new int[numRows, numCols];
            GetArray(arrayRows, numRows, passedArray);

            char[] gammaCharArr = new char[numRows];
            gammaCharArr = GetGammaBin(passedArray).ToCharArray();

            WriteLine(gammaCharArr);


            int oxygenLevel = GetOxygen(passedArray);
            WriteLine("Oxygen Level is " + oxygenLevel);

            int CO2Level = GetCO2(passedArray);
            WriteLine("CO2 Level is " + CO2Level);


            int DiagnosticRep = oxygenLevel * CO2Level;
            WriteLine(DiagnosticRep);

            ReadLine();


        }

        private static void DisplayArray(int[,] ArrayToDisplay)
        {
            WriteLine(ArrayToDisplay.GetLength(0));
            for (int row =0; row < ArrayToDisplay.GetLength(0); row++)
            {
                for (int col =0; col < ArrayToDisplay.GetLength(1); col++)
                {
                    Write(ArrayToDisplay[row, col]);
                }
                Write(" ..... {0}\n", row);
            }
        }

        private static void DisplayTranspose(int[,] dataArray)
        {
            for (int col = 0; col < dataArray.GetLength(1); col++)
            {
                for (int row = 0; row < dataArray.GetLength(0); row++)
                {
                    Write(dataArray[row, col]);
                }
                Write("\n");
            }
        }

        private static int CountRows(string filename)
        {
            var file = new StreamReader(filename).ReadToEnd();
            var lines = file.Split(new char[] { '\n' });
            var count = lines.Length;
            return count - 1;

        }

        private static int CountCols(string filename, string[] arrayRows)
        {
            int numCols = arrayRows[0].Length;
            return numCols;

        }

        private static void GetArray(string[] arrayRows, int numRows, int[,] dataArray)
        {
            for (int i = 0; i < numRows; i++)
            {
                char[] tempRow = arrayRows[i].ToCharArray();
                for (int j = 0; j < arrayRows[i].Length; j++)
                {
                    int toInsert = (int)char.GetNumericValue(tempRow[j]);
                    dataArray[i, j] = toInsert;
                }
            }
            
        }

        private static void GetRows(string filename, string[] arrayRows)
        {
            ReadFile(filename, arrayRows);
        }

        private static void ReadFile(string filename, string[] values)
        {
            StreamReader SR = new StreamReader(filename);

            int x = 0;
            while (!SR.EndOfStream)
            {

                string val = SR.ReadLine();
                values[x] = val;
                x++;
            }
            SR.Close();
        }

        private static int GetGamma(int[,] dataArray)
        {
            string binaryNumStr = "";

            for (int j = 0; j < dataArray.GetLength(1); j++)
            {
                int countOnes = 0;
                int countZeros = 0;

                for (int i = 0; i < dataArray.GetLength(0); i++)
                {
                    if (dataArray[i, j] == 0)
                    {
                        countZeros++;
                    }
                    else if (dataArray[i, j] == 1)
                    {
                        countOnes++;
                    }
                    else
                    {
                        WriteLine("Error1");
                    }
                }

                if (countZeros > countOnes)
                    binaryNumStr = binaryNumStr + "0";
                else binaryNumStr = binaryNumStr + "1";


            }

            int gamma = Convert.ToInt32(binaryNumStr, 2);
            return gamma;

        }

        private static string GetGammaBin(int[,] dataArray)
        {
            string binaryNumStr = "";

            for (int j = 0; j < dataArray.GetLength(1); j++)
            {
                int countOnes = 0;
                int countZeros = 0;

                for (int i = 0; i < dataArray.GetLength(0); i++)
                {
                    if (dataArray[i, j] == 0)
                    {
                        countZeros++;
                    }
                    else if (dataArray[i, j] == 1)
                    {
                        countOnes++;
                    }
                    else
                    {
                        WriteLine("Error2");
                    }
                }

                if (countZeros > countOnes)
                    binaryNumStr = binaryNumStr + "0";
                else binaryNumStr = binaryNumStr + "1";


            }

            return binaryNumStr;

        }

        private static int GetEpsilon(int[,] dataArray)
        {
            string binaryNumStr = "";

            for (int j = 0; j < dataArray.GetLength(1); j++)
            {
                int countZeros = CountZeros(dataArray, j);
                int countOnes = CountOnes(dataArray, j);

                if (countZeros > countOnes)
                    binaryNumStr = binaryNumStr + "1";
                else binaryNumStr = binaryNumStr + "0";

            }

            int gamma = Convert.ToInt32(binaryNumStr, 2);
            return gamma;

        }

        private static int CountOnes(int[,] dataArray, int j)
        {
            int countOnes = 0;
            for (int i = 0; i < dataArray.GetLength(0); i++)
            {
                if (dataArray[i, j] == 1)
                {
                    countOnes++;
                }
            }

            return countOnes;
        }

        private static int CountZeros(int[,] dataArray, int j)
        {
            int countZeros = 0;
            for (int i = 0; i < dataArray.GetLength(0); i++)
            {
                if (dataArray[i, j] == 0)
                {
                    countZeros++;
                }

            }

            return countZeros;
        }

        private static int GetOxygen(int[,] passedArray) 
        {
            int toRemove;
            int bitCriteria;
            int newArrayLength;
            int[,] tempArray;
            int rowOld;
            int rowNew;
            string gammaStr;
            int[,] dataArray = passedArray;
            char[] gammaCharArr = new char[dataArray.GetLength(1)];



            for (int iteration = 0; iteration < dataArray.GetLength(1); iteration++)
            {
                if (dataArray.GetLength(0) > 1)
                {
                    gammaStr = GetGammaBin(dataArray);
                    gammaCharArr = GetGammaBin(dataArray).ToCharArray();

                    bitCriteria = (int)char.GetNumericValue(gammaCharArr[iteration]); //This is where the CO2 and Oxygen Methods differ.
                    if (bitCriteria == 0)
                        toRemove = 1;
                    else
                        toRemove = 0;

                    if (toRemove == 0)
                    {
                        newArrayLength = dataArray.GetLength(0) - CountZeros(dataArray, iteration);
                    }
                    else
                    {
                        newArrayLength = dataArray.GetLength(0) - CountOnes(dataArray, iteration);
                    }

                    tempArray = new int[newArrayLength, dataArray.GetLength(1)];


                    rowOld = 0;
                    rowNew = 0;

                    while (rowOld < dataArray.GetLength(0))
                    {

                        if (dataArray[rowOld, iteration] != toRemove)
                        {
                            for (int col = 0; col < dataArray.GetLength(1); col++)
                            {
                                tempArray[rowNew, col] = dataArray[rowOld, col];
                            }
                            rowNew++;
                        }

                        rowOld++;
                    }
                    dataArray = tempArray;


                }
                else
                    break;

                
            }


            int OxyLev = MatrixToDecimal(dataArray);
            
            return OxyLev;

        }

        private static int GetCO2(int[,] passedArray)
        {
            int toRemove;
            int bitCriteria;
            int newArrayLength;
            int[,] tempArray;
            int rowOld;
            int rowNew;
            string gammaStr;
            int[,] dataArray = passedArray;
            char[] gammaCharArr = new char[dataArray.GetLength(1)];

            

            for (int iteration = 0; iteration < dataArray.GetLength(1); iteration++)
            {
                if (dataArray.GetLength(0) > 1)
                {
                    gammaStr = GetGammaBin(dataArray);
                    gammaCharArr = GetGammaBin(dataArray).ToCharArray();

                    bitCriteria = (int)char.GetNumericValue(gammaCharArr[iteration]); //This is where the CO2 and Oxygen Methods differ.
                    if (bitCriteria == 0)
                        toRemove = 0;
                    else
                        toRemove = 1;

                    if (toRemove == 0)
                    {
                        newArrayLength = dataArray.GetLength(0) - CountZeros(dataArray, iteration);
                    }
                    else
                    {
                        newArrayLength = dataArray.GetLength(0) - CountOnes(dataArray, iteration);
                    }

                    tempArray = new int[newArrayLength, dataArray.GetLength(1)];


                    rowOld = 0;
                    rowNew = 0;

                    while (rowOld < dataArray.GetLength(0))
                    {

                        if (dataArray[rowOld, iteration] != toRemove)
                        {
                            for (int col = 0; col < dataArray.GetLength(1); col++)
                            {
                                tempArray[rowNew, col] = dataArray[rowOld, col];
                            }
                            rowNew++;
                        }

                        rowOld++;
                    }
                    dataArray = tempArray;
                    
                    
                }
                else
                    break;

                //DisplayArray(dataArray);
            }


            int CO2Lev = MatrixToDecimal(dataArray);
            //int CO2Lev = 55;
            return CO2Lev;

        }

        private static int MatrixToDecimal(int[,] dataArray)
        {
            string str = "";
            for (int col =0; col < dataArray.GetLength(1); col++)
            {
                str = str + dataArray[0, col];
            }
            return Convert.ToInt32(str, 2);
        }

        
    }
}