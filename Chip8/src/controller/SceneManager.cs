namespace Chip8.src.controller;

class SceneManager {

    private SceneIdentifier _currentScreen = SceneIdentifier.MainMenu;

    private IFacade _model;

    private GameplayScene _gameplayScene;
    private MainMenu _mainMenu;

    public void LoadGame(string filepath) {
        _model.LoadGame(filepath);
    }

    public SceneManager(IFacade model) {
        this._model = model;
        this._gameplayScene = new GameplayScene(_model);
        this._mainMenu = new MainMenu();
    }
    
    public void Run() {
        switch (_currentScreen) {
            case SceneIdentifier.MainMenu:
            SwitchToScene(_mainMenu.Update());
            break;

            case SceneIdentifier.OptionsMenu:
            break;

            case SceneIdentifier.PauseMenu:
            break;

            case SceneIdentifier.GameplayScreen:
            SwitchToScene(_gameplayScene.Update());
            break;
        }
    }

    public void SwitchToScene(SceneIdentifier scene) {
        _currentScreen = scene;
    }
}