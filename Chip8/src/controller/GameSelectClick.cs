namespace Chip8.src.controller;

class GameSelectClick : IClickable {

    public string Label{get;}
    private string _path;
    private ICore _gameCore;

    public GameSelectClick(string path, ICore gameCore) {
        _path = path;
        Label = Path.GetFileName(_path);
        _gameCore = gameCore;
    }

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            _gameCore.LoadGame(_path);
            return SceneIdentifier.GameScreen;
        }
        return currentScene;
    }
    
}