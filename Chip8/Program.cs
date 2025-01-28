using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Chip8;

class Program
{
    public static void Main(string[] args)
    {
        Window.Init(800, 600, "Hello World!");

        while (!Window.ShouldClose())
        {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.White);
            
            Graphics.DrawText("Hello, World!", 12, 12, 20, Color.Red);
            
            Graphics.EndDrawing();
        }
        
        Window.Close();
    }
}