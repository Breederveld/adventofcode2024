with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics; use Ada.Numerics;

procedure Program is
	--F: File_Type;
	type ThreeBitArray is array(0..15) of Integer;
	--type ThreeBitArray is array(0..5) of Integer;
	type Modular is mod 2**32;
	Program: ThreeBitArray := (2,4,1,3,7,5,4,2,0,3,1,5,5,5,3,0);
	--Program: ThreeBitArray := (0,1,5,4,3,0);
	Pc: Integer := 0;
	RegisterA: Integer := 30118712;
	--RegisterA: Integer := 729;
	RegisterB: Integer := 0;
	RegisterC: Integer := 0;
	Literal: Integer := 0;
	Combo: Integer := 0;
begin
	--Open (F, In_File, "/code/input.txt");
	--while not End_Of_File (F) loop
	--	Put_Line (Get_Line (F));
	--end loop;
	--Close (F);

	while Pc < Program'Length loop
		--Put(Program(Pc)'Image);
		--Put_Line("***");
		Literal := Program(Pc + 1);
		Combo := Literal;
		if Combo = 4 then
			Combo := RegisterA;
		elsif Combo = 5 then
			Combo := RegisterB;
		elsif Combo = 6 then
			Combo := RegisterC;
		end if;

		case Program(Pc) is
			when 0 => -- adv
				RegisterA := RegisterA / 2**Combo;
				--Put_Line(RegisterA'Image);
			when 1 => -- bxl
				RegisterB := Integer(Modular(RegisterB) xor Modular(Literal));
				--Put_Line(RegisterB'Image);
			when 2 => -- bst
				RegisterB := Integer(Combo mod 8);
				--Put_Line(RegisterB'Image);
			when 3 => -- jnz
				if RegisterA /= 0 then
					Pc := Literal - 2;
				end if;
			when 4 => -- bxc
				RegisterB := Integer(Modular(RegisterB) xor Modular(RegisterC));
				--Put_Line(RegisterB'Image);
			when 5 => -- out
				Combo := Combo mod 8;
				Put(Combo'Image & ",");
				--Put_Line("");
			when 6 => -- bdv
				RegisterB := RegisterA / 2**Combo;
				--Put_Line(RegisterB'Image);
			when 7 => -- cdv
				RegisterC := RegisterA / 2**Combo;
				--Put_Line(RegisterC'Image);
			when others => RegisterA := RegisterA;
		end case;
		--Put_Line(RegisterA'Image);
		--Put_Line(RegisterB'Image);
		--Put_Line(RegisterC'Image);

		Pc := Pc + 2;
	end loop;

end Program;
