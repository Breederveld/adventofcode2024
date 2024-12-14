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
	res: integer = 0;
	quadrantCnt0: integer = 0;
	quadrantCnt1: integer = 0;
	quadrantCnt2: integer = 0;
	quadrantCnt3: integer = 0;

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
	for i := 0 to robotsCnt - 1 do
	begin
		robots[i].Move(100);
		writeln(robots[i].ToString());
		if robots[i].x < fieldHorMid then
		begin
			if robots[i].y < fieldVerMid then quadrantCnt0 := quadrantCnt0 + 1
			else if robots[i].y > fieldVerMid then quadrantCnt1 := quadrantCnt1 + 1;
		end
		else if robots[i].x > fieldHorMid then
		begin
			if robots[i].y < fieldVerMid then quadrantCnt2 := quadrantCnt2 + 1
			else if robots[i].y > fieldVerMid then quadrantCnt3 := quadrantCnt3 + 1;
		end;
	end;

	res := quadrantCnt0 * quadrantCnt1 * quadrantCnt2 * quadrantCnt3;
	writeln(res);
end.
