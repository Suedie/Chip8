namespace Chip8.src.controller;

class SceneManager {

    private Screen _currentScreen = Screen.GameplayScreen;

    private IFacade _model;

    private GameplayScene _gameplayScene;

    public void LoadRom(string filepath) {
        _model.LoadGame(filepath);
    }

    public SceneManager(IFacade model) {
        this._model = model;
        this._gameplayScene = new GameplayScene(_model);

    }
    
    public void Run() {
        switch (_currentScreen) {
            case Screen.MainMenu:
            break;

            case Screen.OptionsMenu:
            break;

            case Screen.PauseMenu:
            break;

            case Screen.GameplayScreen:
            _gameplayScene.Update();
            break;
        }
    }

    public void SwitchToScene(Screen scene) {
        _currentScreen = scene;
    }
}