using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using Chip8;

namespace Chip8;

class Program
{

    public static void Main(string[] args)
    {
        Window.Init(800, 600, "Hello World!");

        Processor game = new Processor();
        game.memory.LoadRom("/home/deck/vscodeprojects/Chip8/Chip8/roms/IBM Logo.ch8");

        while (!Window.ShouldClose())
        {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.White);

            game.Decode(game.Fetch());
            Console.WriteLine(game.display.PixelsToString());
            
            Graphics.DrawText(game.display.PixelsToString(), 0, 0, 20, Color.Red);
            
            Graphics.EndDrawing();
        }
        
        Window.Close();
    }
}