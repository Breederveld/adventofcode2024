using System.Numerics;

namespace AoC2024
{
    internal class Day09
    {
        string input;
        public Day09()
        {
            input = InputReader.GetInput(nameof(Day09));
        }

        public string Part1()
        {
            List<int> disk = BuildDisk();

            var blank = disk.IndexOf(-1);
            var cursor = disk.Count - 1;
            while(cursor > blank)
            {
                if (disk[cursor] == -1)
                {
                    cursor--;
                    continue;
                }
                else 
                {
                    disk[blank] = disk[cursor];
                    disk[cursor] = -1;
                    cursor--;
                    while (disk[blank] != -1)
                    {
                        blank++;
                    }
                }
            }

            long checkSum = GetChecksum(disk);
            return checkSum.ToString();
        }


        public string Part2()
        {
            var disk = BuildDisk();
            var blank = disk.IndexOf(-1);
            var cursor = disk.Count - 1;
            while (cursor > blank)
            {
                if (disk[cursor] == -1)
                {
                    cursor--;
                    continue;
                }

                var fileSize = 1;

                var fileCursor = cursor;
                while (disk[fileCursor-1] == disk[cursor])
                {
                    fileSize++;
                    fileCursor--;
                }

                // find first sequence of -1s at least as long as the fileSize
                var blankSpace = 1;
                var blankStart = blank;
                while (blankSpace < fileSize && blank < fileCursor)
                {
                    blank++;
                    if (disk[blank] == -1)
                    {
                        blankSpace++;
                    }
                    else
                    {
                        while (blank < fileCursor && disk[blank] != -1)
                        {
                            blank++;
                        }
                        blankSpace = 1;
                        blankStart = blank;
                    }
                }

                if (blankSpace == fileSize)
                {
                    for (int i = 0; i < fileSize; i++)
                    {
                        disk[blankStart + i] = disk[fileCursor+i];
                        disk[fileCursor+i] = -1;
                    }
                }

                cursor = fileCursor - 1;
                blank = disk.IndexOf(-1);
            }
            return GetChecksum(disk).ToString();
        }

        private List<int> BuildDisk()
        {
            List<int> disk = new List<int>();
            var currentId = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i] - '0';
                for (int j = 0; j < current; j++)
                {
                    if (i % 2 == 0)
                    {
                        disk.Add(currentId);
                    }
                    else
                    {
                        disk.Add(-1);
                    }
                }
                if (i % 2 == 0)
                {
                    currentId++;
                }
            }
            return disk;
        }

        private long GetChecksum(List<int> disk)
        {
            long checkSum = 0;
            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] == -1)
                {
                    continue;
                }
                checkSum += disk[i] * i;
            }
            return checkSum;
        }
    }
}
