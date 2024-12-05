using System.Text.RegularExpressions;

namespace AoC2024
{
    internal class Day03
    {
        string input;
        public Day03()
        {
            input = InputReader.GetInput(nameof(Day03));
        }

        public string Part1()
        {
            return Regex.Matches(input, @"mul\([0-9]+,[0-9]+\)").Select(m =>
            {
                var matches = Regex.Matches(m.Value, @"[0-9]+").ToArray();
                return int.Parse(matches[0].Value) * int.Parse(matches[1].Value);
            }).Sum().ToString();
        }

        public string Part2()
        {
            var instructions = Regex.Matches(input, @"mul\([0-9]+,[0-9]+\)|do\(\)|don't\(\)").OrderBy(m => m.Index).ToList();            
            var mulEnabled = true;
            var enabledMuls = new List<int>();
            foreach (var instruction in instructions)
            {
                if (instruction.Value == "do()")
                {
                    mulEnabled = true;
                }
                else if (instruction.Value == "don't()")
                {
                    mulEnabled = false;
                }
                else if (mulEnabled)
                {
                    var matches = Regex.Matches(instruction.Value, @"[0-9]+").ToArray();
                    enabledMuls.Add(int.Parse(matches[0].Value) * int.Parse(matches[1].Value));
                }
            }
            return enabledMuls.Sum().ToString();
        }
    }
}
