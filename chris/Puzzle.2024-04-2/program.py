data = open("input.txt", "r").read();
lines = data.strip().split("\n");
width = len(lines[0]);
height = len(lines);

directions = [
	[(-1, -1, 'M'), (1, -1, 'S'), (-1, 1, 'M'), (1, 1, 'S')],
	[(-1, -1, 'M'), (1, -1, 'M'), (-1, 1, 'S'), (1, 1, 'S')],
	[(-1, -1, 'S'), (1, -1, 'M'), (-1, 1, 'S'), (1, 1, 'M')],
	[(-1, -1, 'S'), (1, -1, 'S'), (-1, 1, 'M'), (1, 1, 'M')]];
answer = 0;
for x in range(width):
	for y in range(height):
		if (lines[y][x] == 'A'):
			for dirt in directions:
				found = True;
				for (dirx, diry, char) in dirt:
					xx = x + dirx;
					yy = y + diry;
					if (xx < 0 or xx >= width or yy < 0 or yy >= height or lines[yy][xx] != char):
						found = False;
				if found:
					answer = answer + 1;

print(answer);
