﻿using System;

namespace Transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args) {
                Console.WriteLine(StructuredTextToVisualBasic.Parse(arg));
            }
        }
    }
}
