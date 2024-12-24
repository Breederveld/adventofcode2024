using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2024_24_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Trim().Split("\n").Select(s => s.TrimEnd()).ToArray();
            sw.Start();

            var states = new Dictionary<string, bool>();
            foreach (var line in strings.TakeWhile(l => l != string.Empty))
            {
                var s = line.Split(": ");
                states[s[0]] = s[1] == "1";
            }

            var gateRegex = new Regex(@"(?<left>\w+) (?<op>\w+) (?<right>\w+) -> (?<out>\w+)$");
            var gates = new List<Gate>();
            foreach (var line in strings.SkipUntil(l => l == string.Empty))
            {
                var match = gateRegex.Match(line);
                gates.Add(new Gate(
                    match.Groups["left"].Value,
                    match.Groups["right"].Value,
                    match.Groups["op"].Value,
                    match.Groups["out"].Value));
            }

            while (gates.Any())
            {
                foreach (var gate in gates.ToArray())
                {
                    if (states.ContainsKey(gate.Left) && states.ContainsKey(gate.Right))
                    {
                        var left = states[gate.Left];
                        var right = states[gate.Right];
                        var output = false;
                        switch (gate.Op)
                        {
                            case "AND":
                                output = left & right;
                                break;
                            case "OR":
                                output = left | right;
                                break;
                            case "XOR":
                                output = left ^ right;
                                break;
                        }
                        states[gate.Output] = output;
                        gates.Remove(gate);
                    }
                }
            }

            var result = 0L;
            for (var i = 100; i >= 0; i--)
            {
                var state = $"z{i:d2}";
                if (states.ContainsKey(state))
                {
                    result = result * 2 + (states[state] ? 1 : 0);
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }

        private record Gate(
            string Left, string Right, string Op, string Output);
    }
}