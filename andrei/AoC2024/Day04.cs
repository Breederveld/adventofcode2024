using System.Text.RegularExpressions;

namespace AoC2024
{
    internal class Day04
    {
        string input;
        public Day04()
        {
            input = InputReader.GetInput(nameof(Day04));
        }

        public string Part1()
        {
            var lines = input.Split('\n');
            var count = 0;
            var regex = @"(?=(SAMX|XMAS))";
            for (int i = 0; i < lines.Length; i++)
            {
                //Count Horizontal
                count += Regex.Matches(lines[i], regex).Count;
                //Count Vertical
                var vertical = new string(lines.Select(lines => lines[i]).ToArray());
                count += Regex.Matches(vertical, regex).Count;

                //Count main diagonals
                if (i == 0)
                {
                    var diagonal1 = new List<char>();
                    for (int k = 0; k < lines.Length; k++)
                    {
                        diagonal1.Add(lines[k][k]);
                    }
                    count += Regex.Matches(new string(diagonal1.ToArray()), regex).Count;
                    var diagonal2 = new List<char>();
                    for (int k = 0; k < lines.Length; k++)
                    {
                        diagonal2.Add(lines[k][lines.Length - 1 - k]);
                    }
                    count += Regex.Matches(new string(diagonal2.ToArray()), regex).Count;
                }

                //Count small diagonals
                if (i > 0 && i < lines.Length - 1)
                {
                    var diagonalRight1 = new List<char>();
                    var diagonalRight2 = new List<char>();
                    var diagonalLeft1 = new List<char>();
                    var diagonalLeft2 = new List<char>();
                    for (int k = i; k < lines.Length; k++)
                    {
                        diagonalRight1.Add(lines[k - i][k]);
                        diagonalRight2.Add(lines[k][k - i]);

                        diagonalLeft1.Add(lines[k - i][lines.Length - 1 - k]);
                        diagonalLeft2.Add(lines[lines.Length - 1 - (k - i)][k]);
                    }
                    count += Regex.Matches(new string(diagonalRight1.ToArray()), regex).Count;
                    count += Regex.Matches(new string(diagonalRight2.ToArray()), regex).Count;
                    count += Regex.Matches(new string(diagonalLeft1.ToArray()), regex).Count;
                    count += Regex.Matches(new string(diagonalLeft2.ToArray()), regex).Count;

                }
            }
            return count.ToString();
        }

        public string Part2()
        {
            var count = 0;
            var lines = input.Split('\n');
            for (int i = 0; i < lines.Length - 2; i++)
            {
                for (int j = 0; j < lines.Length - 2; j++)
                {
                    string diagonal1 = "" + lines[i][j] + lines[i + 1][j + 1] + lines[i + 2][j + 2];
                    string diagonal2 = "" + lines[i][j + 2] + lines[i + 1][j + 1] + lines[i + 2][j];
                    if (Regex.IsMatch(diagonal1, "SAM|MAS") && Regex.IsMatch(diagonal2, "MAS|SAM"))
                    {
                        count++;
                    }
                }
            }
            return count.ToString();
        }

    }
}
