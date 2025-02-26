using Chip8.src.frontend.scenes;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Chip8.src.frontend.buttons;

class Button {

    public int PosX{get; set;}
    public int PosY{get; set;}
    public int Width;
    public int Height;
    private string _label;
    private readonly IClickable _clickBehaviour;

    //Each button must be created with a behaviour object
    //Each button is identical but what it does is defined by
    //the behaviour object that it holds, allowing for many custom buttons
    public Button (int x, int y, int width, int height, IClickable clickBehaviour) {
        PosX = x;
        PosY = y;
        Width = width;
        Height = height;
        _clickBehaviour = clickBehaviour;
        _label = _clickBehaviour.Label;
    }

    //Each button draws itself, and will change colour when hovered over
    //Also each button draws its own text based on the label variable
    public void DrawButton() {
        Color color = Color.RayWhite;

        if (MouseOverlaps(Input.GetMouseX(), Input.GetMouseY())) {
            color = Color.Gray;
        }
        Graphics.DrawRectangle(PosX, PosY, Width, Height, color);
        _label = _clickBehaviour.Label;
        Graphics.DrawText(_label,PosX + (Width / 10), PosY + (Height / 2), Height / 3, Color.Black);
    }

    private bool MouseOverlaps(int mouseX, int mouseY) {
        if (mouseX < PosX || mouseX > PosX + Width || mouseY < PosY || mouseY > PosY + Height) {
            return false;
        } else {
            return true;
        }
    }

    //When clicked will execute logic that is contained in the Clickbehaviour object
    public SceneIdentifier OnClick(int mouseX, int mouseY, SceneIdentifier currentScene) {
        return _clickBehaviour.OnClick(mouseX, mouseY, currentScene, MouseOverlaps(mouseX, mouseY));
    }
}