#!/usr/bin/perl
use strict;
use warnings;

use autodie;

my $fh;
my @field = ();
my @moves = ();
my $height = 0;
my $isMoves = 0;
my $robotX = -1;
my $robotY = -1;
open($fh, "<", "input.txt") or die $!;
while (<$fh>) {
	if ($isMoves == 0) {
		if ($_ eq "\n") {
			$isMoves = 1;
		} else {
			$_ =~ s/#/##/g;
			$_ =~ s/O/[]/g;
			$_ =~ s/\./../g;
			$_ =~ s/@/@./g;
			my @line = split //, $_ ;
			push(@field, \@line);
			if ($robotX == -1) {
				$robotX = index($_, "@");
				if ($robotX != -1) {
					$robotY = $height;
				}
			}
			$height++;
		}
	} else {
		push(@moves, split //, substr($_, 0, -1));
	}
}
my @list = @{$field[3]};
${$field[$robotY]}[$robotX] = ".";
my $width = $#{$field[0]};

print $height, ", ", $width, "\n";
print $#moves, "\n";

sub TryMove {
	my $x = $_[0];
	my $y = $_[1];
	my $dx = $_[2];
	my $dy = $_[3];
	my $whatIf = $_[4];
	my $xx = $x + $dx;
	my $yy = $y + $dy;

	my $this = ${$field[$y]}[$x];
	if ($this eq "#") {
		return 0;
	}
	if ($this eq ".") {
		return 1;
	}
	if ($dx == 0) {
		if ($this eq "]") {
			$x--;
			$xx--;
		}
		if (TryMove($xx, $yy, $dx, $dy, 1) == 1 && TryMove($xx + 1, $yy, $dx, $dy, 1) == 1) {
			TryMove($xx, $yy, $dx, $dy, $whatIf);
			TryMove($xx + 1, $yy, $dx, $dy, $whatIf);
			if ($whatIf == 0) {
				${$field[$yy]}[$xx] = ${$field[$y]}[$x];
				${$field[$yy]}[$xx + 1] = ${$field[$y]}[$x + 1];
				${$field[$y]}[$x] = ".";
				${$field[$y]}[$x + 1] = ".";
			}
			return 1;
		}
	} else {
		if (TryMove($xx, $yy, $dx, $dy, $whatIf) == 1) {
			if ($whatIf == 0) {
				${$field[$yy]}[$xx] = ${$field[$y]}[$x];
			}
			return 1;
		}
	}
	return 0;
}

sub Count {
	my $sum = 0;
	for (my $y = 0; $y < $#field + 1; $y++) {
		for (my $x = 0; $x < $#{$field[$y]}; $x++) {
			if (${$field[$y]}[$x] eq "[") {
				$sum += 100 * $y + $x;
			}
		}
	}
	return $sum;
}

sub PrintField {
	my $steps = $_[0];
	my $move = $_[1];
	print "* Step ", $steps, ", move ", $move , " *\n";
	for (my $y = 0; $y < $#field + 1; $y++) {
		for (my $x = 0; $x < $#{$field[$y]}; $x++) {
			if ($x == $robotX && $y == $robotY) {
				print "@";
			} else {
				print ${$field[$y]}[$x];
			}
		}
		print "\n";
	}
}

my $steps = 0;
#PrintField($steps, " ");
foreach (@moves) {
	my $move = $_;
	my $dx = 0;
	my $dy = 0;
	if ($move eq "<") {
		$dx = -1;
	} elsif ($move eq ">") {
		$dx = 1;
	} elsif ($move eq "^") {
		$dy = -1;
	} elsif ($move eq "v") {
		$dy = 1;
	}
	if (TryMove($robotX + $dx, $robotY + $dy, $dx, $dy, 0) == 1) {
		$robotX += $dx;
		$robotY += $dy;
		${$field[$robotY]}[$robotX] = ".";
	}
	$steps++;
	#PrintField($steps, $move);
}
print Count(), "\n";
