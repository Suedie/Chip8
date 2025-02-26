using Chip8.src.emulator;
using Chip8.src.frontend.scenes;

namespace Chip8.src.frontend.buttons;

class ReloadClick : IClickable
{

    public string Label { get; }
    private readonly string _path;
    private readonly ICore _gameCore;

    //When clicked will reload the current game
    public ReloadClick(string label, ICore gameCore)
    {
        _path = gameCore.CurrentGamePath;
        Label = label;
        _gameCore = gameCore;
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked)
    {
        if (wasClicked)
        {
            _gameCore.LoadGame(_path);
            return SceneIdentifier.GameScreen;
        }
        return currentScene;
    }

}