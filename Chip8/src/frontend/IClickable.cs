namespace Chip8.src.frontend;

interface IClickable {
    SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene, bool wasClicked);

    string Label{get;}
}