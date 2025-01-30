namespace Chip8;

class Processor {
    public uint PC = 0;

    public ushort I = 0;

    public byte[] Registers = new byte[0xF];

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
            ClearDisplay();
            break;

            case 0x0 when opcode == 0x00EE:
            break;

            case 0x0:
            break;

            case 0x1:
            Jump(NNN);
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
            SetRegister (X, NN);
            break;

            case 0x7:
            AddToRegister(X, NN);
            break;

            case 0x8:
            break;
            case 0x9:
            break;

            case 0xA:
            SetIndex(NNN);
            break;

            case 0xB:
            break;
            case 0xC:
            break;
            case 0xD:
            DrawToDisplay(X, Y, N);
            break;
            case 0xE:
            break;
            case 0xF:
            break;
        }
    }

    private void ClearDisplay() {

    }

    private void Jump(uint NNN) {
        PC = NNN;
    }

    private void SetRegister(uint X, uint NN) {
        Registers[X] = (byte) NN;
    }

    private void AddToRegister(uint X, uint NN) {
        Registers[X] += (byte) NN;
    }

    private void SetIndex(uint NNN) {
        I = (ushort) NNN;
    }

    private void DrawToDisplay(uint X, uint Y, uint N) {
        
    }

}