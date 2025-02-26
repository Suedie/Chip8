using Chip8.src.frontend.scenes;
using Raylib_CSharp.Windowing;

namespace Chip8.src.frontend.buttons;

class ResolutionChangeClick : IClickable
{

    private string _label = "0 x 0";
    public string Label
    {
        get { SetLabel(); return _label; }
        set => _label = value;
    }
    private int _index = 0;
    private (int, int)[] resolutions = Array.Empty<(int, int)>();

    public ResolutionChangeClick()
    {
        Init();
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked)
    {
        if (wasClicked && !Window.IsFullscreen())
        {
            CycleResolution();
        }
        return currentScene;
    }

    //Every time it is called the index is increased and a new corresponding resolution is loaded from the array
    private void CycleResolution()
    {
        _index++;
        if (_index >= resolutions.Length)
        {
            _index = 0;
        }
        int width = resolutions[_index].Item1;
        int height = resolutions[_index].Item2;

        ResizeWindow(width, height);
        SetLabel();
    }

    //Creates a list of resolutions then sets the index to match the current resolution
    //so that it will cycle correctly
    //It then sets the label of the button to match the current resolution
    private void Init()
    {
        SetList();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Program.WindowWidth == resolutions[i].Item1 && Program.WindowHeight == resolutions[i].Item2)
            {
                _index = i;
                break;
            }
        }
        SetLabel();
    }

    private void SetLabel()
    {
        Label = Program.WindowWidth + " x " + Program.WindowHeight;
    }

    private void ResizeWindow(int width, int height)
    {
        if (width <= Window.GetMonitorWidth(Window.GetCurrentMonitor()) || height <= Window.GetMonitorHeight(Window.GetCurrentMonitor()))
        {
            Program.WindowWidth = width;
            Program.WindowHeight = height;

            Window.SetSize(Program.WindowWidth, Program.WindowHeight);
            Window.SetPosition(Window.GetMonitorWidth(Window.GetCurrentMonitor()) / 2 - width / 2, Window.GetMonitorHeight(Window.GetCurrentMonitor()) / 2 - height / 2);
        }
    }

    private void SetList()
    {
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