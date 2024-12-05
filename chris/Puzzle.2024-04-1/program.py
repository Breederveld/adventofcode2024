data = open("input.txt", "r").read();
lines = data.strip().split("\n");
width = len(lines[0]);
height = len(lines);
search = "XMAS";

directions = [(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)]
answer = 0;
for x in range(width):
	for y in range(height):
		if (lines[y][x] == search[0]):
			for (dirx, diry) in directions:
				found = True;
				for pos in range(1, len(search)):
					xx = x + dirx * pos;
					yy = y + diry * pos;
					if (xx < 0 or xx >= width or yy < 0 or yy >= height or lines[yy][xx] != search[pos]):
						found = False;
				if found:
					answer = answer + 1;

print(answer);
