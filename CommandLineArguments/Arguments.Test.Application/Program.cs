using System;
using Drexel.Arguments;

namespace Arguments.Test.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            args = new String[] { "-s", "0", "-u", @"HELLO\world", "-e", "-b", "This is a test", "-c", "This", "is", "a", "test" };

            UserParameters parameters = new UserParameters();

            ArgumentGroup manual = new ArgumentGroup(
                new ManualArgument(
                    "aaaaa", 
                    "a", 
                    "Example Description 1", 
                    () => Console.WriteLine("Occurs when supplied"),
                    () => Console.WriteLine("Occurs when not supplied")),
                new ManualArgument(
                    "bbbbb",
                    "b",
                    "Example Value",
                    "Example Description 2",
                    x => Console.WriteLine($"Test: {x}")),
                new ManualArgument(
                    "ccccc",
                    "c",
                    "Example Value 2",
                    "Example Description 3",
                    4,
                    x => Console.WriteLine($"Test: {string.Join(" ", x)}")));
            
            bool helpRequested = Context.Initialize(args, new string[] { "-" }, "?", manual);

            Context.Invoke();

            Console.WriteLine($"{parameters.StatusCode}, {parameters.Username}, {parameters.Execute}");

            Console.ReadLine();
        }
    }
}
