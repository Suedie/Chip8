using Chip8.src.emulator;
using Chip8.src.frontend.scenes;
using Chip8.src.rendering;

namespace Chip8.src.frontend;

class SceneManager {

    private SceneIdentifier _previousSceneID;
    private SceneIdentifier _currentSceneID;
    private SceneIdentifier _nextSceneID;
    private IScene _currentScene;

    private readonly ICore _emuCore;
    private readonly IRenderer _emuRenderer;

    private readonly IScene _gameScene;

    public SceneManager(ICore emucore, IRenderer emuRenderer) {
        _previousSceneID = SceneIdentifier.StartMenu;
        _currentSceneID = SceneIdentifier.StartMenu;
        _emuCore = emucore;
        _emuRenderer = emuRenderer;
        _currentScene = new StartMenu();
        _gameScene = new GameplayScene(_emuCore, _emuRenderer);
    }
    
    //Switches to a scene based on the enum that identifies the scene
    //Menus are recreated every time they are switched to
    //Game is persistent
    public void SwitchToScene(SceneIdentifier sceneID) {
        switch (sceneID) {
            case SceneIdentifier.StartMenu:
            _currentScene = new StartMenu();
            break;

            case SceneIdentifier.GameSelect:
            _currentScene = new GameSelectMenu(_previousSceneID, _emuCore);
            break;

            case SceneIdentifier.OptionsMenu:
            _currentScene = new OptionsMenu(_previousSceneID);
            break;

            case SceneIdentifier.PauseMenu:
            _currentScene = new PauseMenu();
            break;

            case SceneIdentifier.GameScreen:
            _currentScene = _gameScene;
            break;
        }
    }

    //When a scene is updated it returns an enum that represents the next scene.
    //If the next scene is different from the current scene the scene is switched
    public void Run() {
        _nextSceneID = _currentScene.Update();

        if (_nextSceneID != _currentSceneID) {
            _previousSceneID = _currentSceneID;
            _currentSceneID = _nextSceneID;
            SwitchToScene(_nextSceneID);
        }
    }
}