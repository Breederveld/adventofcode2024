file = File.open("input.txt")
file_data = file.readlines.map(&:chomp);
file.close

reports = file_data.map { |str| str.split(" ").map { |str2| str2.to_i } }
cnt = reports.select { |a| \
	(0..a.count - 1).any? { |remove| \
		b = remove == 0 ? a[1..-1] : a[0..remove - 1].concat(a[remove + 1..-1])
		b[1] > b[0] && (0..b.count - 2).all? { |i| b[i + 1] - b[i] > 0 && b[i + 1] - b[i] < 4 } ||
		b[1] < b[0] && (0..b.count - 2).all? { |i| b[i] - b[i + 1] > 0 && b[i] - b[i + 1] < 4 }
	}
}.count
p cnt
