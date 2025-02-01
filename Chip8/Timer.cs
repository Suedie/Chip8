using Raylib_CSharp.Windowing;
namespace Chip8;

public abstract class Timer {

    public float FrameTime = 0f;

    public byte Register = 0xFF;

    public const float Frequency = 1f/60f;

    public void Pause() {
        FrameTime = 0f;
    }
    
    public abstract void Update(float frameTime);

    public void SetTimer(byte value) {
        Register = value;
    }

}