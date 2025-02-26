namespace Chip8.src.emulator;

public interface IFont {

    //Interface for making implementation of custom fonts easier
    public byte[] TextFont {get; set;}
}