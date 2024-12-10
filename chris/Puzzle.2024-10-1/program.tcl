set fp [open "input.txt" r]
set input [read $fp]
close $fp
set lines [split $input "\n"]
set height [expr [llength $lines] - 1]
set width [string length [lindex $lines 0]]

set result 0
set directions [list 0 -1 1 0 0 1 -1 0]
set todo [list]
for {set y 0} {$y < $height} {incr y} {
	for {set x 0} {$x < $width} {incr x} {
		set value [string index [lindex $lines $y] $x]
		set expect 0
		if { $value != $expect } continue

		set found [list]
		lappend todo $x $y 0
		incr expect

		puts "*** $x $y"
		while {$expect > 0} {
			set dir [lindex $todo [expr ($expect - 1) * 3 + 2]]
			set xx [expr [lindex $directions [expr $dir * 2]] + [lindex $todo [expr ($expect - 1) * 3]]]
			set yy [expr [lindex $directions [expr $dir * 2 + 1]] + [lindex $todo [expr ($expect - 1) * 3 + 1]]]

			if {$xx >= 0 && $yy >= 0 && $xx < $width && $yy < $height} {
				set value [string index [lindex $lines $yy] $xx]
				if {$value == $expect} {
					if {$value == 9} {
						lappend found [expr $xx + $yy * $width]
					}
					lappend todo $xx $yy 0
					incr expect
					continue
				}
			}
			while 1 {
				set xx [lindex $todo end-2] 
				set yy [lindex $todo end-1] 
				set dir [lindex $todo end] 
				set todo [lreplace $todo end-2 end]
				incr dir
				if {$dir >= 4} {
					set expect [expr $expect - 1]
				}
				if {$dir < 4 || $expect == 0} break
			}
			lappend todo $xx $yy $dir
		}
		set todo [lreplace $todo end-2 end]
		set result [expr $result + [llength [lsort -unique $found]]]
	}
}
puts $result
