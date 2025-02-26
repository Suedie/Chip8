namespace Chip8.src.emulator;

public abstract class Timer {
    public byte Register = 0xFF;
    
    public abstract void Update();

    public void SetTimer(byte value) {
        Register = value;
    }

}