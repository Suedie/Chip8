namespace Chip8.src.controller;

class SceneManager {

    private SceneIdentifier _currentScreen;

    private IFacade _model;

    private IScene _gameScene;
    private IScene _startMenu;
    private IScene _pauseMenu;

    public void LoadGame(string filepath) {
        _model.LoadGame(filepath);
    }

    public SceneManager(IFacade model) {
        _currentScreen = SceneIdentifier.StartMenu;
        _model = model;
        _gameScene = new GameplayScene(_model);
        _startMenu = new StartMenu();
        _pauseMenu = new PauseMenu();
    }
    
    public void Run() {
        switch (_currentScreen) {
            case SceneIdentifier.StartMenu:
            SwitchToScene(_startMenu.Update());
            break;

            case SceneIdentifier.GameSelect:
            break;

            case SceneIdentifier.OptionsMenu:
            break;

            case SceneIdentifier.PauseMenu:
            SwitchToScene(_pauseMenu.Update());
            break;

            case SceneIdentifier.GameScreen:
            SwitchToScene(_gameScene.Update());
            break;
        }
    }

    public void SwitchToScene(SceneIdentifier scene) {
        _currentScreen = scene;
    }
}