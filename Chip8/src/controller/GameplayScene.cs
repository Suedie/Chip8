using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.controller;

class GameplayScene : IScene{
    private ICore _emuCore;
    public SceneIdentifier ThisScene{get;} = SceneIdentifier.GameScreen;
    public GameplayScene(ICore emuCore) {
        _emuCore = emuCore;
    }

    public SceneIdentifier Update() {
        _emuCore.Update();
        DrawMatrix(_emuCore.GetScreenMatrix());

        if (Input.IsKeyPressed(KeyboardKey.Escape)) {
            return Back();
        }

        return ThisScene;
    }

    public SceneIdentifier Back() {
        return SceneIdentifier.PauseMenu;
    }

    private void DrawMatrix(byte[,] pixels) {
        int scale = Program.WindowWidth / 64;
        int xOffset = (Program.WindowWidth - (64 * scale)) / 2;
        int yOffset = (Program.WindowHeight - (32 * scale)) / 2;

        for (int pixelRow = 0; pixelRow < pixels.GetLength(0); pixelRow++) {
            for (int pixelColumn = 0; pixelColumn < pixels.GetLength(1); pixelColumn++) {
                if(pixels[pixelRow, pixelColumn] == 1) {
                    Graphics.DrawRectangle((pixelRow*scale) + xOffset, (pixelColumn*scale) + yOffset, scale, scale, Color.RayWhite);
                }
            }
        }
    }
}