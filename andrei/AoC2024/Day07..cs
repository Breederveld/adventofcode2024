namespace AoC2024
{
    internal class Day07
    {
        string input;
        List<(long result, List<long> terms)> lines;
        public Day07()
        {
            input = InputReader.GetInput(nameof(Day07));
            lines = input.Split("\n").Select(x =>
            {
                var parts = x.Split(": ");
                return (long.Parse(parts[0]), (parts[1].Split(" ").Select(p => long.Parse(p)).ToList()));
            }).ToList();
        }

        public string Part1()
        {
            long sum = 0;
            foreach (var item in lines)
            {
                var totalCombinations = (int)Math.Pow(2, item.terms.Count()-1);
                for (int i = 0; i < totalCombinations; i++)
                {
                    var configuration = Convert.ToString(i, 2).PadLeft(item.terms.Count() - 1, '0');
                    long test = item.terms[0];
                    for (int j = 0; j < configuration.Length; j++)
                    {
                        if (configuration[j] == '1')
                        {
                            test += item.terms[j+1];
                        }
                        else
                        {
                            test *= item.terms[j + 1];
                        }
                    }
                    if (test == item.result)
                    {
                        sum += item.result;
                        break;     
                    }
                }
            }
            return sum.ToString();
        }

        public string Part2()
        {
            long sum = 0;
            foreach (var item in lines)
            {
                var totalCombinations = (int)Math.Pow(3, item.terms.Count() - 1);
                for (int i = 0; i < totalCombinations; i++)
                {
                    var configuration = ConvertToBase3(i).PadLeft(item.terms.Count() - 1, '0');
                    long test = item.terms[0];
                    for (int j = 0; j < configuration.Length; j++)
                    {
                        if (configuration[j] == '1')
                        {
                            test += item.terms[j + 1];
                        }
                        else if (configuration[j] == '0')
                        {
                            test *= item.terms[j + 1];
                        }
                        else
                        {
                            test = long.Parse(test.ToString() + item.terms[j + 1].ToString());
                        }
                    }
                    if (test == item.result)
                    {
                        sum += item.result;
                        break;
                    }
                }
            }
            return sum.ToString();
        }

        private string ConvertToBase3(int input)
        {
            var result = "";
            while (input > 0)
            {
                int rem = input % 3;
                input = input / 3;
                result = rem + result;
            }
            return result;
        }
    }
}
