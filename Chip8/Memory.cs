namespace Chip8;

public class Memory
{
    //Chip-8 has a 12 bit index register which means 4096 possible addresses in memory
    //Each address is 1-byte
    //This translates to 4kB of RAM
    public byte[] MemoryArray = new Byte [0x1000];

    public Memory() {
        LoadFont();
    }

    //Puts the font data at 0x50 (80) which has become an unofficial standard
    public void LoadFont() {
        Chip8.Font _font = new();

        byte[] _tmpFont = _font.TextFont;

        Array.Copy(_tmpFont, 0, MemoryArray, 0x50, _tmpFont.Length);
    }
    
    //Loads a rom into memory starting at adress 0x200 (512)
    //Some early Chip-8 games expect to be loaded into this address
    public void LoadRom(string filepath) {
        byte[] rom = File.ReadAllBytes(filepath);

        Array.Copy(rom, 0, MemoryArray, 0x200, rom.Length);
    }

}