using System;

namespace SimpleWithMssql
{
    public static class Display
    {
        public static void FormatedResult(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(string.Format("Result: {0}", format), args);
            Console.ResetColor();
        }

        public static void Result(params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(string.Format("Result: " + string.Join(", ", args)));
            Console.ResetColor();
        }

        public static void Title([System.Runtime.CompilerServices.CallerMemberName] string title = "")
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nExample: {0}", title);
            Console.ResetColor();
        }
    }
}