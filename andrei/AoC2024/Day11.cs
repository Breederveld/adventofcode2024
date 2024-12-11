namespace AoC2024
{
    internal class Day11
    {
        string input;
        List<long> stones = new List<long>();
        public Day11()
        {
            input = InputReader.GetInput(nameof(Day11));
            stones = input.Split(' ').Select(long.Parse).ToList();
        }

        public string Part1()
        {
            return CountStones(25).ToString();
        }


        public string Part2()
        {
            return CountStones(75).ToString();
        }

        private long CountStones(int blinks)
        {
            var stones = input.Split(' ').Select(long.Parse).ToDictionary(x => x, x => 1L);
            for (int b = 0; b < blinks; b++)
            {
                var keys = stones.Keys.ToList();
                var newStones = new Dictionary<long, long>();
                foreach (var stone in keys)
                {
                    if (stone == 0)
                    {
                        if (!newStones.ContainsKey(1))
                            newStones[1] = 0;
                        newStones[1] += stones[stone];
                    }
                    else
                    {
                        var number = stone.ToString();
                        if (number.Length % 2 == 0)
                        {
                            var left = long.Parse(number.Substring(0, number.Length / 2));
                            var right = long.Parse(number.Substring(number.Length / 2, number.Length / 2));
                            if (!newStones.ContainsKey(left))
                            {
                                newStones[left] = 0;
                            }
                            newStones[left] += stones[stone];
                            if (!newStones.ContainsKey(right))
                            {
                                newStones[right] = 0;
                            }
                            newStones[right] += stones[stone];
                        }
                        else
                        {
                            if (!newStones.ContainsKey(stone * 2024))
                            {
                                newStones[stone * 2024] = 0;
                            }
                            newStones[stone * 2024] += stones[stone];
                        }

                    }
                    stones.Remove(stone);
                }
                foreach (var stone in newStones)
                {
                    if (!stones.ContainsKey(stone.Key))
                    {
                        stones[stone.Key] = 0;
                    }
                    stones[stone.Key] += stone.Value;
                }
            }
            return stones.Values.Sum();
        }
    }
}
