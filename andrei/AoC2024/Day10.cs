using System.Numerics;

namespace AoC2024
{
    internal class Day10
    {
        string input;
        int[][] map;
        List<(int y, int x)> trailHeads = new List<(int y, int x)>();
        public Day10()
        {
            input = InputReader.GetInput(nameof(Day10));
            map = input.Split("\r\n").Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 0)
                    {
                        trailHeads.Add((y, x));
                    }
                }
            }
        }

        public string Part1()
        {
            var sum = 0;
            foreach (var head in trailHeads)
            {
                var summits = new List<string>();
                FindSummits(head, summits);
                sum += summits.Distinct().Count();
            }
            return sum.ToString();
        }

        public string Part2()
        {
            var sum = 0;
            foreach (var head in trailHeads)
            {
                sum += GetDistinctTrails(head);
            }
            return sum.ToString();
        }

        private void FindSummits((int y, int x) head, List<string> summits)
        {
            if (map[head.y][head.x] == 9)
            {
                summits.Add($"{head.y},{head.x}");
            }
            var nextSteps = this.GetNextSteps(head);
            foreach (var nextStep in nextSteps)
            {
                FindSummits(nextStep, summits);
            }
        }

        private int GetDistinctTrails((int y, int x) head)
        {
            if (map[head.y][head.x] == 9)
            {
                return 1;
            }
            var nextSteps = this.GetNextSteps(head);
            if (nextSteps.Count == 0) { return 0; }
            var count = 0;
            foreach (var nextStep in nextSteps)
            {
                count += GetDistinctTrails(nextStep);
            }
            return count;
        }

        private List<(int y, int x)> GetNextSteps((int y, int x) head)
        {
            var steps = new List<(int y, int x)>();
            if (head.y > 0) { steps.Add((head.y - 1, head.x)); }
            if (head.y < map.Length - 1) { steps.Add((head.y + 1, head.x)); }
            if (head.x > 0) { steps.Add((head.y, head.x - 1)); }
            if (head.x < map[head.y].Length - 1) { steps.Add((head.y, head.x + 1)); }
            return steps.Where(step => map[step.y][step.x] == map[head.y][head.x] + 1).ToList();
        }
    }
}
