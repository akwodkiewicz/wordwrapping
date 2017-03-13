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
            int[] lines;
            var res = WordWrapping(new int[4] { 4,2,1,7 },7,out lines);
            Console.WriteLine("Result: {0}",res);
            res = WordWrapping(new int[4] { 5,4,1,4 },7,out lines);
            Console.WriteLine("Result: {0}",res);
        }


        static int WordWrapping(int[] words,int maxChars,out int[] lines)
        {
            int len = words.Length;
            int[] wordsOne = new int[len + 1];
            for (int i = 1; i < len + 1; i++)
                wordsOne[i] = words[i - 1];
            wordsOne[0] = 0;

            int[,] cost = new int[len + 1,len + 1];
            lines = new int[len + 1];
            int[,] left = new int[len + 1,len + 1];

            //Counting which words can be in one line together
            for (int i = 1; i <= len; i++)
            {
                left[i,i] = maxChars - wordsOne[i];
                for (int j = i + 1; j <= len; j++)
                    left[i,j] = left[i,j - 1] - wordsOne[j] - 1;
            }
            for (int i = 1; i <= len; i++)
                for (int j = i; j <= len; j++)
                {
                    if (left[i,j] < 0)
                        cost[i,j] = int.MaxValue;
                    else
                    {
                        if (j == len) //it's the last line => cost==0
                            cost[i,j] = 0;
                        else
                            cost[i,j] = (int)Math.Pow(left[i,j],3.0);
                    }
                }
            for (int k = 1; k <= len; k++)
            {
                cost[0,k] = int.MaxValue;
                for (int q = 1; q <= k; q++)
                {
                    if (cost[0,k - 1] != int.MaxValue
                        && cost[q,k] != int.MaxValue
                        && (cost[0,q - 1] + cost[q,k] < cost[0,k]))
                    {
                        cost[0,k] = cost[0,q - 1] + cost[q,k];
                        lines[k] = q;
                    }
                }
            }

            return cost[0,len];

            /*for (int i = 0; i < len; i++)
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
                            tempcharsleft = charsLeft[q,j - 1] - lengths[j];
                            if (tempcharsleft < 0)
                                break;
                            costAbove = cost[i,q - 1];
                            costBelow = cost[q,j];
                        }
                        else
                        {
                            tempcharsleft = charsLeft[i,j - 1] - lengths[j];
                            costAbove = 0;
                            if (tempcharsleft < 0)
                                break;
                            costBelow = cost[i,j - 1] - Cubed(charsLeft[i,j - 1]) + Cubed(tempcharsleft - 1);
                        }
                        tempcost = costAbove + costBelow;

                        if (tempcost < cost[i,j])
                        {
                            firstWord[i,j] = q;
                            charsLeft[i,j] = tempcharsleft - 1;
                            cost[i,j] = tempcost;
                        }
                    }
                }
            }
            return cost[0,len - 1];*/

        }



        static int Cubed(double n)
        {
            return (int)Math.Pow(n,3);
        }
    }
}
