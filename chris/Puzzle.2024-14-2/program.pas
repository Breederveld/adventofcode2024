{$MODE OBJFPC}
uses  
  Classes, SysUtils; 

const
	fieldWidth = 101;
	fieldHeight = 103;
	fieldHorMid = 50;
	fieldVerMid = 51;

type
	Robot = object
		public
			x: integer;
			y: integer;
			vx: integer;
			vy: integer;
			procedure Move(steps: integer);
			function ToString() : String;
	end;
	procedure Robot.Move(steps: integer);
		begin
			x := ((x + steps * vx) mod fieldWidth + fieldWidth) mod fieldWidth;
			y := ((y + steps * vy) mod fieldHeight + fieldHeight) mod fieldHeight;
		end;
	function Robot.ToString() : String;
		begin
			ToString := IntToStr(x) + ', ' + IntToStr(y) + ', ' + IntToStr(vx) + ', ' + IntToStr(vy);
		end;

var  
  inputs: TStringList; 
  line: TStringList; 
  subline: TStringList; 
	i: integer;
	aRobot: Robot;
	robots: array[0..500] Of Robot;
	robotsCnt: integer = 0;
	x: integer;
	y: integer;
	step: integer;
	found: boolean;
	foundCnt: integer;

procedure SplitText(aDelimiter: Char; const s: String; aList: TStringList);
begin
  aList.Delimiter := aDelimiter;
  aList.StrictDelimiter := True; // Spaces excluded from being a delimiter
  aList.DelimitedText := s;
end;

begin  
	// Read file into robots.
  inputs := TStringList.Create;  
  try  
    inputs.LoadFromFile('input.txt');  
		for i := 0 to inputs.Count - 1 do
		begin
			line := TStringList.Create;
			SplitText(' ', inputs.Strings[i], line);
			subline := TStringList.Create;
			SplitText(',', RightStr(line.Strings[0], line.Strings[0].Length - 2), subline);
			aRobot.x := StrToInt(subline.Strings[0]);
			aRobot.y := StrToInt(subline.Strings[1]);
			SplitText(',', RightStr(line.Strings[1], line.Strings[1].Length - 2), subline);
			aRobot.vx := StrToInt(subline.Strings[0]);
			aRobot.vy := StrToInt(subline.Strings[1]);
			//writeln(aRobot.ToString());
			subline.Free;
			line.Free;
			robotsCnt := robotsCnt + 1;
			robots[i] := aRobot;
		end;
  finally  
    inputs.Free; 
  end; 

	// Move the robots, then count the quadrants
	step := 65;
	for i := 0 to robotsCnt - 1 do
	begin
		robots[i].Move(65);
	end;
	while step < 10000 do
		begin
			step := step + 103;
			write('Round ');
			writeln(step);
			for i := 0 to robotsCnt - 1 do
			begin
				robots[i].Move(103);
			end;
			for x := 0 to fieldWidth - 1 do
				begin
					foundCnt := 0;
					for y := 0 to fieldHeight - 1 do
						begin
							found := false;
							for i := 0 to robotsCnt - 1 do
							begin
								if (robots[i].x = x) and (robots[i].y = y) then found := true;
							end;
							//if found then write('*') else write (' ');
							if found then foundCnt := foundCnt + 1;
						end;
					if foundCnt > 20 then
						begin
							writeln('Found it!');
							break;
						end;
					//writeln;
				end;
				if foundCnt > 20 then break;
				//writeln;
		end;
end.
