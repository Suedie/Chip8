using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

using Chip8.src;
using Chip8.src.controller;

namespace Chip8;

class Program
{
    public static float accumulatedTime = 0f;
    public static string? GamePath;


    public static void Main()
    {
        int targetFPS = 60;

        IFacade core = new Chip8Core(targetFPS);
        SceneManager sceneManager = new SceneManager(core);

        Console.WriteLine("Enter a filepath for the ROM:");
        GamePath = Console.ReadLine();

        sceneManager.LoadRom(GamePath);
        
        Raylib_CSharp.Time.SetTargetFPS(targetFPS);

        Window.Init(1280, 640, "CHIP-8");

        while (!Window.ShouldClose()) {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.Black);

            accumulatedTime += Raylib_CSharp.Time.GetFrameTime();
            if(accumulatedTime >= (1f / (float) targetFPS)) {
                sceneManager.Run();
                accumulatedTime -= 1f / targetFPS;
            }
            Graphics.EndDrawing();
        }

        Window.Close();
    }
}