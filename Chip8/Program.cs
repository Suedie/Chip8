using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

using Chip8.src;
using Chip8.src.frontend;
using Chip8.src.rendering;

namespace Chip8;

class Program
{
    public static int WindowWidth{get;set;} = 800;
    public static int WindowHeight{get;set;} = 600;
    public static float accumulatedTime = 0f;
    public static readonly string GAME_DIRECTORY = "./games";


    public static void Main()
    {
        int targetFPS = 60;

        Directory.CreateDirectory(GAME_DIRECTORY);
        
        Raylib_CSharp.Time.SetTargetFPS(targetFPS);

        Window.Init(WindowWidth, WindowHeight, "CHIP-8");
        Window.SetMaxSize(Window.GetMonitorWidth(Window.GetCurrentMonitor()), Window.GetMonitorHeight(Window.GetCurrentMonitor()));

        Input.SetExitKey(KeyboardKey.Null);

        Chip8Core core = new(targetFPS);
        Chip8Renderer renderer = new(core);
        SceneManager sceneManager = new(core, renderer);


        while (!Window.ShouldClose()) {
            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.Black);

            accumulatedTime += Raylib_CSharp.Time.GetFrameTime();
            if(accumulatedTime >= (1f / targetFPS)) {
                sceneManager.Run();
                accumulatedTime -= 1f / targetFPS;
            }
            Graphics.EndDrawing();
        }
        Window.Close();
    }
}