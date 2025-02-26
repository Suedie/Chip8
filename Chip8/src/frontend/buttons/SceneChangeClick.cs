using Chip8.src.frontend.scenes;

namespace Chip8.src.frontend.buttons;

class SceneChangeClick : IClickable
{

    public string Label { get; }
    private SceneIdentifier _nextScene;

    public SceneChangeClick(string label, SceneIdentifier nextScene)
    {
        Label = label;
        _nextScene = nextScene;
    }

    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked)
    {
        if (wasClicked)
        {
            return _nextScene;
        }
        return currentScene;
    }

}