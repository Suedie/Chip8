using Raylib_CSharp.Windowing;
namespace Chip8;

public abstract class Timer {

    private float _frameTime;

    private byte _register = 0xFF;

    private const float Frequency = 1/60;

    public void init(float frameTime) {
        _frameTime = frameTime;
    }

    public void pause() {
        _frameTime = 0;
    }
    
    public void Update(float frameTime) {
        _frameTime += frameTime;

        if (_frameTime >= Frequency) {
            _register = (byte) (_register - 1);
            if (_register == 0) {
                _register = 255;
            }
            _frameTime -= Frequency;
        }
    }

    public void setTimer(byte value) {
        _register = value;
    }

}