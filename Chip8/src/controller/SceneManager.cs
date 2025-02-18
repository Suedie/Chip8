namespace Chip8.src.controller;

class SceneManager {

    private SceneIdentifier _currentScreen = SceneIdentifier.GameplayScreen;

    private IFacade _model;

    private GameplayScene _gameplayScene;

    public void LoadGame(string filepath) {
        _model.LoadGame(filepath);
    }

    public SceneManager(IFacade model) {
        this._model = model;
        this._gameplayScene = new GameplayScene(_model);

    }
    
    public void Run() {
        switch (_currentScreen) {
            case SceneIdentifier.MainMenu:
            break;

            case SceneIdentifier.OptionsMenu:
            break;

            case SceneIdentifier.PauseMenu:
            break;

            case SceneIdentifier.GameplayScreen:
            _gameplayScene.Update();
            break;
        }
    }

    public void SwitchToScene(SceneIdentifier scene) {
        _currentScreen = scene;
    }
}