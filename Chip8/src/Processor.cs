using Raylib_CSharp.Interact;

namespace Chip8.src;

class Processor {
    public uint PC = 0x200;

    public ushort I = 0;

    public byte[] Registers = new byte[0x10];

    private readonly Random _rnd = new Random();

    private readonly Memory _memory;

    private readonly Display _display;

    private readonly Keypad _keyboard;

    private readonly DelayTimer _delay;

    private readonly SoundTimer _sound;
    public Processor (Memory memory, Display display, Keypad keyboard, DelayTimer delay, SoundTimer sound) {
        this._memory = memory;
        this._display = display;
        this._keyboard = keyboard;
        this._delay = delay;
        this._sound = sound;
    }

    public void LoadGame(string filepath) {
        _memory.LoadRom(filepath);
    }

    public byte[,] GetScreenMatrix() {
        return _display.Pixels;
    }

    public uint Fetch() {
        uint part1 = _memory.RAM[PC];
        part1 <<= 8;
        uint opcode = part1 + _memory.RAM[PC + 1];

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
            //0NNN unimplemented in modern versions of interpreter
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

                    case 0xE:
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
                switch (NN) {
                    case 0x9E:
                    SkipIfKeyPressed(X);
                    break;

                    case 0xA1:
                    SkipIfKeyUp(X);
                    break;
                }
            break;

            case 0xF:
                switch (NN) {
                    case 0x07:
                    ReadTimer(X);
                    break;

                    case 0x15:
                    SetDelayTimer(X);
                    break;

                    case 0x18:
                    SetSoundTimer(X);
                    break;

                    case 0x1E:
                    AddToIndex(X);
                    break;

                    case 0x0A:
                    WaitForKey(X);
                    break;

                    case 0x29:
                    GetFontCharacter(X);
                    break;

                    case 0x33:
                    BinaryCodedDecimalConversion(X);
                    break;

                    case 0x55:
                    StoreRegistersIntoMemory(X);
                    break;

                    case 0x65:
                    LoadMemoryIntoRegisters(X);
                    break;
                }
            break;
        }
    }

    //00E0
    private void ClearDisplay() {
        _display.ClearDisplay();
    }

    //00EE
    private void Return() {
        PC = _memory.Pop();
    }

    //1NNN
    private void Jump(uint NNN) {
        PC = NNN;
    }

    //2NNN
    private void CallSubroutine(uint NNN) {
        _memory.Push((ushort)PC);
        PC = NNN;
    } 

    //3XNN
    private void SkipIfEqual(uint X, uint NN) {
        if (Registers[X] == NN)
            PC += 2;
    }

    //4XNN
    private void SkipIfNotEqual(uint X, uint NN) {
        if (Registers[X] != NN)
            PC += 2;
    }

    //5XY0
    private void SkipIfRegistersEqual(uint X, uint Y) {
        if(Registers[X] == Registers[Y])
            PC+= 2;
    }

    //9XY0
    private void SkipIfRegistersNotEqual(uint X, uint Y) {
        if(Registers[X] != Registers[Y])
            PC+= 2;
    }

    //6XNN
    private void SetRegister(uint X, uint NN) {
        Registers[X] = (byte) NN;
    }

    //7XNN
    private void AddToRegister(uint X, uint NN) {
        Registers[X] += (byte) NN;
    }

    //8XY0
    private void SetVXToVY(uint X, uint Y) {
        Registers[X] = Registers[Y];
    }

    //8XY1
    private void BinaryOR(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] | Registers[Y]);
    }

    //8XY2
    private void BinaryAND(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] & Registers[Y]);
    }

    //8XY3
    private void LogicalXOR(uint X, uint Y) {
        Registers[X] = (byte) (Registers[X] ^ Registers[Y]);
    }

    //8XY4
    private void AddRegisterVYToVX(uint X, uint Y) {
        bool overflows = Registers[X] + Registers[Y] > 255;

        Registers[X] = (byte) ((Registers[X] + Registers[Y]) % 256);

        if (overflows) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }
    }

    //8XY5
    private void SubtractVY(uint X, uint Y) {
        bool notUnderflows = Registers[X] >= Registers[Y];

        Registers[X] = (byte) ((Registers[X] - Registers[Y]) % 256);

        if (notUnderflows) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }
    }

    //8XY7
    private void SubtractVX(uint X, uint Y) {
        bool notUnderflows = Registers[Y] >= Registers[X];

        Registers[X] = (byte) ((Registers[Y] - Registers[X]) % 256);

        if (notUnderflows) {
            Registers[0xF] = 1;
        } else {
            Registers[0xF] = 0;
        }

    }

    //8XY6
    private void ShiftRight(uint X, uint Y) {
        Registers[X] = Registers[Y];

        byte underflow = (byte) (Registers[X] & 0b_0000_0001);

        Registers[X] = (byte) (Registers[X] >> 1);

        Registers[0xF] = underflow;
    }

    //8XYE
    private void ShiftLeft(uint X, uint Y) {
        Registers[X] = Registers[Y];

        byte overflow = (byte) ((Registers[X] & 0b_1000_0000) >> 7);

        Registers[X] = (byte) (Registers[X] << 1);

        Registers[0xF] = overflow;
    }

    //ANNN
    private void SetIndex(uint NNN) {
        I = (ushort) NNN;
    }

    //BNNN
    private void JumpOffset(uint NNN) {
        PC = NNN + Registers[0];
    }

    //CXNN
    private void RandomNumber(uint X, uint NN) {
        int num = _rnd.Next(0xFF);

        Registers[X] = (byte) (num & NN);
    }

    //DXYN
    private void DrawToDisplay(uint X, uint Y, uint N) {
        int posX = Registers[X] % 64;
        int posY = Registers[Y] % 32;

        Registers[0xF] = 0;

        for (int h = 0; h < N; h++) {
            int spriteRow = _memory.RAM[I + h];
            for (int w = 0; w < 8; w++) {
                int pixel = (spriteRow >> (7 - w)) & 1;

                if (posX + w >= 64 || posY + h >= 32) {
                    break;
                }

                if (pixel == 1 && _display.Pixels[posX+w, posY+h] == 1) {
                    _display.Pixels[posX + w, posY + h] = 0;
                    Registers[0xF] = 1;
                } else if (pixel == 1) {
                    _display.Pixels[posX + w, posY + h] = 1;
                }
            }
        }
    }

    //EX9E
    private void SkipIfKeyPressed(uint X) {
        byte key = (byte) (Registers[X] & 0x0F);

        if (Input.IsKeyDown( (KeyboardKey)_keyboard.HexToKey(key))) {
            PC += 2;
        }
    }

    //EXA1
    private void SkipIfKeyUp(uint X) {
        byte key = (byte) (Registers[X] & 0x0F);

        if (Input.IsKeyUp( (KeyboardKey) _keyboard.HexToKey(key))) {
            PC += 2;
        }
    }

    //FX07
    private void ReadTimer(uint X) {
        Registers[X] = _delay.Register;
    }

    //FX15
    private void SetDelayTimer(uint X) {
        _delay.Register = Registers[X];
    }

    //FX18
    private void SetSoundTimer(uint X) {
        _sound.Register = Registers[X];
    }

    //FX1E
    private void AddToIndex(uint X) {
        I += Registers[X];

        if (I > 0x0FFF) {
            Registers[0xF] = 1;
        }
    }

    //FX0A
    private void WaitForKey(uint X) {
        int key = Input.GetKeyPressed();
        if (_keyboard.ContainsKey(key)) {
            Registers[X] = _keyboard.KeyToHex(key);
        } else {
            PC -= 2;
        }
    }

    // private void WaitForKey(uint X) {
    //     if (Input.IsKeyDown(KeyboardKey.One))
    //         Registers[X] = 0x01;
    //     else if (Input.IsKeyDown(KeyboardKey.Two))
    //         Registers[X] = 0x02;
    //     else if (Input.IsKeyDown(KeyboardKey.Three))
    //         Registers[X] = 0x03;
    //     else if (Input.IsKeyDown(KeyboardKey.Four))
    //         Registers[X] = 0x0C;
    //     else if (Input.IsKeyDown(KeyboardKey.Q))
    //         Registers[X] = 0x04;
    //     else if (Input.IsKeyDown(KeyboardKey.W))
    //         Registers[X] = 0x05;
    //     else if (Input.IsKeyDown(KeyboardKey.E))
    //         Registers[X] = 0x06;
    //     else if (Input.IsKeyDown(KeyboardKey.R))
    //         Registers[X] = 0x0D;
    //     else if (Input.IsKeyDown(KeyboardKey.A))
    //         Registers[X] = 0x07;
    //     else if (Input.IsKeyDown(KeyboardKey.S))
    //         Registers[X] = 0x08;
    //     else if (Input.IsKeyDown(KeyboardKey.D))
    //         Registers[X] = 0x09;
    //     else if (Input.IsKeyDown(KeyboardKey.F))
    //         Registers[X] = 0x0E;
    //     else if (Input.IsKeyDown(KeyboardKey.Z))
    //         Registers[X] = 0x0A;
    //     else if (Input.IsKeyDown(KeyboardKey.X))
    //         Registers[X] = 0x00;
    //     else if (Input.IsKeyDown(KeyboardKey.C))
    //         Registers[X] = 0x0B;
    //     else if (Input.IsKeyDown(KeyboardKey.V))
    //         Registers[X] = 0x0F;
    //     else
    //         PC -= 2;
    // }

    //FX29
    private void GetFontCharacter(uint X) {
        I = (ushort) (0x50 + ((Registers[X] & 0x0F) * 0x5));
    }

    //FX33
    private void BinaryCodedDecimalConversion(uint X) {
        byte num = Registers[X];

        int ones = num % 10;
        int tens = num % 100;
        int hundreds = num;

        _memory.RAM[I] = (byte) (hundreds / 100);
        _memory.RAM[I+1] = (byte) (tens / 10);
        _memory.RAM[I+2] = (byte) ones;
    }

    //FX55
    private void StoreRegistersIntoMemory(uint X) {
        for (int i = 0; i <= X; i++ ) {
            _memory.RAM[I+i] = Registers[i];
        }
    }

    //FX65
    private void LoadMemoryIntoRegisters(uint X) {
        for (int i = 0; i <= X; i++ ) {
            Registers[i] = _memory.RAM[I+i];
        }
    }

}