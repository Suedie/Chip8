using Chip8.src.frontend.scenes;

namespace Chip8.src.frontend.buttons;

interface IClickable
{
    SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked);

    string Label { get; }
}