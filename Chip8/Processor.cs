using System.Diagnostics;

namespace Chip8;

class Processor {
    public uint PC = 0x200;

    public ushort I = 0;

    public byte[] Registers = new byte[0x10];

    public Memory memory;

    public Display display;
    public Processor (Memory memory, Display display) {
        this.memory = memory;
        this.display = display;
    }

    public uint Fetch() {

        uint part1 = memory.RAM[PC];
        part1 = part1 << 8;
        uint opcode = part1 + memory.RAM[PC + 1];

        PC += 2;

        return opcode;
    }

    public void Decode(uint opcode) {
        uint firstNibble = (opcode & 0xF000) >> 12;
        uint X = (opcode & 0x0F00) >> 8; //X is the address to Register VX
        uint Y = (opcode & 0x00F0) >> 4; //Y is the address to Register VY
        uint N = opcode & 0x000F; //fourth nibble
        uint NN = opcode & 0x00FF;
        uint NNN = opcode & 0x0FFF;

        switch (firstNibble) {
            case 0x0 when opcode == 0x00E0:
            ClearDisplay();
            break;

            case 0x0 when opcode == 0x00EE:
            Return();
            break;

            case 0x0:
            //Unimplemented in modern versions
            break;

            case 0x1:
            Jump(NNN);
            break;

            case 0x2:
            CallSubroutine(NNN);
            break;

            case 0x3:
            SkipIfEqual(X, NN);
            break;

            case 0x4:
            SkipIfNotEqual(X, NN);
            break;

            case 0x5:
            SkipIfRegistersEqual(X, Y);
            break;

            case 0x6:
            SetRegister (X, NN);
            break;

            case 0x7:
            AddToRegister(X, NN);
            break;

            case 0x8:
                switch (N) {
                    case 0x0:
                    SetVXToVY(X, Y);
                    break;

                    case 0x1:
                    BinaryOR(X, Y);
                    break;

                    case 0x2:
                    BinaryAND(X, Y);
                    break;

                    case 0x3:
                    LogicalXOR(X, Y);
                    break;

                    case 0x4:
                    AddRegisterVYToVX(X, Y);
                    break;

                    case 0x5:
                    SubtractVY(X, Y);
                    break;

                    case 0x6:
                    ShiftRight(X, Y);
                    break;

                    case 0x7:
                    SubtractVX(X, Y);
                    break;

                    case 0x8:
                    ShiftLeft(X, Y);
                    break;
                }
            break;

            case 0x9:
            SkipIfRegistersNotEqual(X, Y);
            break;

            case 0xA:
            SetIndex(NNN);
            break;

            case 0xB:
            JumpOffset(NNN); //Other possible implementation
            break;

            case 0xC:
            RandomNumber(X, NN);
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
        display.ClearDisplay();
    }

    private void Return() {
        PC = memory.Pop();
    }

    private void CallSubroutine(uint NNN) {
        memory.Push((ushort)PC);
        PC = NNN;
    } 

    private void Jump(uint NNN) {
        PC = NNN;
    }

    private void SkipIfEqual(uint X, uint NN) {
        if (Registers[X] == NN)
            PC += 2;
    }

    private void SkipIfNotEqual(uint X, uint NN) {
        if (Registers[X] != NN)
            PC += 2;
    }

    private void SkipIfRegistersEqual(uint X, uint Y) {
        if(Registers[X] == Registers[Y])
            PC+= 2;
    }

    private void SkipIfRegistersNotEqual(uint X, uint Y) {
        if(Registers[X] != Registers[Y])
            PC+= 2;
    }

    private void SetRegister(uint X, uint NN) {
        Registers[X] = (byte) NN;
    }

    private void AddToRegister(uint X, uint NN) {
        Registers[X] += (byte) NN;
    }

    private void SetVXToVY(uint X, uint Y) {
        Registers[X] = Registers[Y];
    }

    private void BinaryOR(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] | Registers[Y]);
    }

    private void BinaryAND(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] & Registers[Y]);
    }

    private void LogicalXOR(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] ^ Registers[Y]);
    }

    private void AddRegisterVYToVX(uint X, uint Y) {
        if (Registers[X] + Registers[Y] > 255) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }

        Registers[X] = (byte) ((Registers[X] + Registers[Y]) % 255);
    }

    private void SubtractVY(uint X, uint Y) {
        if (Registers[X] >= Registers[Y]) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }

        Registers[X] = (byte) ((Registers[X] - Registers[Y]) % 255);
    }

    private void SubtractVX(uint X, uint Y) {
        if (Registers[Y] >= Registers[X]) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }

        Registers[X] = (byte) ((Registers[Y] - Registers[X]) % 255);
    }

    private void ShiftRight(uint X, uint Y) {
        Registers[X] = Registers[Y];

        Registers[0xF] = (byte) (Registers[X] & 0b_0000_0001);

        Registers[X] = (byte) (Registers[X] >> 1);
    }

    private void ShiftLeft(uint X, uint Y) {
        Registers[X] = Registers[Y];

        Registers[0xF] = (byte) (Registers[X] & 0b_1000_0000);

        Registers[X] = (byte) (Registers[X] << 1);
    }


    private void SetIndex(uint NNN) {
        I = (ushort) NNN;
    }

    private void JumpOffset(uint NNN) {
        PC = NNN + Registers[0];
    }

    private void RandomNumber(uint X, uint NN) {
        Random rnd = new Random();
        int num = rnd.Next(0xFF);

        Registers[X] = (byte) (NN & num);
    }

    private void DrawToDisplay(uint X, uint Y, uint N) {
        int posX = Registers[X] % 64;
        int posY = Registers[Y] % 32;

        Registers[0xF] = 0;

        for (int h = 0; h < N; h++) {
            int spriteRow = memory.RAM[I + h];
            for (int w = 0; w < 8; w++) {
                int pixel = (spriteRow >> (7 - w)) & 1;

                if (posX + w >= 64 || posY + h >= 32) {
                    break;
                }

                if (pixel == 1 && display.Pixels[posX+w, posY+h] == 1) {
                    display.Pixels[posX + w, posY + h] = 0;
                    Registers[0xF] = 1;
                } else if (pixel == 1) {
                    display.Pixels[posX + w, posY + h] = 1;
                }
            }
        }
    }

    private void AddToIndex(uint X) {
        if (I + Registers[X] > 0x0FFF) {
            Registers[0xF] = 1;
        }

        I += (byte) (Registers[X] % 0xFFFF);
    }

}