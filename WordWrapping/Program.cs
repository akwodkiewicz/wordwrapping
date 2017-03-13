using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordWrapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = WordWrapping(new int[4] { 4,2,1,7 },7);
            Console.WriteLine("Result: {0}",res);
            res = WordWrapping(new int[4] { 5,4,1,4 },7);
            Console.WriteLine("Result: {0}",res);
        }


        static int WordWrapping(int[] lengths,int maxChars)
        {
            int len = lengths.Length, costBelow, costAbove, tempcharsleft, tempcost;
            int[,] cost = new int[len,len];
            int[,] firstWord = new int[len,len];
            int[,] charsLeft = new int[len,len];

            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                {
                    charsLeft[i,j] = maxChars;
                    cost[i,j] = int.MaxValue;
                }
            for (int i = 0; i < len; i++)
            {
                firstWord[i,i] = i;
                charsLeft[i,i] = maxChars - lengths[i];
                cost[i,i] = Cubed(charsLeft[i,i]);
            }

            for (int numOfWords = 2; numOfWords <= len; numOfWords++)
            {
                //i: first word
                for (int i = 0; i + numOfWords <= len; i++)
                {
                    int j = i + numOfWords - 1;
                    for (int q = j; q >= i; q--)
                    {
                        costBelow = cost[q,j];
                        if (q != i)
                        {
                            tempcharsleft = charsLeft[q,j-1] - lengths[j];
                            if (tempcharsleft < 0)
                                break;
                            costAbove = cost[i,q - 1];
                            costBelow = cost[q,j];
                        }
                        else
                        {
                            tempcharsleft = charsLeft[i,j-1] - lengths[j];
                            costAbove = 0;
                            if (tempcharsleft < 0)
                                break;
                            costBelow = cost[i,j - 1] - Cubed(charsLeft[i,j-1])+ Cubed(tempcharsleft-1);
                        }
                        tempcost = costAbove + costBelow;

                        if (tempcost < cost[i,j])
                        {
                            firstWord[i,j] = q;
                            charsLeft[i,j] = tempcharsleft-1;
                            cost[i,j] = tempcost;
                        }
                    }
                }
            }
            return cost[0,len - 1];
        }

       

        static int Cubed(double n)
        {
            return (int)Math.Pow(n, 3);
        }
    }
}
