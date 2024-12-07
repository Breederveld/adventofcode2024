use std::fs;

fn main() {
		let file = fs::read_to_string("input.txt")
			.expect("No input");
		let mut data = file
			.trim()
			.split("\n")
			.map(|s| s.chars().collect::<Vec<char>>())
			.collect::<Vec<Vec<char>>>();
		let height = data.len();
		let width = data[0].len();
		println!("{}x{}", width, height);

		let mut curr: (usize, usize, usize) = (0, 0, 0);
		for x in 0..width {
			for y in 0..height {
				if data[y][x] == '^' {
					curr = (x, y, 0);
					println!("{}x{}", curr.0, curr.1);
				}
			}
		}

		let moves: [(isize, isize); 4] = [(0, -1), (1, 0), (0, 1), (-1, 0)];
		let mut cnt = 1;
		loop {
			let dir = curr.2;
			let prev = curr;
			curr = ((curr.0 as isize + moves[dir].0) as usize, (curr.1 as isize + moves[dir].1) as usize, dir);
			if curr.0 > width - 1 || curr.1 > height - 1 {
				break;
			}
			if data[curr.1][curr.0] == '.' {
				data[curr.1][curr.0] = ' ';
				cnt += 1;
			} else if data[curr.1][curr.0] == '#' {
				let dir = (prev.2 + 1) % 4;
				curr = (prev.0, prev.1, dir);
			}
		}

	println!("{}", cnt);
}
