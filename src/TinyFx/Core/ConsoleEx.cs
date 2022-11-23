using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Text;

namespace TinyFx
{
    public static class ConsoleEx
    {
        private static object _locker = new object();
        public static void WriteLine(string input, ConsoleColor textColor = default, ConsoleColor bgColor = default)
        {
            lock (_locker)
            {
                if (textColor != default)
                    Console.ForegroundColor = textColor;
                if (bgColor != default)
                    Console.BackgroundColor = bgColor;
                Console.Write(input);
                Console.ResetColor();
                Console.WriteLine();
                //Console.WriteLine(input.Pastel(textColor, bgColor));
            }
        }
        public static void Write(string input, ConsoleColor textColor = default, ConsoleColor bgColor = default)
        {
            lock (_locker)
            {
                if (textColor != default)
                    Console.ForegroundColor = textColor;
                if (bgColor != default)
                    Console.BackgroundColor = bgColor;
                Console.Write(input);
                Console.ResetColor();
                //Console.Write(input.Pastel(textColor, bgColor));
            }
        }
        /// <summary>
        /// 输出当前行，用完记得 Console.WriteLine()
        /// </summary>
        /// <param name="input"></param>
        /// <param name="textColor"></param>
        /// <param name="bgColor"></param>
        public static void WriteCurrentLine(string input, ConsoleColor textColor = default, ConsoleColor bgColor = default)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Write(input, textColor, bgColor);
        }
    }
}
