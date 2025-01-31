using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Chip8;

class Program
{

    public static void Main(string[] args)
    {
        float accumulatedTime = Raylib_CSharp.Time.GetFrameTime();

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
        game.LoadGame("../Chip8/roms/5-quirks.ch8");

        while (!Window.ShouldClose())
        {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.White);

            delay.Update(Raylib_CSharp.Time.GetFrameTime());
            sound.Update(Raylib_CSharp.Time.GetFrameTime());

            accumulatedTime += Raylib_CSharp.Time.GetFrameTime();

            if (accumulatedTime >= (float) 1/60) {
                game.Decode(game.Fetch());
                accumulatedTime -= (float) 1/60;
            }

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