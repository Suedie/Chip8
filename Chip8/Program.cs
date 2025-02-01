using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using Raylib_CSharp.Audio;

using Chip8.src;

namespace Chip8;

class Program
{

    public static bool debugEnabled = true;
    public static bool isPaused = false;

    public static float accumulatedTime = 0f;
    public static int targetFps = 60;
    public static float targetCPUCyclesPerSecond = 1000f;
    public static float cyclesPerFrame = targetCPUCyclesPerSecond / (float) targetFps;

    public static Memory memory = new Memory(new Font());
    public static Display display = new Display();
    public static Keypad keypad = new Keypad();

    public static DelayTimer delay = new DelayTimer();
    public static SoundTimer sound = new SoundTimer();

    public static Processor game = new Processor(memory, display, keypad, delay, sound);


    public static void Main()
    {
        game.LoadGame("/home/deck/vscodeprojects/Chip8/Chip8/roms/5-quirks.ch8");
        
        Raylib_CSharp.Time.SetTargetFPS(targetFps);

        Window.Init(1280, 640, "CHIP-8");

        while (!Window.ShouldClose()) {
           if (Input.IsKeyPressed(KeyboardKey.L))
                debugEnabled = !debugEnabled;

            if (!debugEnabled) {
                EmuLoop(memory, display, keypad, delay, sound, game, accumulatedTime, targetFps, cyclesPerFrame);
            } else if (debugEnabled) {
                DebugLoop(memory, display, keypad, delay, sound, game, accumulatedTime, targetFps, cyclesPerFrame);
            }
        }

        Window.Close();
    }

    private static void EmuLoop(Memory memory, Display display, Keypad keypad, DelayTimer delayTimer, SoundTimer soundTimer, Processor game,
     float accumulatedTime, int targetFps, float cyclesPerFrame) {

        Graphics.BeginDrawing();
        Graphics.ClearBackground(Color.White);

        accumulatedTime += Raylib_CSharp.Time.GetFrameTime();

        while (accumulatedTime >= 1f / targetFps) {
            delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
            soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

            for(int i = 0; i < cyclesPerFrame; i++) {
                game.Decode(game.Fetch());
            }
            accumulatedTime -= 1f / targetFps;
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
            if (!isPaused) {
                delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
                soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

                for(int i = 0; i < cyclesPerFrame; i++) {
                    game.Decode(game.Fetch());
                }
            } else if (isPaused) {
                if (Input.IsKeyPressed(KeyboardKey.Space)) {
                    delayTimer.Update(Raylib_CSharp.Time.GetFrameTime());
                    soundTimer.Update(Raylib_CSharp.Time.GetFrameTime());

                    game.Decode(game.Fetch());
                    game.PrintCurrentOpcode();
                }
            }
            accumulatedTime -= 1f / targetFps;
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

    public static void DebugControls() {
        if (Input.IsKeyPressed(KeyboardKey.P)) {
            isPaused = !isPaused;
        }
        if (Input.IsKeyPressed(KeyboardKey.M)) {
            game.PrintCurrentOpcode();
        }
        if (Input.IsKeyPressed(KeyboardKey.N)) {
            game.PrintRAM();
        }
        if (Input.IsKeyPressed(KeyboardKey.B)) {
            game.PrintFollowingMemory();
        }
        if (Input.IsKeyPressed(KeyboardKey.K)) {
            game.PrintMemorySnippet(16);
        }
    }
}