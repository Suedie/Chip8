namespace Chip8.src.frontend;

class GameSelectMenu : AbstractMenu {

    public SceneIdentifier PreviousScene;

    private ICore _gameCore;
    private string[] _gamePaths;

    public GameSelectMenu(SceneIdentifier previousScene, ICore gameCore) {
        ThisScene = SceneIdentifier.GameSelect;
        PreviousScene = previousScene;
        _gameCore = gameCore;
        _gamePaths = Directory.GetFiles(Program.GAME_DIRECTORY);
        Buttons = CreateButtons();
    }

    public override void AlignButtons() {
        ButtonWidth = Program.WindowWidth - (Program.WindowWidth / 10);
        for (int i = 0; i < _gamePaths.Length; i++) {
            Buttons[i].Width = ButtonWidth;
        }
        base.AlignButtons();
    }

    public override Button[] CreateButtons() {
        UpdateGameList(Program.GAME_DIRECTORY);
        Buttons = new Button[_gamePaths.Length];
        for (int i = 0; i < _gamePaths.Length; i++) {
            Buttons[i] = new Button(0,0, ButtonWidth, ButtonHeight, new GameSelectClick(_gamePaths[i], _gameCore));
        }
        AlignButtons();

        return Buttons;
    }

    public void UpdateGameList(string gameDirectory) {
        _gamePaths = Directory.GetFiles(gameDirectory);
    }

    public override SceneIdentifier Back() {
        return PreviousScene;
    }

    

}