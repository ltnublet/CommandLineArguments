using System;
using Drexel.Arguments;

namespace Arguments.Test.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            args = new String[] { "-s", "0", "-u", @"HELLO\world", "-e" };

            UserParameters parameters = new UserParameters();
            bool helpRequested = Context.Initialize(args, new string[] { "-" }, "?");

            Console.WriteLine($"{parameters.StatusCode}, {parameters.Username}, {parameters.Execute}");
            Console.ReadLine();
        }
    }
}
