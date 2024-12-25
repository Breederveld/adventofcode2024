using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2024_25_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Split("\n").Select(s => s.TrimEnd()).ToArray();
            sw.Start();

            var y = 0;
            var locks = new List<int[]>();
            var keys = new List<int[]>();
            var width = strings[0].Length;
            var height = strings.TakeUntil(s => s == string.Empty).Count() - 1;
            var arr = new int[width];
            var isLock = false;
            foreach (var line in strings)
            {
                if (line == string.Empty)
                {
                    if (isLock)
                    {
                        locks.Add(arr);
                    }
                    else
                    {
                        keys.Add(arr.Select(i => height - i).ToArray());
                    }
                    y = 0;
                    continue;
                }
                if (y == 0)
                {
                    arr = new int[width];
                    isLock = line[0] == '#';                   
                }
                for (var i = 0; i < width; i++)
                {
                    if (line[i] == '#')
                    {
                        arr[i]++;
                    }
                }
                y++;
            }

            var result = 0;
            for (var key = 0; key < keys.Count; key++)
            {
                for (var lck = 0; lck < locks.Count; lck++)
                {
                    if (Enumerable.Range(0, width).All(i => keys[key][i] >= locks[lck][i]))
                    {
                        result++;
                    }
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}