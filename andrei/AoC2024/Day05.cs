using System.Text.RegularExpressions;

namespace AoC2024
{
    internal class Day05
    {
        string input;
        Dictionary<int, List<int>> orderingRules;
        int[][] updates;
        public Day05()
        {
            input = InputReader.GetInput(nameof(Day05));
            var parts = input.Split("\r\n\r\n");
            var rules = parts[0].Split("\r\n");
            orderingRules = new Dictionary<int, List<int>>();
            foreach (var rule in rules)
            {
                var pages = rule.Split("|").Select(p => int.Parse(p)).ToArray();
                if (orderingRules.ContainsKey(pages[0]))
                {
                    orderingRules[pages[0]].Add(pages[1]);
                }
                else
                {
                    orderingRules.Add(pages[0], new List<int> { pages[1] });
                }
            }
            updates = parts[1].Split("\r\n").Select(u => u.Split(',').Select(n => int.Parse(n)).ToArray()).ToArray();
        }

        public string Part1()
        {
            var correctMiddles = new List<int>();
            var comparer = new CustomPageComparer(orderingRules);
            foreach (var update in updates)
            {
                var orderedUpdate = update.Order(comparer).ToList();
                if (orderedUpdate.SequenceEqual(update))
                {
                    correctMiddles.Add(update[update.Length / 2]);
                }
            }
            return correctMiddles.Sum().ToString();
        }

        public string Part2()
        {
            var correctedMiddles = new List<int>();
            var comparer = new CustomPageComparer(orderingRules);
            foreach (var update in updates)
            {
                var orderedUpdate = update.Order(comparer).ToArray();
                if (!orderedUpdate.SequenceEqual(update))
                {
                    correctedMiddles.Add(orderedUpdate[orderedUpdate.Length / 2]);
                }
            }
            return correctedMiddles.Sum().ToString();
        }

        private class CustomPageComparer : IComparer<int>
        {
            Dictionary<int, List<int>> orderingRules;
            public CustomPageComparer(Dictionary<int, List<int>> orderingRules)
            {
                this.orderingRules = orderingRules;
            }

            public int Compare(int x, int y)
            {
                if (orderingRules.ContainsKey(x) && orderingRules[x].Contains(y))
                {
                    return -1;
                }
                else if (orderingRules.ContainsKey(y) && orderingRules[y].Contains(x))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
