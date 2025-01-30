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
        uint firstNibble = (opcode & 0xF000) >> 12;
        uint X = (opcode & 0x0F00) >> 8; //second nibble
        uint Y = (opcode & 0x00F0) >> 4; //third nibble
        uint N = opcode & 0x000F; //fourth nibble
        uint NN = opcode & 0x00FF;
        uint NNN = opcode & 0x0FFF;

        switch (firstNibble) {
            case 0x0 when opcode == 0x00E0:
            break;
            case 0x0 when opcode == 0x00EE:
            break;
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