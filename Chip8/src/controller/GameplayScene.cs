using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.controller;

class GameplayScene : IScene{
    private IFacade model;
    public SceneIdentifier ThisScene{get;} = SceneIdentifier.GameScreen;
    public GameplayScene(IFacade model) {
        this.model = model;
    }

    public SceneIdentifier Update() {
        model.Update();
        DrawMatrix(model.GetScreenMatrix());

        if (Input.IsKeyPressed(KeyboardKey.Escape)) {
            return Back();
        }

        return ThisScene;
    }

    public SceneIdentifier Back() {
        return SceneIdentifier.PauseMenu;
    }

    private static void DrawMatrix(byte[,] pixels) {

        for (int i = 0; i < pixels.GetLength(0); i++) {
            for (int j = 0; j < pixels.GetLength(1); j++) {
                if(pixels[i, j] == 1) {
                    Graphics.DrawRectangle(i*20, j*20, 20, 20, Color.RayWhite);
                }
            }
        }
    }
}