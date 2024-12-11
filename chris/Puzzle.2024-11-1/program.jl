open("input.txt") do inputFile
	stones = map((x) -> string(x), split(readline(inputFile), " "))
	println(stones);

	for i in 0:24
		stones = Iterators.flatmap((stone) ->
			if (stone == "0")
				["1"]
			elseif (length(stone) % 2 == 0)
				[string(parse(BigInt, first(stone, convert(Int16, length(stone) / 2)))), string(parse(BigInt, last(stone, convert(Int16, length(stone) / 2))))]
			else
				[string(parse(BigInt, stone) * 2024)]
		end, stones) |> collect
	end
	println(length(stones))
end
