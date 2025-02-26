# Chip-8 Emulator/Interpreter
This is a simple Chip-8 emulator that implements the standard 35 Chip-8 opcodes. It is written entirely in C# and uses the Raylib-Csharp library for graphics and inputs

# Playing a game

To play a game drop a .ch8 file in the games folder at the root directory, then select it in the "Load Game" menu

# Controls

Chip-8 originally used a hexadecimal keypad with keys ranging from 0 to F. This interpreter follows the modern standard of mapping these keys to the left side of the keyboard, using 1-4, Q-R, A-F and Z-V on a QWERTY layout keyboard to match the layout of the hex keypad.

1 	2 	3 	C || 1 	2 	3 	4

4 	5 	6 	D || Q 	W 	E 	R

7 	8 	9 	E || A 	S 	D 	F

A 	0 	B 	F || Z 	X 	C 	V

**Enter**: Reloads the current game

# Debug
Pressing backspace will put the game into debug mode. In debug mode a few extra controls are available to manipulate the game:

**Backspace**: Enter/Exit debug mode

**P**: Halt/unhalt the game

**Space**: When halted space will manually step through the game one instruction as well as display the current instruction opcode in the console

**M**: Displays the current opcode being executed and its memory address in the console

**N**: Prints the entire memory with all opcodes

**B**: Prints all bytes in memory following the current location

**K**: Will print the current and next 9 opcodes in memory 
