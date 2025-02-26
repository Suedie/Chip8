namespace Chip8.src.emulator;

public class DelayTimer : Timer {
    public override void Update() {
        Register -= 1;
    }
}