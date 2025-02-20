using Raylib_CSharp.Windowing;

namespace Chip8.src.controller;

class FullscreenSwitchClick : IClickable {

    public string Label{get; set;}

    private bool isBorderless;

    public FullscreenSwitchClick() {
        GetCurrentWindowMode();
    }

    public SceneIdentifier onClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked) {
        if (wasClicked) {
            SwitchWindowMode();
        }
        return currentScene;
    }

    private void SwitchWindowMode() {
        if (Window.IsFullscreen()) {
            Window.ToggleFullscreen();
            Label = "Windowed";
            isBorderless = false;
        } else if (!isBorderless) {
            Window.ToggleBorderless();
            Label = "Borderless Fullscreen";
            isBorderless = true;
        } else {
            Window.ToggleFullscreen();
            Label = "Fullscreen";
            isBorderless = false;
        }
    }

    private void GetCurrentWindowMode() {
        if (Window.IsFullscreen()) {
            Label = "Fullscreen";
            isBorderless = false;
        } else if (!isBorderless) {
            Label = "Windowed";
            isBorderless = false;
        } else {
            Label = "Borderless Fullscreen";
            isBorderless = true;
        }
    }
    
}