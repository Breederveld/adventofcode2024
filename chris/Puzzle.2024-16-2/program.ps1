$input = Get-Content "input.txt";
$lines = $input.Split("\n");
$height = $lines.Count;
$width = $lines[0].Length;

# Create grids for distances.
# N, E, S, W
$startX = 0;
$startY = 0;
$endX = 0;
$endY = 0;
$grids = @();
for ($i = 0; $i -lt 4; $i++) {
	$grid = @();
	for ($y = 0; $y -lt $height; $y++) {
		$line = @();
		for ($x = 0; $x -lt $width; $x++) {
			if ($lines[$y][$x] -eq '#') {
				$line += , -1;
			} else {
				$line += , 99999999;
			}
			if ($lines[$y][$x] -eq 'S') {
				$startX = $x;
				$startY = $y;
			}
			if ($lines[$y][$x] -eq 'E') {
				$endX = $x;
				$endY = $y;
			}
		}
		$grid += , $line;
	}
	$grids += , $grid;
}

# Walk the path.
$dirs = @(@(0, -1), @(1, 0), @(0, 1), @(-1, 0));

[System.Collections.ArrayList]$queue = @();
[void]$queue.Add(@($startX, $startY, 1, 0));
while ($queue.Length -gt 0) {
	$curr = $queue[0];
	$queue.RemoveAt(0);
	$x = $curr[0];
	$y = $curr[1];
	$dir = $curr[2];
	$score = $curr[3];

	if ($x -lt 0 -or $y -lt 0 -or $x -ge $width -or $y -ge $height) {
		continue;
	}
	if ($grids[$dir][$y][$x] -le $score) {
		continue;
	}
	$grids[$dir][$y][$x] = $score;

	# Step.
	[void]$queue.Add(@($($x + $dirs[$dir][0]), $($y + $dirs[$dir][1]), $dir, $($score + 1)));

	# Rotate
	[void]$queue.Add(@($x, $y, $(($dir + 1) % 4), $($score + 1000)));
	[void]$queue.Add(@($x, $y, $(($dir + 3) % 4), $($score + 1000)));
}

Write-Host $grids[0][$endY][$endX];
Write-Host $grids[1][$endY][$endX];
Write-Host $grids[2][$endY][$endX];
Write-Host $grids[3][$endY][$endX];

$positions = @{};
# Walk the path back.
[void]$queue.Add(@($endX, $endY, 1, $grids[1][$endY][$endX]));
[void]$queue.Add(@($endX, $endY, 2, $grids[2][$endY][$endX]));
while ($queue.Length -gt 0) {
	$curr = $queue[0];
	$queue.RemoveAt(0);
	$x = $curr[0];
	$y = $curr[1];
	$dir = $curr[2];
	$score = $curr[3];

	if ($x -lt 0 -or $y -lt 0 -or $x -ge $width -or $y -ge $height) {
		continue;
	}
	if ($grids[$dir][$y][$x] -ne $score) {
		continue;
	}
	$key = "" + $x + "_" + $y;
	$positions[$key] = 1;

	# Step.
	[void]$queue.Add(@($($x - $dirs[$dir][0]), $($y - $dirs[$dir][1]), $dir, $($score - 1)));

	# Rotate
	[void]$queue.Add(@($x, $y, $(($dir + 1) % 4), $($score - 1000)));
	[void]$queue.Add(@($x, $y, $(($dir + 3) % 4), $($score - 1000)));
}
Write-Host $($positions.Count - 1);
