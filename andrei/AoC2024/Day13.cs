using System.Text.RegularExpressions;

namespace AoC2024
{
    internal class Day13
    {
        string input;

        public Day13()
        {
            input = InputReader.GetInput(nameof(Day13));
        }

        public string Part1()
        {
            var sum = 0;
            input.Split("\r\n\r\n").ToList().ForEach(arcade =>
            {
                int Ax, Ay, Bx, By, Px, Py;
                var parts = arcade.Split("\r\n").ToArray();
                var butA = Regex.Matches(parts[0], @"[0-9]+").ToArray();
                Ax = int.Parse(butA[0].Value);
                Ay = int.Parse(butA[1].Value);
                var butB = Regex.Matches(parts[1], @"[0-9]+").ToArray();
                Bx = int.Parse(butB[0].Value);
                By = int.Parse(butB[1].Value);
                var prize = Regex.Matches(parts[2], @"[0-9]+").ToArray();
                Px = int.Parse(prize[0].Value);
                Py = int.Parse(prize[1].Value);

                var NB = (Py*Ax - Ay*Px) / (By*Ax -Ay * Bx);
                var NA = (Px - Bx * NB) / Ax;

                if (Ax * NA + Bx * NB == Px && Ay * NA + By * NB == Py)
                    sum += 3 * NA + NB;

            });
            return sum.ToString();
        }


        public string Part2()
        {
            long sum = 0;
            input.Split("\r\n\r\n").ToList().ForEach(arcade =>
            {
                long Ax, Ay, Bx, By, Px, Py;
                var parts = arcade.Split("\r\n").ToArray();
                var butA = Regex.Matches(parts[0], @"[0-9]+").ToArray();
                Ax = int.Parse(butA[0].Value);
                Ay = int.Parse(butA[1].Value);
                var butB = Regex.Matches(parts[1], @"[0-9]+").ToArray();
                Bx = int.Parse(butB[0].Value);
                By = int.Parse(butB[1].Value);
                var prize = Regex.Matches(parts[2], @"[0-9]+").ToArray();
                Px = int.Parse(prize[0].Value) + 10000000000000;
                Py = int.Parse(prize[1].Value) + 10000000000000;

                var NB = (Py * Ax - Ay * Px) / (By * Ax - Ay * Bx);
                var NA = (Px - Bx * NB) / Ax;

                if (Ax * NA + Bx * NB == Px && Ay * NA + By * NB == Py)
                    sum += 3 * NA + NB;

            });
            return sum.ToString();
        }
    }
}
