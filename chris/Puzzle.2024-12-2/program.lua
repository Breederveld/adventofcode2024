local input = {}
for line in io.lines("input.txt") do
	local y = #input + 1
	if (line == "") then
		break
	end
	input[y] = {}
	for x = 1, #line do
		input[y][x] = line:sub(x,x)
	end
end
local height = #input
local width = #(input[1])

local regions = {}
for y = 1, height do
	regions[y] = {}
	for x = 1, width do
		regions[y][x] = 0
	end
end

local regionCnt = 0;
local result = 0;
local cornerPos = { { {-1, -1}, {-1, 0}, {0, -1} },  { {1, -1}, {0, -1}, {1, 0} },  { {1, 1}, {1, 0}, {0, 1} },  { {-1, 1}, {0, 1}, {-1, 0} } }
for y = 1, height do
	for x = 1, width do
		if (regions[y][x] == 0) then
			regionCnt = regionCnt + 1
			local area = 0
			local stack = {}
			stack[1] = { x, y }
			while (#stack > 0) do
				local t = stack[#stack]
				stack[#stack] = nil
				local xx = t[1]
				local yy = t[2]
				if (regions[yy][xx] == 0) then
					area = area + 1
					regions[yy][xx] = regionCnt
		
					if (xx > 1 and input[yy][xx - 1] == input[yy][xx]) then
						stack[#stack + 1] = { xx - 1, yy }
					end
					if (yy > 1 and input[yy - 1][xx] == input[yy][xx]) then
						stack[#stack + 1] = { xx, yy - 1 }
					end
					if (xx < width and input[yy][xx + 1] == input[yy][xx]) then
						stack[#stack + 1] = { xx + 1, yy }
					end
					if (yy < height and input[yy + 1][xx] == input[yy][xx]) then
						stack[#stack + 1] = { xx, yy + 1 }
					end
				end
			end
			local corners = 0
			for xx = 1, height do
				for yy = 1, width do
					if regions[yy][xx] == regionCnt then
						for posIdx = 1, #cornerPos do
							local filled = {}
							for i = 1, 3 do
								local xxx = cornerPos[posIdx][i][1] + xx
								local yyy = cornerPos[posIdx][i][2] + yy
								filled[i] = xxx > 0 and xxx <= width and yyy > 0 and yyy <= height and regions[yyy][xxx] == regions[yy][xx]
							end
							if (not(filled[2]) and not(filled[3])) or (not(filled[1]) and filled[2] == filled[3]) then
								corners = corners + 1
							end
						end
					end
				end
			end
			result = result + corners * area
		end
	end
end

print(result)
