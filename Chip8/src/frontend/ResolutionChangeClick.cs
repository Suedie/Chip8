using Raylib_CSharp.Windowing;

namespace Chip8.src.frontend;

class ResolutionChangeClick : IClickable {

    private string _label;
    public string Label {
        get {SetLabel(); return _label;} set => _label = value;}
    private int _index = 0;
    private (int, int)[] resolutions;

    public ResolutionChangeClick() {
        init();
    }

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked && !Window.IsFullscreen()) {
            cycleResolution();
        }
        return currentScene;
    }

    private void cycleResolution() {
        if (_index >= resolutions.Length) {
            _index = 0;
        }
        int width = resolutions[_index].Item1;
        int height = resolutions[_index].Item2;
        _index++;

        ResizeWindow(width, height);
        SetLabel();
    }

    private void init() {
        SetList();
        for (int i = 0; i < resolutions.Length; i++) {
            if (Program.WindowWidth == resolutions[i].Item1 && Program.WindowHeight == resolutions[i].Item2) {
                _index = i;
                break;
            }
        }
        SetLabel();
    }

    private void SetLabel() {
        Label = Program.WindowWidth + " x " + Program.WindowHeight;
    }

    private void ResizeWindow(int width, int height) {
        if (width <= Window.GetMonitorWidth(Window.GetCurrentMonitor()) || height <= Window.GetMonitorHeight(Window.GetCurrentMonitor())) {
            Program.WindowWidth = width;
            Program.WindowHeight = height;

            Window.SetSize(Program.WindowWidth, Program.WindowHeight);
            Window.SetPosition((Window.GetMonitorWidth(Window.GetCurrentMonitor()) / 2) - (width / 2), (Window.GetMonitorHeight(Window.GetCurrentMonitor()) / 2) - (height / 2));
        }
    }

    private void SetList() {
        resolutions = new (int, int)[]
        {
            (640, 480),
            (800, 600),
            (1024, 768),
            (1280, 720),
            (1366, 768),
            (1600, 900),
            (1920, 1080),
            (1920, 1200),
            (2560, 1440)
        };
    }
    
}