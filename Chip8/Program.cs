using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using Chip8;

namespace Chip8;

class Program
{

    public static void Main(string[] args)
    {
        Window.Init(832, 416, "CHIP-8");

        IFont font = new Font();
        Memory memory = new Memory(font);
        Display display = new Display();

        Processor game = new Processor(memory, display);
        game.memory.LoadRom("/home/deck/vscodeprojects/Chip8/Chip8/roms/IBM Logo.ch8");

        while (!Window.ShouldClose())
        {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.White);

            game.Decode(game.Fetch());

            Draw(game.display.Pixels);
            
            Graphics.EndDrawing();
        }
        
        Window.Close();
    }

    public static void Draw(byte[,] pixels) {

        Graphics.BeginDrawing();
        Graphics.ClearBackground(Color.Black);

        for (int i = 0; i < pixels.GetLength(0); i++) {
            for (int j = 0; j < pixels.GetLength(1); j++) {
                if(pixels[i, j] == 1) {
                    Graphics.DrawRectangle(i*13, j*13, 13, 13, Color.White);
                }
            }
        }
    }
}