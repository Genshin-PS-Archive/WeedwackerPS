using System.CommandLine;
using System.CommandLine.IO;
using System.Text;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands
{
    public class StaticConsole : IConsole
    {
        private readonly StringBuilder _sb;

        public IStandardStreamWriter Out { get; }
        public bool IsOutputRedirected => false;
        public IStandardStreamWriter Error { get; }
        public bool IsErrorRedirected => false;
        public bool IsInputRedirected => false;

        public StaticConsole()
        {
            _sb = new StringBuilder();

            Out = StandardStreamWriter.Create(new StringWriter(_sb));
            Error = StandardStreamWriter.Create(TextWriter.Null);
        }

        public string GetResult()
        {
            string result = _sb.ToString();
            _sb.Clear();

            return result;
        }
    }
}
