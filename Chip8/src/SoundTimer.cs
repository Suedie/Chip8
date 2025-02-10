namespace Chip8.src;

class SoundTimer : Timer {

    public override void Update() {
        Register -= 1;
        if (Register > 0) {
            Console.Beep(); //Doesn't work on Linux but at least compiles
        }
    }
}