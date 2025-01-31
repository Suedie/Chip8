using Raylib_CSharp.Windowing;
namespace Chip8;

public abstract class Timer {

    public float FrameTime;

    public byte Register = 0xFF;

    public const float Frequency = 1/60;

    public void init(float frameTime) {
        FrameTime = frameTime;
    }

    public void pause() {
        FrameTime = 0;
    }
    
    public abstract void Update(float frameTime);

    public void SetTimer(byte value) {
        Register = value;
    }

}