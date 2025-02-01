using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using Raylib_CSharp.Audio;

using Chip8.src;

namespace Chip8;

class Program
{

    private static bool _debugEnabled = true;
    private static bool _isPaused = false;

    private static float _accumulatedTime = 0f;
    private static int _targetFps = 60;
    private static float _targetCPUCyclesPerSecond = 1000f;
    private static float _cyclesPerFrame = _targetCPUCyclesPerSecond / (float) _targetFps;

    private static Memory _memory = new Memory(new Font());
    private static Display _display = new Display();
    private static Keypad _keypad = new Keypad();

    private static DelayTimer _delay = new DelayTimer();
    private static SoundTimer _sound = new SoundTimer();

    private static Processor _game = new Processor(_memory, _display, _keypad, _delay, _sound);

    public static string GamePath;


    public static void Main()
    {
        GamePath = "/home/deck/vscodeprojects/Chip8/Chip8/roms/5-quirks.ch8";
        _game.LoadGame(GamePath);
        
        Raylib_CSharp.Time.SetTargetFPS(_targetFps);

        Window.Init(1280, 640, "CHIP-8");

        while (!Window.ShouldClose()) {
            if (!_debugEnabled) {
                EmuLoop(_memory, _display, _keypad, _delay, _sound, _game, _accumulatedTime, _targetFps, _cyclesPerFrame);
            } else if (_debugEnabled) {
                DebugLoop(_memory, _display, _keypad, _delay, _sound, _game, _accumulatedTime, _targetFps, _cyclesPerFrame);
            }
        }

        Window.Close();
    }

    private static void EmuLoop(Memory memory, Display display, Keypad keypad, DelayTimer delayTimer, SoundTimer soundTimer, Processor game,
     float accumulatedTime, int targetFps, float cyclesPerFrame) {

        Graphics.BeginDrawing();
        Graphics.ClearBackground(Color.White);

        accumulatedTime += Raylib_CSharp.Time.GetFrameTime();

        //If targetFPS == 60 then update at 60hz
        while (accumulatedTime >= 1f / targetFps) {
            delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
            soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

            //CPU runs at 1000 Instructions per second
            //At 60 FPS that becomes 17 instructions per frame
            for(int i = 0; i < cyclesPerFrame; i++) {
                game.Decode(game.Fetch());
            }
            accumulatedTime -= 1f / targetFps;

            if (Input.IsKeyPressed(KeyboardKey.Backspace))
                _debugEnabled = !_debugEnabled;
        }

        DrawMatrix(game.GetScreenMatrix());
                
        Graphics.EndDrawing();
    }

    private static void DebugLoop(Memory memory, Display display, Keypad keypad, DelayTimer delayTimer, SoundTimer soundTimer, Processor game,
     float accumulatedTime, int targetFps, float cyclesPerFrame) {
        
        Graphics.BeginDrawing();
        Graphics.ClearBackground(Color.White);

        accumulatedTime += Raylib_CSharp.Time.GetFrameTime();

        while (accumulatedTime >= 1f / targetFps) {
            DebugControls();
            if (!_isPaused) {
                delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
                soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

                for(int i = 0; i < cyclesPerFrame; i++) {
                    game.Decode(game.Fetch());
                }
            } else if (_isPaused) { //Manually step through each opcode and print it
                if (Input.IsKeyPressed(KeyboardKey.Space)) {
                    delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
                    soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

                    game.Decode(game.Fetch());
                    game.PrintCurrentOpcode();
                }
            }
            accumulatedTime -= 1f / targetFps;

            if (Input.IsKeyPressed(KeyboardKey.Backspace))
                _debugEnabled = !_debugEnabled;
        }

        DrawMatrix(game.GetScreenMatrix());
                
        Graphics.EndDrawing();
     }

    private static void DrawMatrix(byte[,] pixels) {

        Graphics.BeginDrawing();
        Graphics.ClearBackground(Color.Black);

        for (int i = 0; i < pixels.GetLength(0); i++) {
            for (int j = 0; j < pixels.GetLength(1); j++) {
                if(pixels[i, j] == 1) {
                    Graphics.DrawRectangle(i*20, j*20, 20, 20, Color.White);
                }
            }
        }
    }

    //When debugmode is enabled allows manual control of printing opcodes from Memory
    public static void DebugControls() {
        if (Input.IsKeyPressed(KeyboardKey.P)) {
            _isPaused = !_isPaused;
        }
        if (Input.IsKeyPressed(KeyboardKey.M)) {
            _game.PrintCurrentOpcode();
        }
        if (Input.IsKeyPressed(KeyboardKey.N)) {
            _game.PrintRAM();
        }
        if (Input.IsKeyPressed(KeyboardKey.B)) {
            _game.PrintFollowingMemory();
        }
        if (Input.IsKeyPressed(KeyboardKey.K)) {
            _game.PrintMemorySnippet(16);
        }
    }
}