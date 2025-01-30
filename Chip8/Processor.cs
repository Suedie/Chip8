namespace Chip8;

class Processor {
    public int PC = 0;

    public ushort I = 0;

    public Memory memory = new Memory();

    public ushort Fetch() {

        int part1 = memory.MemoryArray[PC];
        part1 = part1 << 4;
        int opcode = part1 + memory.MemoryArray[PC + 1];

        PC += 2;

        return (ushort) opcode;
    }

    

}