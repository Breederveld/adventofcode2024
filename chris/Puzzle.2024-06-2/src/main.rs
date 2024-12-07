use std::fs;

fn main() {
	let file = fs::read_to_string("input.txt")
		.expect("No input");
	let orig = file
		.trim()
		.split("\n")
		.map(|s| s.chars().collect::<Vec<char>>())
		.collect::<Vec<Vec<char>>>();
	let height = orig.len();
	let width = orig[0].len();
	println!("{}x{}", width, height);

	let mut xx = 0;
	let mut yy = 0;
	let mut cnt = 0;
	loop {
		xx += 1;
		if xx == width {
		  xx = 0;
			yy += 1;
			if yy == height {
				break;
			}
		}
		if orig[yy][xx] != '.' {
			continue;
		}
		let mut data = orig.clone();
		data[yy][xx] = '#';

		let mut curr: (usize, usize, usize) = (0, 0, 0);
		for x in 0..width {
			for y in 0..height {
				if data[y][x] == '^' {
					curr = (x, y, 0);
				}
			}
		}

		let moves: [(isize, isize); 4] = [(0, -1), (1, 0), (0, 1), (-1, 0)];
		let mut steps = 0;
		loop {
			steps += 1;
			let dir = curr.2;
			let prev = curr;
			curr = ((curr.0 as isize + moves[dir].0) as usize, (curr.1 as isize + moves[dir].1) as usize, dir);
			if curr.0 > width - 1 || curr.1 > height - 1 {
				break;
			}
			if data[curr.1][curr.0] == '#' {
				if steps > 10000 {
					cnt += 1;
					break;
				}
				let dir = (prev.2 + 1) % 4;
				curr = (prev.0, prev.1, dir);
			}
		}
	
	}
	println!("{}", cnt);
}
