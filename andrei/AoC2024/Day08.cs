namespace AoC2024
{
    internal class Day08
    {
        string input;
        char[][] map;
        Dictionary<char, List<(int y, int x)>> antenas = new Dictionary<char, List<(int y, int x)>>();
        public Day08()
        {
            input = InputReader.GetInput(nameof(Day08));
            map = input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if(map[i][j] != '.')
                    {
                        if(!antenas.ContainsKey(map[i][j]))
                        {
                            antenas[map[i][j]] = new List<(int y, int x)>() { (i, j) };
                        }
                        else
                        {
                            antenas[map[i][j]].Add((i, j));
                        }
                    }
                }
            }
        }

        public string Part1()
        {
            Dictionary<string, bool> antinodes = new Dictionary<string, bool>();
            foreach (var antena in antenas)
            {
                if(antena.Value.Count == 1)
                {
                    continue;
                }
                for (int i = 0; i < antena.Value.Count-1; i++)
                {
                    for (int j = i + 1; j < antena.Value.Count; j++)
                    {
                        var a1 = antena.Value[i];
                        var a2 = antena.Value[j];
                        var yDif = Math.Abs(a1.y - a2.y);
                        var xDif = Math.Abs(a1.x - a2.x);

                        var t1y = a1.y + yDif * (a1.y < a2.y ? -1 : 1);
                        var t1x = a1.x + xDif * (a1.x < a2.x ? -1 : 1);

                        if(t1y>=0 && t1x>=0 && t1y<map.Length && t1x < map[0].Length)
                        {
                            antinodes[$"{t1y},{t1x}"] = true;
                        }

                        var t2y = a2.y + yDif * (a1.y < a2.y ? 1 : -1);
                        var t2x = a2.x + xDif * (a1.x < a2.x ? 1 : -1);
                        if(t2y >= 0 && t2x >= 0 && t2y < map.Length && t2x < map[0].Length)
                        {
                            antinodes[$"{t2y},{t2x}"] = true;
                        }
                    }
                }
            }
            return antinodes.Count.ToString();
        }


        public string Part2()
        {
            var antinodes = new Dictionary<string, bool>();
            foreach(var antena in antenas)
            {
                if (antena.Value.Count == 1)
                {
                    continue;
                }
                antena.Value.ForEach(x => antinodes[$"{x.y},{x.x}"] = true);
                for (int i = 0; i < antena.Value.Count - 1; i++)
                {
                    for (int j = i + 1; j < antena.Value.Count; j++)
                    {
                        var a1 = antena.Value[i];
                        var a2 = antena.Value[j];
                        var yDif = Math.Abs(a1.y - a2.y);
                        var xDif = Math.Abs(a1.x - a2.x);

                        var t1y = a1.y + yDif * (a1.y < a2.y ? -1 : 1);
                        var t1x = a1.x + xDif * (a1.x < a2.x ? -1 : 1);

                        while(t1y >= 0 && t1x >= 0 && t1y < map.Length && t1x < map[0].Length)
                        {
                            antinodes[$"{t1y},{t1x}"] = true;
                            t1y += yDif * (a1.y < a2.y ? -1 : 1);
                            t1x += xDif * (a1.x < a2.x ? -1 : 1);
                        }

                        var t2y = a2.y + yDif * (a1.y < a2.y ? 1 : -1);
                        var t2x = a2.x + xDif * (a1.x < a2.x ? 1 : -1);
                        while(t2y >= 0 && t2x >= 0 && t2y < map.Length && t2x < map[0].Length)
                        {
                            antinodes[$"{t2y},{t2x}"] = true;
                            t2y += yDif * (a1.y < a2.y ? 1 : -1);
                            t2x += xDif * (a1.x < a2.x ? 1 : -1);
                        }
                    }
                }
            }
            return antinodes.Count.ToString();
        }
    }
}
