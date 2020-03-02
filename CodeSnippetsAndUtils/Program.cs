using CodeSnippetsAndUtils.Collections;
using CodeSnippetsAndUtils.Enums;
using System;
using System.Collections.Generic;

namespace CodeSnippetsAndUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[10000];
            test.RapidFill(2);

            string[] toWrite = new string[20];
            for (int i = 0; i < 20; i++) toWrite[i] = test[i].ToString();

            Console.WriteLine(string.Join(", ", toWrite));
            Console.ReadLine();
        }
    }
}
