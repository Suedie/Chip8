namespace Chip8;

class Processor {
    public int PC = 0;

    public ushort I = 0;

    public Memory memory = new Memory();

    public uint Fetch() {

        uint part1 = memory.MemoryArray[PC];
        part1 = part1 << 4;
        uint opcode = part1 + memory.MemoryArray[PC + 1];

        PC += 2;

        return opcode;
    }

    public void Decode(uint opcode) {
        uint firstNibble = (opcode & 0b_1100_0000) >> 6;
        uint secondNibble = (opcode & 0b_0011_0000) >> 4;
        uint thirdNibble = (opcode & 0b_0000_1100) >> 2;
        uint fourthNibble = opcode & 0b_0000_0011;

        switch (firstNibble) {
            case 0x0:
            break;
            case 0x1:
            break;
            case 0x2:
            break;
            case 0x3:
            break;
            case 0x4:
            break;
            case 0x5:
            break;
            case 0x6:
            break;
            case 0x7:
            break;
            case 0x8:
            break;
            case 0x9:
            break;
            case 0xA:
            break;
            case 0xB:
            break;
            case 0xC:
            break;
            case 0xD:
            break;
            case 0xE:
            break;
            case 0xF:
            break;
        }
    }

}