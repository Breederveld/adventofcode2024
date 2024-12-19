with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics; use Ada.Numerics;

procedure Program is
	type ThreeBitArray is array(0..15) of Integer;
	type Modular is mod 2**64;
	Program: ThreeBitArray := (2,4,1,3,7,5,4,2,0,3,1,5,5,5,3,0);
	Pc: Integer := 0;
	Input: ThreeBitArray := (0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
	Printed: Integer := 0;
	LastMatch: Integer := 0;
	RegisterA: Long_Integer := 0;
	RegisterB: Long_Integer := 0;
	RegisterC: Long_Integer := 0;
	Literal: Long_Integer := 0;
	Combo: Long_Integer := 0;
begin
	while LastMatch /= 16 loop
		loop
			Input(LastMatch) := Input(LastMatch) + 1;
			if Input(LastMatch) > 7 then
				LastMatch := LastMatch - 1;
			else
				exit;
			end if;
		end loop;
		for I in (LastMatch + 1)..15 loop
			Input(I) := 0;
		end loop;
		LastMatch := 16;
		Printed := 0;
		Pc := 0;
		RegisterA := 0;
		RegisterB := 0;
		RegisterC := 0;
		for I in 0..15 loop
			RegisterA := RegisterA * 8 + Long_Integer(Input(I));
		end loop;
		Put_Line(RegisterA'Image);
	
		while Pc < Program'Length loop
			Literal := Long_Integer(Program(Pc + 1));
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
					RegisterA := RegisterA / 2**Integer(Combo);
				when 1 => -- bxl
					RegisterB := Long_Integer(Modular(RegisterB) xor Modular(Literal));
				when 2 => -- bst
					RegisterB := Combo mod 8;
				when 3 => -- jnz
					if RegisterA /= 0 then
						Pc := Integer(Literal - 2);
					end if;
				when 4 => -- bxc
					RegisterB := Long_Integer(Modular(RegisterB) xor Modular(RegisterC));
				when 5 => -- out
					Combo := Combo mod 8;
					if (Integer(Combo) /= Program(Printed)) then
						LastMatch := 15 - Printed;
					end if;
					Printed := Printed + 1;
				when 6 => -- bdv
					RegisterB := RegisterA / 2**Integer(Combo);
				when 7 => -- cdv
					RegisterC := RegisterA / 2**Integer(Combo);
				when others => RegisterA := RegisterA;
			end case;
	
			Pc := Pc + 2;
		end loop;
	end loop;
end Program;
