using System;

namespace Eleventh_day
{
    static class Program
    {
        static void Main(string[] args)
        {
            string instructions = "cqjxjnds";
            Console.WriteLine(genPassword(instructions));
            Console.ReadLine();
        }
        private static string genPassword(string input)
        {
            char[] password = input.ToCharArray();
            do
            {
                increment(ref password);
            } while (!isValid(ref password));
            return new string(password);
        }
        private static bool isValid(ref char[] password)
        {
            // does it contain i o l
            foreach (char c in password)
            {
                if (c == 'i' || c == 'o' || c == 'l')
                    return false;
            }
            // does it have 3 consequtive letters
            bool consequitve = false;
            for (int i = password.Length - 2; i > 1; i--)
            {
                // work from right to left
                if (password[i - 2] == password[i - 1] - 1 && password[i - 1] == password[i] - 1)
                {
                    consequitve = true;
                    break;
                }
            }
            // does it contain 2 sets of the same letters
            bool sameLetters = false;
            for (int i = password.Length - 1; i > 0; i--)
            {
                if (password[i] == password[i - 1])
                {
                    // right to left again
                    for (int j = i - 2; j > 0; j--)
                    {
                        if (password[j] == password[j - 1])
                        {
                            sameLetters = true;
                            break;
                        }
                    }
                }
            }
            return consequitve && sameLetters;
        }
        private static void increment(ref char[] password, int index = -1)
        {
            index = index == -1 ? password.Length - 1 : index;
            // increment the password
            password[index]++;
            // increment again if it's i, o or l
            if (password[index] == 9 || password[index] == 12 || password[index] == 15)
                password[index]++;
            // return if it's z or below
            if (password[index] <= 'z')
                return;
            // if it's z roll it over to a
            password[index] = 'a';
            increment(ref password, index - 1);
        }
        private static string generatePasswordOne(string input)
        {
            // from string to int[]
            char[] inputArray = input.ToCharArray();
            int[] passArray = new int[input.Length];
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = inputArray[i] % 32;
            }
            // increment 9, 12 and 15
            incrementWrongLetters(ref passArray);
            // get differences between each consecutive value
            int[] diffArray = new int[passArray.Length - 1];
            int[] bigDiffArray = new int[passArray.Length - 1];
            getDifferences(passArray, ref diffArray, ref bigDiffArray);
            // get 2 closest numbers at least 2 indexes apart
            int min = int.MaxValue;
            int[] mins = new int[2];
            findPairs(diffArray, bigDiffArray, ref min, ref mins);
            // increment these numbers to make pairs
            makePairs(mins, diffArray, ref passArray);
            // update differences
            getDifferences(passArray, ref diffArray, ref bigDiffArray);
            // now find 3 closest incrementing numbers
            int pair = int.MaxValue;
            int pairs = -1;
            findIncrements(diffArray, bigDiffArray, mins, ref pair, ref pairs);
            if (pairs == -1)
            {
                return "error";
            }
            // now increment these numbers
            int[] tempPass = new int[passArray.Length];
            makeIncrement(pairs, ref tempPass, ref passArray);
            char[] password = new char[passArray.Length];
            for (int i = 0; i < passArray.Length; i++)
            {
                password[i] = (char)(passArray[i] + 64);
            }
            return new string(password);
        }
        private static string generatePasswordTwo(string input)
        {
            char[] inputArray = input.ToCharArray();
            int[] passArray = new int[input.Length];
            int min = int.MaxValue;
            int[] mins = new int[2];
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = inputArray[i] % 32;
            }
            // increment 9, 12 and 15
            incrementWrongLetters(ref passArray);
            // get differences between each consecutive value
            int[] diffArray = new int[passArray.Length - 1];
            int[] bigDiffArray = new int[passArray.Length - 1];
            getDifferences(passArray, ref diffArray, ref bigDiffArray);
            // now find 3 closest incrementing numbers
            int pair = int.MaxValue;
            int pairs = -1;
            for (int i = 0; i < diffArray.Length - 1; i++)
            {
                int current = bigDiffArray[i] + bigDiffArray[i + 1];
                if (current < pair)
                {
                    pair = current;
                    pairs = i;
                }
            }
            if (pairs == -1)
            {
                return "error";
            }
            // now increment these numbers
            int[] tempPass = new int[passArray.Length];
            makeIncrement(pairs, ref tempPass, ref passArray);
            // get 2 closest numbers at least 2 indexes apart
            for (int i = 0; i < diffArray.Length - 2; i++)
            {
                if (i != pairs && i != pairs + 1 && i != pairs + 2)
                {
                    for (int j = 2; j < diffArray.Length; j++)
                    {
                        if (j != pairs && j != pairs + 1 && j != pairs + 2)
                        {
                            int current = bigDiffArray[i] + bigDiffArray[j];
                            if (current < min && (j - i > 2 || i - j > 2))
                            {
                                min = current;
                                mins[0] = i;
                                mins[1] = j;
                            }
                        }
                    }
                }
            }
            // increment these numbers to make pairs
            makePairs(mins, diffArray, ref passArray);
            // update differences
            getDifferences(passArray, ref diffArray, ref bigDiffArray);
            char[] password = new char[passArray.Length];
            for (int i = 0; i < passArray.Length; i++)
            {
                password[i] = (char)(passArray[i] + 64);
            }
            return new string(password);
        }
        private static void incrementWrongLetters(ref int[] passArray)
        {
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = (passArray[i] == 9 || passArray[i] == 12 || passArray[i] == 15) ? passArray[i] + 1 : passArray[i];
            }
        }
        private static void getDifferences(int[] passArray, ref int[] diffArray, ref int[] bigDiffArray)
        {
            for (int i = 0; i < passArray.Length - 1; i++)
            {
                diffArray[i] = passArray[i + 1] > passArray[i] ? passArray[i + 1] - passArray[i] : passArray[i] - passArray[i + 1];
            }
            Array.Copy(diffArray, bigDiffArray, diffArray.Length);
            for (int i = 0; i < passArray.Length - 1; i++)
            {
                bigDiffArray[i] = diffArray[i] > 13 ? 26 - diffArray[i] : diffArray[i];
            }
        }
        private static void findPairs(int[] diffArray, int[] bigDiffArray, ref int min, ref int[] mins)
        {
            for (int i = 0; i < diffArray.Length - 2; i++)
            {
                for (int j = 2; j < diffArray.Length; j++)
                {
                    int current = bigDiffArray[i] + bigDiffArray[j];
                    if (current < min && (j - i > 2 || i - j > 2))
                    {
                        min = current;
                        mins[0] = i;
                        mins[1] = j;
                    }
                }
            }
        }
        private static void findIncrements(int[] diffArray, int[] bigDiffArray, int[]mins, ref int pair, ref int pairs)
        {
            for (int i = 0; i < diffArray.Length - 1; i++)
            {
                if (i != mins[0] && i != mins[1] && i != mins[0] + 1 && i != mins[1] + 1 &&
                    i + 1 != mins[0] && i + 1 != mins[1] && i + 1 != mins[0] + 1 && i + 1 != mins[1] + 1 &&
                    i + 2 != mins[0] && i + 2 != mins[1] && i + 2 != mins[0] + 1 && i + 2 != mins[1] + 1)
                {
                    int current = bigDiffArray[i] + bigDiffArray[i + 1];
                    if (current < pair)
                    {
                        pair = current;
                        pairs = i;
                    }
                }
            }
        }
        private static void makePairs(int[] mins, int[] diffArray, ref int[] passArray)
        {
            for (int i = 0; i < mins.Length; i++)
            {
                if (diffArray[mins[0]] < 14)
                {
                    if (passArray[mins[i]] > passArray[mins[i] + 1])
                    {
                        passArray[mins[i] + 1] = passArray[mins[i]];
                    }
                    else
                    {
                        passArray[mins[i]] = passArray[mins[i] + 1];
                    }
                }
                else
                {
                    if (passArray[mins[i]] < passArray[mins[i] + 1])
                    {
                        passArray[mins[i] + 1] = passArray[mins[i]];
                    }
                    else
                    {
                        passArray[mins[i]] = passArray[mins[i] + 1];
                    }
                }
            }
        }
        private static void makeIncrement(int pairs, ref int[] tempPass, ref int[] passArray)
        {
            for (int i = 0; i < tempPass.Length; i++)
            {
                tempPass[i] = passArray[i] > 13 ? 27 - passArray[i] : passArray[i];
            }
            if (tempPass[pairs] > tempPass[pairs + 1] && tempPass[pairs] > tempPass[pairs + 2])
            {
                passArray[pairs + 1] = passArray[pairs] + 1;
                passArray[pairs + 2] = passArray[pairs] + 2;
            }
            else if (tempPass[pairs + 1] > tempPass[pairs] && tempPass[pairs + 1] > tempPass[pairs + 2])
            {
                passArray[pairs] = passArray[pairs + 1] - 1;
                passArray[pairs + 2] = passArray[pairs + 1] + 1;
            }
            else
            {
                passArray[pairs] = passArray[pairs + 2] - 2;
                passArray[pairs + 1] = passArray[pairs + 2] - 1;
            }
        }
    }
}
