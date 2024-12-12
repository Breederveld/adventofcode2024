

namespace AoC2024
{
    internal class Day12
    {
        string input;
        char[][] map;
        HashSet<(int, int)> visited;
        public Day12()
        {
            input = InputReader.GetInput(nameof(Day12));
            map = input.Split("\r\n").Select(l => l.ToCharArray()).ToArray();
        }

        public string Part1()
        {
            var totalCost = 0;
            visited = new HashSet<(int, int)>();
            char currentPlant;
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (visited.Contains((i, j))) continue;
                    currentPlant = map[i][j];
                    var currentArea = new HashSet<(int, int)>();
                    totalCost += this.GetPerimeter((i, j), currentPlant, currentArea) * currentArea.Count;
                }
            }
            return totalCost.ToString();
        }

        public string Part2()
        {
            var totalCost = 0;
            visited = new HashSet<(int, int)>();
            char currentPlant;
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (visited.Contains((i, j))) continue;
                    currentPlant = map[i][j];
                    var currentArea = new HashSet<(int, int)>();
                    this.GetPerimeter((i, j), currentPlant, currentArea);
                    var corners = this.GetCorners(currentArea);
                    totalCost += corners.Count * currentArea.Count;
                }
            }
            return totalCost.ToString();
        }

        private List<(double, double)> GetCorners(HashSet<(int y, int x)> area)
        {
            var corners = new List<(double y, double x)>();
            foreach (var p in area)
            {
                //Check upper left
                if ((!corners.Contains((p.y-0.5, p.x-0.5)) && 
                    (!area.Contains((p.y-1,p.x-1)) && !area.Contains((p.y - 1, p.x)) && !area.Contains((p.y, p.x - 1)) ||
                    area.Contains((p.y-1,p.x-1)) && !area.Contains((p.y, p.x-1)) ||
                    area.Contains((p.y,p.x - 1)) && !area.Contains((p.y - 1, p.x-1)) && area.Contains((p.y-1,p.x)) ||
                    area.Contains((p.y-1,p.x-1)) && !area.Contains((p.y, p.x - 1))))

                    || (corners.Contains((p.y - 0.5, p.x - 0.5)) && 
                    area.Contains((p.y-1, p.x-1)) && !area.Contains((p.y-1, p.x)) && !area.Contains((p.y,p.x-1))))
                    corners.Add((p.y - 0.5, p.x - 0.5));

                //Check upper right
                if (!corners.Contains((p.y - 0.5, p.x + 0.5)) &&
                    (!area.Contains((p.y - 1, p.x + 1)) && !area.Contains((p.y - 1, p.x)) && !area.Contains((p.y, p.x + 1)) ||
                    area.Contains((p.y-1,p.x+1)) && !area.Contains((p.y, p.x+1)) ||
                    area.Contains((p.y-1, p.x)) && !area.Contains((p.y - 1, p.x + 1)) && area.Contains((p.y, p.x + 1)) ||
                    area.Contains((p.y - 1, p.x + 1)) && !area.Contains((p.y-1, p.x))) ||
                    (corners.Contains((p.y-0.5, p.x+0.5)) &&
                    area.Contains((p.y - 1, p.x + 1)) && !area.Contains((p.y - 1, p.x)) && !area.Contains((p.y, p.x + 1)))
                    ) 
                    corners.Add((p.y - 0.5, p.x + 0.5));

                //Check lower left
                if (!corners.Contains((p.y + 0.5, p.x - 0.5)) &&
                    (!area.Contains((p.y + 1, p.x - 1)) && !area.Contains((p.y + 1, p.x)) && !area.Contains((p.y, p.x - 1)) ||
                    area.Contains((p.y + 1, p.x - 1)) && !area.Contains((p.y, p.x - 1)) ||
                    area.Contains((p.y + 1, p.x)) && area.Contains((p.y + 1, p.x - 1)) && !area.Contains((p.y, p.x - 1)) ||
                    area.Contains((p.y + 1, p.x)) && !area.Contains((p.y + 1, p.x - 1)) && area.Contains((p.y, p.x - 1))) ||
                    
                    (corners.Contains((p.y + 0.5, p.x - 0.5)) &&
                    area.Contains((p.y + 1, p.x - 1)) && !area.Contains((p.y + 1, p.x)) && !area.Contains((p.y, p.x - 1)))
                    )
                    corners.Add((p.y + 0.5, p.x - 0.5));

                //Check lower right
                if (!corners.Contains((p.y + 0.5, p.x + 0.5)) &&
                    (!area.Contains((p.y + 1, p.x + 1)) && !area.Contains((p.y + 1, p.x)) && !area.Contains((p.y, p.x + 1)) ||
                    area.Contains((p.y + 1, p.x + 1)) && !area.Contains((p.y, p.x + 1)) ||
                    area.Contains((p.y,p.x+1)) && !area.Contains((p.y + 1, p.x + 1)) && area.Contains((p.y + 1, p.x)) ||
                    area.Contains((p.y + 1, p.x + 1)) && !area.Contains((p.y + 1, p.x))) || 
                    (corners.Contains((p.y + 0.5, p.x + 0.5)) &&
                    area.Contains((p.y + 1, p.x + 1)) && !area.Contains((p.y + 1, p.x)) && !area.Contains((p.y, p.x + 1)))
                    )
                    corners.Add((p.y + 0.5, p.x + 0.5));

            }
            return corners;
        }

        private int GetPerimeter((int y, int x) point, char currentPlant, HashSet<(int, int)> currentArea)
        {
            var cost = 0;
            this.visited.Add(point);
            currentArea.Add(point);
            if (point.y == 0 || map[point.y - 1][point.x] != map[point.y][point.x]) { cost++; }
            else
            {
                if (!currentArea.Contains((point.y - 1, point.x)))
                {
                    cost += this.GetPerimeter((point.y - 1, point.x), currentPlant, currentArea);
                }
            }
            if (point.y == map.Length - 1 || map[point.y + 1][point.x] != map[point.y][point.x]) cost++;
            else if (!currentArea.Contains((point.y + 1, point.x)))
            {
                cost += this.GetPerimeter((point.y + 1, point.x), currentPlant, currentArea);
            }
            if (point.x == 0 || map[point.y][point.x - 1] != map[point.y][point.x]) cost++;
            else if (!currentArea.Contains((point.y, point.x - 1)))
            {
                cost += this.GetPerimeter((point.y, point.x - 1), currentPlant, currentArea);
            }
            if (point.x == map[point.y].Length - 1 || map[point.y][point.x + 1] != map[point.y][point.x]) cost++;
            else if (!currentArea.Contains((point.y, point.x + 1)))
            {
                cost += this.GetPerimeter((point.y, point.x + 1), currentPlant, currentArea);
            }
            return cost;
        }
    }
}
