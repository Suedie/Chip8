using Chip8.src.emulator;
using Chip8.src.frontend.scenes;

namespace Chip8.src.frontend.buttons;

class GameSelectClick : IClickable
{

    public string Label { get; }
    private string _path;
    private ICore _gameCore;

    //When clicked will load the game specified by the path variable
    public GameSelectClick(string path, ICore gameCore)
    {
        _path = path;
        Label = Path.GetFileName(_path);
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