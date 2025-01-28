namespace Chip8;

public class Memory
{
    //Chip-8 has a 12 bit index register which means 4096 possible addresses in memory
    //Each address is 1-byte
    //This translates to 4kB of RAM
    public byte[] MemoryArray = new Byte [0x1000];
    
    
}