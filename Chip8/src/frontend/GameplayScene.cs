using Chip8.src.rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.frontend;

class GameplayScene : IScene{
    private ICore _emuCore;
    private IRenderer _emuRenderer;
    public SceneIdentifier ThisScene{get;} = SceneIdentifier.GameScreen;
    public GameplayScene(ICore emuCore, IRenderer emuRenderer) {
        _emuCore = emuCore;
        _emuRenderer = emuRenderer;
    }

    public SceneIdentifier Update() {
        _emuCore.Update();
        _emuRenderer.Update();

        if (Input.IsKeyPressed(KeyboardKey.Escape)) {
            return Back();
        }

        return ThisScene;
    }

    public SceneIdentifier Back() {
        return SceneIdentifier.PauseMenu;
    }
}