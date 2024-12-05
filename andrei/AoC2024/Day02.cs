namespace AoC2024
{
    internal class Day02
    {
        string input;
        List<List<int>> reports;

        public Day02()
        {
            input = InputReader.GetInput(nameof(Day02));
            reports = input.Split('\n').Select(l => l.Split(' ').Select(r => int.Parse(r)).ToList()).ToList();
        }

        public string Part1()
        {
            return reports.Count(IsReportSafe).ToString();
        }


        public string Part2()
        {
            var safeReports = reports.Count(report =>
            {
                if (IsReportSafe(report)) {
                    return true;
                }
                for(int i = 0; i < report.Count; i++)
                {
                    if (IsReportSafe(report.Where((r, index) => i != index).ToList()))
                    {
                        return true;
                    }
                }
                return false;
            });
            return safeReports.ToString();
        }

        private bool IsReportSafe(List<int> report)
        {
            if (report[0] == report[1])
            {
                return false;
            }
            var isDecrease = report[0] > report[1];
            for (int i = 0; i < report.Count - 1; i++)
            {
                var difference = report[i] - report[i + 1];
                if (difference == 0 || (isDecrease && difference < 0) || (!isDecrease && difference > 0))
                {
                    return false;
                }
                var differenceAbs = Math.Abs(difference);
                if (differenceAbs > 3)
                    return false;
            }
            return true;
        }
    }
}
