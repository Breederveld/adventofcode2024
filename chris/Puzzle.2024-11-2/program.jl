function stonesplit(stone)			
	if (stone == "0")
		["1"]
	elseif (length(stone) % 2 == 0)
		[string(parse(BigInt, first(stone, convert(Int16, length(stone) / 2)))), string(parse(BigInt, last(stone, convert(Int16, length(stone) / 2))))]
	else
		[string(parse(BigInt, stone) * 2024)]
	end
end
stonedict = Dict{String, BigInt}()
function stonecount(stone, depth)
	key = stone * "_" * string(depth)
	if (haskey(stonedict, key))
		return stonedict[key]
	end
	if (depth == 1)
		return length(stonesplit(stone))
	end
	result = sum(map((x) -> stonecount(x, depth - 1), stonesplit(stone)))
	stonedict[key] = result
	return result;
end

open("input.txt") do inputFile
	stones = map((x) -> string(x), split(readline(inputFile), " "))
	println(stones);

	result = sum(map((x) -> stonecount(x, 75), stones))
	println(result)
end
