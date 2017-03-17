using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Seventh_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day7instructions.txt");
            List<string> inString = instructions.ToList();
            processsCicrut(ref inString);
            Console.ReadLine();
        }
        private static void processsCicrut(ref List<string> inString)
        {
            List<string> variablesList = new List<string>();
            List<int> variableValuesList = new List<int>();
            string firstPart, goesInto, num, firstVar, oper, secondVar;
            // get a list of variables
            foreach (string line in inString)
            {
                // first one
                string firtVar = new string(line.TakeWhile(c => char.IsLower(c)).ToArray());
                // last one
                string lastVar = new string((Regex.Split(line, " -> ")[1])
                    .Where(c => char.IsLower(c)).ToArray());
                // midle one
                string middleVar = new string(Regex.Split(line, " -> ")[0]
                    .Skip(firtVar.Length)
                    .Where(c => char.IsLower(c)).ToArray());
                // add each to the list if there isn't already one
                if (!variablesList.Contains(lastVar) && lastVar.Length > 0)
                {
                    variablesList.Add(lastVar);
                    variableValuesList.Add(int.MaxValue);
                }
                if (!variablesList.Contains(middleVar) && middleVar.Length > 0)
                {
                    variablesList.Add(middleVar);
                    variableValuesList.Add(int.MaxValue);
                }
                if (!variablesList.Contains(firtVar) && firtVar.Length > 0)
                {
                    variablesList.Add(firtVar);
                    variableValuesList.Add(int.MaxValue);
                }
            }
            while (inString.Count() > 1)
            {
                List<int> toRemove = new List<int>();
                foreach (string line in inString)
                {
                    setupStrings(line, out firstPart, out goesInto, out num, out firstVar, out oper, out secondVar);
                    // number asignment
                    if (num.Length > 0 && oper.Length == 0)
                    {
                        int index = variablesList.IndexOf(goesInto);
                        variableValuesList[index] = int.Parse(num);
                        toRemove.Add(inString.IndexOf(line));
                    }
                }
                foreach (string line in inString)
                {
                    setupStrings(line, out firstPart, out goesInto, out num, out firstVar, out oper, out secondVar);
                    int firstVarIndex = variablesList.IndexOf(firstVar);
                    int secondVarIndex = variablesList.IndexOf(secondVar);
                    int index = variablesList.IndexOf(goesInto);
                    string firstVarNot = new string(firstPart.Where(c => char.IsLower(c)).ToArray());
                    int firstVarIndexNot = variablesList.IndexOf(firstVarNot);
                    // if one value is known and the other is a number
                    if (num.Length > 0 && secondVarIndex != -1 && variableValuesList.ElementAt(secondVarIndex) != int.MaxValue)
                    {
                        switch (oper)
                        {
                            case "AND":
                                variableValuesList[index] = int.Parse(num) & variableValuesList[secondVarIndex];
                                toRemove.Add(inString.IndexOf(line));
                                break;
                            case "OR":
                                variableValuesList[index] = int.Parse(num) | variableValuesList[secondVarIndex];
                                toRemove.Add(inString.IndexOf(line));
                                break;
                        }
                    }
                    // if both values are known
                    else if (firstVarIndex != -1 && variableValuesList.ElementAt(firstVarIndex) != int.MaxValue && secondVarIndex != -1 && variableValuesList.ElementAt(secondVarIndex) != int.MaxValue)
                    {
                        switch (oper)
                        {
                            case "AND":
                                variableValuesList[index] = variableValuesList[firstVarIndex] & variableValuesList[secondVarIndex];
                                toRemove.Add(inString.IndexOf(line));
                                break;
                            case "OR":
                                variableValuesList[index] = variableValuesList[firstVarIndex] | variableValuesList[secondVarIndex];
                                toRemove.Add(inString.IndexOf(line));
                                break;
                        }
                    }
                    // for shift and not operators
                    else if (oper.Contains("SHIFT") || oper == "NOT")
                    {
                        if ((firstVarIndex == -1 || variableValuesList.ElementAt(firstVarIndex) == int.MaxValue) && (firstVarIndexNot == -1 || variableValuesList.ElementAt(firstVarIndexNot) == int.MaxValue))
                            continue;
                        switch (oper)
                        {
                            case "LSHIFT":
                                variableValuesList[index] = variableValuesList[firstVarIndex] << int.Parse(secondVar);
                                toRemove.Add(inString.IndexOf(line));
                                break;
                            case "RSHIFT":
                                variableValuesList[index] = variableValuesList[firstVarIndex] >> int.Parse(secondVar);
                                toRemove.Add(inString.IndexOf(line));
                                break;
                            case "NOT":
                                variableValuesList[index] = 65535 - variableValuesList[firstVarIndexNot];
                                toRemove.Add(inString.IndexOf(line));
                                break;
                        }
                    }
                }
                toRemove.Sort();
                toRemove.Reverse();
                foreach (int i in toRemove)
                {
                    inString.RemoveAt(i);
                }
            }
            for (int i = 0; i < variablesList.Count(); i++)
            {
                Console.WriteLine($"Variable: {variablesList[i]}\tValue: {variableValuesList[i]}");
            }
            Console.WriteLine($"A value: { variableValuesList[154]}");
        }
        private static void setupStrings(string line, out string firstPart, out string goesInto, out string num, out string firstVar, out string oper, out string secondVar)
        {
            firstPart = Regex.Split(line, " -> ")[0];
            goesInto = Regex.Split(line, " -> ")[1];
            num = new string(firstPart.TakeWhile(c => char.IsDigit(c)).ToArray());
            firstVar = new string(firstPart.TakeWhile(c => char.IsLower(c)).ToArray());
            oper = new string(firstPart.Where(c => char.IsUpper(c)).ToArray());
            secondVar = new string(firstPart
                .Skip(num.Length + firstVar.Length + oper.Length + 2)
                .TakeWhile(c => char.IsLetterOrDigit(c))
                .ToArray());
        }
    }
}