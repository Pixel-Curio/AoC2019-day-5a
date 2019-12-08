using System;

namespace Day_5a
{
    class Intcode
    {
        private int _pointer;
        private readonly int[] _code;

        private const string AddCommand = "01";
        private const string MultiplyCommand = "02";
        private const string InputCommand = "03";
        private const string OutputCommand = "04";
        private const string ExitCommand = "99";

        public Intcode(int[] code) => _code = code;

        public void Process()
        {
            while (true)
            {
                var command = ConsumeOpCode().ToString();
                var opCode = command.Length < 2 ? "0" + command : command[^2..];
                var parameters = command.Length > 2 ? command[..^2].PadLeft(4, '0') : "0000";

                if (opCode == AddCommand) Add(parameters);
                else if (opCode == MultiplyCommand) Multiply(parameters);
                else if (opCode == InputCommand) Input(parameters);
                else if (opCode == OutputCommand) Output(parameters);
                else if (opCode == ExitCommand) return;
            }
        }

        private void Add(string parameters)
        {
            int a = ConsumeOpCode();
            int b = ConsumeOpCode();
            int t = ConsumeOpCode();

            a = parameters[^1] == '0' ? _code.ExpandingGet(a) : a;
            b = parameters[^2] == '0' ? _code.ExpandingGet(b) : b;

            _code.ExpandingSet(t, a + b);
        }

        private void Multiply(string parameters)
        {
            int a = ConsumeOpCode();
            int b = ConsumeOpCode();
            int t = ConsumeOpCode();

            a = parameters[^1] == '0' ? _code.ExpandingGet(a) : a;
            b = parameters[^2] == '0' ? _code.ExpandingGet(b) : b;

            _code.ExpandingSet(t, a * b);
        }

        private void Input(string parameters)
        {
            int t = ConsumeOpCode();

            Console.WriteLine($"Waiting for input at ({t}): ");
            int input = Convert.ToInt32(Console.ReadLine());

            _code.ExpandingSet(t, input);
        }

        private void Output(string parameters)
        {
            int t = ConsumeOpCode();

            int value = parameters[^1] == '0' ? _code.ExpandingGet(t) : t;

            Console.WriteLine($"Value at ({t}): {value}");
        }

        public int GetValue(int index) => _code[index];

        private int ConsumeOpCode() => _pointer < _code.Length ? _code[_pointer++] : 99;
    }

    public static class ArrayExtensions
    {
        public static void ExpandingSet(this int[] source, int index, int value)
        {
            if (index > source.Length) Array.Resize(ref source, index + 1);
            source[index] = value;
        }
        public static int ExpandingGet(this int[] source, int index)
        {
            if (index > source.Length) Array.Resize(ref source, index + 1);
            return source[index];
        }
    }
}
