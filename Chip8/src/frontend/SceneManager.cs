using Chip8.src.rendering;

namespace Chip8.src.frontend;

class SceneManager {

    private SceneIdentifier _previousScene;
    private SceneIdentifier _currentScene;

    private readonly ICore _emuCore;
    private readonly IRenderer _emuRenderer;

    private readonly IScene _gameScene;
    private readonly IScene _startMenu;
    private readonly IScene _pauseMenu;
    private readonly IScene _optionsMenu;

    private readonly IScene _gameSelectMenu;

    public SceneManager(ICore emucore, IRenderer emuRenderer) {
        _previousScene = SceneIdentifier.StartMenu;
        _currentScene = SceneIdentifier.StartMenu;
        _emuCore = emucore;
        _emuRenderer = emuRenderer;
        _gameScene = new GameplayScene(_emuCore, _emuRenderer);
        _startMenu = new StartMenu();
        _pauseMenu = new PauseMenu();
        _optionsMenu = new OptionsMenu(_previousScene);
        _gameSelectMenu = new GameSelectMenu(_previousScene, _emuCore);
    }
    
    public void Run() {
        switch (_currentScene) {
            case SceneIdentifier.StartMenu:
            SwitchToScene(_startMenu.Update());
            break;

            case SceneIdentifier.GameSelect:
            SwitchToScene(_gameSelectMenu.Update());
            break;

            case SceneIdentifier.OptionsMenu:
            SwitchToScene(_optionsMenu.Update());
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
        _previousScene = _currentScene;
        _currentScene = scene;
    }
}