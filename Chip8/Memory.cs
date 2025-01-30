namespace Chip8;

public class Memory
{
    //Chip-8 has a 12 bit index register which means 4096 possible addresses in memory
    //Each address is 1-byte
    //This translates to 4kB of RAM
    public byte[] RAM{get;} = new Byte [0x1000];

    private readonly Stack<ushort> _stack = new Stack<ushort>(16);

    public Memory(IFont font) {
        LoadFont(font);
    }

    //Puts the font data at 0x50 (80) which has become an unofficial standard
    public void LoadFont(IFont font) {
        byte[] _tmpFont = font.TextFont;

        Array.Copy(_tmpFont, 0, RAM, 0x50, _tmpFont.Length);
    }
    
    //Loads a rom into memory starting at adress 0x200 (512)
    //Some early Chip-8 games expect to be loaded into this address
    public void LoadRom(string filepath) {
        byte[] rom = File.ReadAllBytes(filepath);

        Array.Copy(rom, 0, RAM, 0x200, rom.Length);
    }

    public void Push(ushort value) {
        _stack.Push(value);
    }

    public ushort Pop() {
        return _stack.Pop();
    }

}