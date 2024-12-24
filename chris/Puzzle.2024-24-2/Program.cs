using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2024_24_2
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

            var swaps = new Dictionary<string, (string, int)>
            {
                ["z09"] = ("nnf", 2),
                ["nnf"] = ("z09", 2),
                ["z20"] = ("nhs", 2),
                ["nhs"] = ("z20", 2),
                ["kqh"] = ("ddn", 2),
                ["ddn"] = ("kqh", 2),
                ["z34"] = ("wrc", 2),
                ["wrc"] = ("z34", 2),
            };
            var replace = new Func<string, int, string>((str, pos) => swaps.ContainsKey(str) && pos == swaps[str].Item2 ? swaps[str].Item1 : str);

            var gateRegex = new Regex(@"(?<left>\w+) (?<op>\w+) (?<right>\w+) -> (?<out>\w+)");
            var gates = new List<Gate>();
            foreach (var line in strings.SkipUntil(l => l == string.Empty))
            {
                var match = gateRegex.Match(line);
                gates.Add(new Gate(
                    replace(match.Groups["left"].Value, 0),
                    replace(match.Groups["right"].Value, 1),
                    match.Groups["op"].Value,
                    replace(match.Groups["out"].Value, 2)));
            }
            var origGates = gates.ToArray();

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

            for (var i = 0; i < 100; i++)
            {
                var state = $"z{i:d2}";
                if (states.ContainsKey(state))
                {
                    var formula = GetFormula(origGates, state);
                    Console.WriteLine($"{state}: {formula.Substring(0, Math.Min(formula.Length, 2000))}");
                }
            }

            var result = string.Join(",", swaps.Keys.OrderBy(s => s));
            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }

        private static Dictionary<string, string> OpShorthands = new Dictionary<string, string>
        {
            ["AND"] = "&",
            ["OR"] = "|",
            ["XOR"] = "^",
        };
        private static string GetFormula(Gate[] gates, string output)
        {
            var gate = gates.FirstOrDefault(g => g.Output == output);
            if (gate == null)
            {
                return output;
            }
            if (gate.Left.Substring(1) == gate.Right.Substring(1))
            {
                return OpShorthands[gate.Op] + gate.Left.Substring(1);
            }
            var left = GetFormula(gates, gate.Left);
            var right = GetFormula(gates, gate.Right);
            if (left.Length > right.Length)
            {
                var tmp = left;
                left = right;
                right = tmp;
            }
            return $"({output}: {left} {gate.Op} {right})";
        }

        private record Gate(string Left, string Right, string Op, string Output);
    }
}