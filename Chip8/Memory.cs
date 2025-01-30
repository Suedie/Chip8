namespace Chip8;

public class Memory
{
    //Chip-8 has a 12 bit index register which means 4096 possible addresses in memory
    //Each address is 1-byte
    //This translates to 4kB of RAM
    public byte[] MemoryArray = new Byte [0x1000];

    //Puts the font data at 0x50 (80) which has become an unofficial standard
    public void LoadFont() {
        Chip8.Font _font = new();

        byte[] _tmpFont = _font.TextFont;

        MemoryArray.CopyTo(_tmpFont, 0x50);
    }
    
    //Loads a rom into adress 0x200 (512)
    //Some early Chip-8 games expect to be loaded into this address
    public void LoadRom(string filepath) {
        byte[] rom = File.ReadAllBytes(filepath);

        MemoryArray.CopyTo(rom, 0x200);
    }

}