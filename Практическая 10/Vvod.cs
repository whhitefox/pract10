namespace Pract10
{
    public class Vvod
    {
        public static string GetValue(string current, bool secure = false)
        {
            Console.CursorVisible = true;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            int maxLenght = Console.WindowWidth - 1;
            Console.SetCursorPosition(left + current.Length, top);
            string value = current;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != (ConsoleKey)HotKeys.Submit)
            {
                if (key.Key == ConsoleKey.Backspace && value.Length > 0)
                {
                    Console.SetCursorPosition(left + value.Length - 1, top);
                    Console.Write(" ");
                    value = value.Remove(value.Length - 1, 1);
                    Console.SetCursorPosition(left + value.Length, top);
                } else if (key.Key != ConsoleKey.Backspace && value.Length + left < maxLenght)
                {
                    Console.Write(secure ? "*" : key.KeyChar);
                    value += key.KeyChar;
                }

                key = Console.ReadKey(true);
            }
            Console.CursorVisible = false;
            return value;
        }
    }
}
