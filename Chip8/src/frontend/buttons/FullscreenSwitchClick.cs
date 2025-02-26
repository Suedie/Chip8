using Chip8.src.frontend.scenes;
using Raylib_CSharp.Windowing;

namespace Chip8.src.frontend.buttons;

class FullscreenSwitchClick : IClickable {

    private string _label = "Windowed";
    public string Label
    {
        get { GetCurrentWindowMode(); return _label; }
        set => _label = value;
    }

    public FullscreenSwitchClick() {
        GetCurrentWindowMode();
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            SwitchWindowMode();
        }
        return currentScene;
    }

    //When clicked will toggle between fullscreen and windowed modes
    //The window will automatically be centered when switching to windowed mode
    private void SwitchWindowMode() {
        if (Window.IsFullscreen()) {
            Window.ToggleFullscreen();
            Label = "Windowed";
            Window.SetSize(Program.WindowWidth, Program.WindowHeight);
            Window.SetPosition((Window.GetMonitorWidth(Window.GetCurrentMonitor()) / 2) - (Program.WindowWidth / 2),
             (Window.GetMonitorHeight(Window.GetCurrentMonitor()) / 2) - (Program.WindowHeight / 2));
        } else {
            Label = "Fullscreen";
            Program.WindowWidth = Window.GetMonitorWidth(Window.GetCurrentMonitor());
            Program.WindowHeight = Window.GetMonitorHeight(Window.GetCurrentMonitor());
            Window.SetSize(Window.GetMonitorWidth(Window.GetCurrentMonitor()), Window.GetMonitorHeight(Window.GetCurrentMonitor()));
            Window.ToggleFullscreen();
        }
    }

    private void GetCurrentWindowMode() {
        if (Window.IsFullscreen()) {
            Label = "Fullscreen";
        } else {
            Label = "Windowed";
        }
    }
    
}