﻿using System;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerHTTP(@"C:\Users\Andreea\source\repos\JSONClasses\json.txt");
            Console.WriteLine("Hello World!");
        }
    }
}
