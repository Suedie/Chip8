namespace Chip8;

public class Memory
{
    //Chip-8 has a 12 bit index register which means 4096 possible addresses in memory
    //This translates to 4kB of RAM
    public int[] MemoryArray = new int[0x1000];
    
    
}