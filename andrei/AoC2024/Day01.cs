namespace AoC2024
{
    internal class Day01
    {
        string input;
        List<int> left = new List<int>();
        List<int> right = new List<int>();
        public Day01()
        {
            input = InputReader.GetInput(nameof(Day01));

            foreach (var line in input.Split('\n'))
            {
                var lineParts = line.Split("   ");
                left.Add(int.Parse(lineParts[0]));
                right.Add(int.Parse(lineParts[1]));
            }
            left.Sort();
            right.Sort();
        }

        public string Part1()
        {
            var distance = 0;
            for (int i = 0; i < left.Count; i++)
            {
                distance += Math.Abs(left[i] - right[i]);
            }
            return distance.ToString();
        }


        public string Part2()
        {
            var simmilarity = 0;
            var leftGroup = left.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var rightGroup = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            leftGroup.Keys.ToList().ForEach(x =>
            {
                if(rightGroup.ContainsKey(x))
                {
                    simmilarity += x*leftGroup[x] * rightGroup[x];
                }
            });
            return simmilarity.ToString();
        }
    }
}
