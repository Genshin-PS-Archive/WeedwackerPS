using System.Runtime.CompilerServices;
using System.Text;

namespace Weedwacker.Shared.Utils
{
    public static class Logger
    {
        private static readonly object ParseLock = new();
        private static readonly StringBuilder LineBuilder = new(3);
        private static readonly string[] DebugArgs = new string[3];

#if DEBUG
        public static void DebugWriteLine(string message)
        {
            Console.WriteLine(ParseMessage(message));
        }

        public static void DebugWriteWarningLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ParseMessage(message));
            Console.ResetColor();
        }
#endif

        public static void WriteLine(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.WriteLine(ParseMessage(message, memberName, fileName, lineNumber));
        }

        public static void WriteWarningLine(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ParseMessage(message, memberName, fileName, lineNumber));
            Console.ResetColor();
        }

        public static void WriteErrorLine(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Error.WriteLine(ParseMessage(message, memberName, fileName, lineNumber));
            Console.ResetColor();
        }

        public static void WriteErrorLine(string message, Exception e, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            WriteErrorLine(message, memberName, fileName, lineNumber);
            WriteErrorLine(e.Message);
            WriteErrorLine(e.StackTrace);
        }

        private static string ParseMessage(string message, string memberName = "", string filePath = "", int lineNumber = 0)
        {
            lock (ParseLock)
            {
                LineBuilder.Clear();
                LineBuilder.Append($"[{DateTime.UtcNow:yy-MM-dd HH:mm:ss}]");

                int debugArgs = 0;

                string fileName = filePath.Split("\\").Last();
                if (!string.IsNullOrEmpty(fileName))
                    DebugArgs[debugArgs++] = fileName;
                if (!string.IsNullOrEmpty(memberName))
                    DebugArgs[debugArgs++] = memberName;
                if (lineNumber > 0)
                    DebugArgs[debugArgs++] = $"L{lineNumber}";

                if (debugArgs > 0)
                    LineBuilder.Append($"<{string.Join(':', DebugArgs[..debugArgs])}>");

                LineBuilder.Append($" {message}");
                string line = LineBuilder.ToString();

                return line;
            }
        }
    }
}