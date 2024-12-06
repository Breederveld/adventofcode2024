namespace AoC2024
{
    internal class Day06
    {
        string input;
        char[][] map;
        List<char> directions = new List<char> { '^', '>', 'v', '<' };
        List<(int y, int x, int direction)> pathPositions = new List<(int y, int x, int direction)>();
        (int y, int x, int direction) start;

        public Day06()
        {
            input = InputReader.GetInput(nameof(Day06));
            map = input.Split("\r\n").Select(line => line.ToCharArray()).ToArray();
            start = this.GetStart();
        }

        public string Part1()
        {
            var count = 1;
            var pos = (start.y, start.x, start.direction);
            map[pos.y][pos.x] = 'X';
            while (pos.y > 0 && pos.y < map.Length - 1 && pos.x > 0 && pos.x < map[0].Length - 1)
            {
                var nextPos = this.Move(pos);
                while (map[nextPos.y][nextPos.x] == '#')
                {
                    pos.direction = (pos.direction + 1) % 4;
                    nextPos = this.Move(pos);
                }
                pos = nextPos;
                if (map[nextPos.y][nextPos.x] == '.')
                {
                    count++;
                    pathPositions.Add((pos.y, pos.x, pos.direction));
                }
                map[pos.y][pos.x] = 'X';
            }

            return count.ToString();
        }


        public string Part2()
        {
            var count = 0;
            foreach (var pathPos in pathPositions)
            {
                map[pathPos.y][pathPos.x] = '#';
                (int y, int x, int direction) pos = (start.y, start.x, start.direction);
                var currentPath = new Dictionary<string, bool>();
                while (pos.y > 0 && pos.y < map.Length - 1 && pos.x > 0 && pos.x < map[0].Length - 1)
                {
                    var nextPos = this.Move(pos);
                    while (map[nextPos.y][nextPos.x] == '#')
                    {
                        pos.direction = (pos.direction + 1) % 4;
                        nextPos = this.Move(pos);
                    }
                    pos = nextPos;
                    var curentPathKey = $"{pos.y},{pos.x},{pos.direction}";
                    if (currentPath.ContainsKey(curentPathKey))
                    {
                        count++;
                        break;
                    }
                    currentPath.Add(curentPathKey, true);
                }

                map[pathPos.y][pathPos.x] = '.';
            }
            return count.ToString();
        }

        private (int y, int x, int direction) Move((int y, int x, int direction) pos)
        {
            switch (pos.direction)
            {
                case 0:
                    return (pos.y - 1, pos.x, pos.direction);
                case 1:
                    return (pos.y, pos.x + 1, pos.direction);
                case 2:
                    return (pos.y + 1, pos.x, pos.direction);
                case 3:
                    return (pos.y, pos.x - 1, pos.direction);
                default:
                    throw new Exception("Invalid direction");
            }
        }

        private (int y, int x, int direction) GetStart()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map.Length; j++)
                {
                    var direction = directions.IndexOf(map[i][j]);
                    if (direction != -1)
                    {
                        return (i, j, direction);
                    }
                }
            }
            return (-1, -1, -1);
        }
    }
}
