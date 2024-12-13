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
		$prizeX += 10000000000000;
		$prizeY += 10000000000000;
		print("$buttonAX, $buttonAY - $buttonBX, $buttonBY = $prizeX, $prizeY\n");

		# Shortest path.
		$minSum = 10000000000000;
		$bMin = 0;
		$bMax = 100000000000000;
		$sign = $prizeX / $buttonAX <= $prizeY / $buttonAY ? 1 : -1;
		while ($bMax >= $bMin)
		{
			$b = floor(($bMax - $bMin) / 2 + $bMin);
			$aX = ($prizeX - $b * $buttonBX) / $buttonAX;
			$aY = ($prizeY - $b * $buttonBY) / $buttonAY;
			$aDiff = $aY - $aX;
			if ($aDiff == 0)
			{
				$bMax--;
			}
			else if ($aDiff * $sign < 0)
			{
				$bMax = $b - 1;
				continue;
			}
			else
			{
				$bMin = $b + 1;
				continue;
			}
			if ($prizeY == $buttonAY * $aY + $buttonBY * $b) {
				$sum = $aY * 3 + $b;
				if ($sum < $minSum && $aX == round($aX)) {
					print ("Found: $aY, $b, $sum\n");
					$minSum = $sum;
				}
			}
		}

		if ($minSum != 10000000000000) {
			$prizes++;
			$cost += $minSum;
		}
		print("\n");
	}

	print ($prizes . " for " . $cost . "\n");
?>
