<?php
	$input = file_get_contents("input.txt");
	$lines = explode("\n", $input);
	$prizes = 0;
	$cost = 0;
	for ($i = 0; $i < sizeof($lines); $i += 4) {
		# Read input.
		$buttonA = explode(",", explode(":", $lines[$i])[1]);
		$buttonAX = explode("+", $buttonA[0])[1];
		$buttonAY = explode("+", $buttonA[1])[1];
		$buttonB = explode(",", explode(":", $lines[$i + 1])[1]);
		$buttonBX = explode("+", $buttonB[0])[1];
		$buttonBY = explode("+", $buttonB[1])[1];
		$prize = explode(",", explode(":", $lines[$i + 2])[1], 3);
		$prizeX = explode("=", $prize[0])[1];
		$prizeY = explode("=", $prize[1])[1];
		print($buttonAX . ", " . $buttonAY . "\n");
		print($buttonBX . ", " . $buttonBY . "\n");
		print($prizeX . ", " . $prizeY . "\n");

		# Shortest path.
		$prizeSum = $prizeX + $prizeY;
		$buttonBSum = $buttonBX + $buttonBY;
		$buttonASum = $buttonAX + $buttonAY;
		$minSum = 99999;
		for ($b = 0; $b < 100; $b++) {
			$a = ($prizeX - $b * $buttonBX) / $buttonAX;
			if ($prizeY == $buttonAY * $a + $buttonBY * $b) {
				if ($a < 0 || $a > 100) {
					continue;
				}
				$sum = $a * 3 + $b;
				if ($sum < $minSum) {
					$minSum = $sum;
				}
			}
		}
		if ($minSum != 99999) {
			$prizes++;
			$cost += $minSum;
		}
		print("\n");
	}

	print ($prizes . " for " . $cost . "\n");
?>
