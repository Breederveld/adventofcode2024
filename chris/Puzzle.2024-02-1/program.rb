file = File.open("input.txt")
file_data = file.readlines.map(&:chomp);
file.close

reports = file_data.map { |str| str.split(" ").map { |str2| str2.to_i } }
cnt = reports.select { |a| \
	a[1] > a[0] && (0..a.count - 2).all? { |i| a[i + 1] - a[i] > 0 && a[i + 1] - a[i] < 4 } ||
	a[1] < a[0] && (0..a.count - 2).all? { |i| a[i] - a[i + 1] > 0 && a[i] - a[i + 1] < 4 }
}.count
p cnt
