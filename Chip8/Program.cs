﻿using Raylib_CSharp.Colors;
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

    
    public static void Main()
    {
        float accumulatedTime = 0f;
        int targetFps = 60;
        float targetCPUCyclesPerSecond = 1000f;
        float cyclesPerFrame = targetCPUCyclesPerSecond / (float) targetFps;

        Memory memory = new Memory(new Font());
        Display display = new Display();
        Keypad keypad = new Keypad();

        DelayTimer delay = new DelayTimer();
        SoundTimer sound = new SoundTimer();

        Processor game = new Processor(memory, display, keypad, delay, sound);
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
            if (Input.IsKeyPressed(KeyboardKey.P)) {
                isPaused = !isPaused;
            }
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

    public static void DebugControls(bool debugEnabled) {

        if (Input.IsKeyDown(KeyboardKey.P)) {

        }
    }
}