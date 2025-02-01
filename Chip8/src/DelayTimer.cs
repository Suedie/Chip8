namespace Chip8.src;

public class DelayTimer : Timer {

    public override void Update(float frameTime) {
        FrameTime += frameTime;

        if (FrameTime >= Frequency) {
            Register = (byte) (Register - 1);
            if (Register == 0) {
                Register = 255;
            }
            FrameTime -= Frequency;
        }
    }
}