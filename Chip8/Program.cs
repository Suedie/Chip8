using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Chip8;

class Program
{

    public static void Main(string[] args)
    {
        float accumulatedTime = 0f;
        float targetFrameTime = 1f/60f;

        Window.Init(1280, 640, "CHIP-8");

        IFont font = new Font();
        Memory memory = new Memory(font);
        Display display = new Display();
        Keypad keypad = new Keypad();

        DelayTimer delay = new DelayTimer();
        SoundTimer sound = new SoundTimer();

        delay.init(Raylib_CSharp.Time.GetFrameTime());
        sound.init(Raylib_CSharp.Time.GetFrameTime());

        Processor game = new Processor(memory, display, keypad, delay, sound);
        game.LoadGame("/home/deck/vscodeprojects/Chip8/Chip8/roms/6-keypad.ch8");
        
        Raylib_CSharp.Time.SetTargetFPS(60);


        while (!Window.ShouldClose())
        {
            delay.Update(Raylib_CSharp.Time.GetFrameTime());
            sound.Update(Raylib_CSharp.Time.GetFrameTime());

            accumulatedTime += Raylib_CSharp.Time.GetFrameTime();

            game.Decode(game.Fetch());
            
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.White);

            DrawMatrix(game.GetScreenMatrix());
            
            Graphics.EndDrawing();
        }
        
        Window.Close();
    }

    public static void DrawMatrix(byte[,] pixels) {

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
}