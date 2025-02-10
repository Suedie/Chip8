namespace Chip8.src;

public class DelayTimer : Timer {
    public override void Update() {
        Register -= 1;
    }
}