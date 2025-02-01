namespace Chip8.src;

class SoundTimer : Timer {

    public override void Update(float frameTime) {
        FrameTime += frameTime;

        if (FrameTime >= Frequency) {
            Register = (byte) (Register - 1);
            if (Register == 0) {
                Register = 255;
            }
            FrameTime -= Frequency;
        }

        if (Register > 0) {
            Console.Beep(); //Doesn't work on Linux but at least compiles
        }
    }



}