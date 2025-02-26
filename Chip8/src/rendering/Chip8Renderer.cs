using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;

namespace Chip8.src.rendering;

class Chip8Renderer : IRenderer {
    private Chip8Core _chip8Core;

    public int WindowWidth = Program.WindowWidth;
    public int WindowHeight = Program.WindowHeight;

    public Chip8Renderer(Chip8Core chip8Core) {
        _chip8Core = chip8Core;
    }

    public void Update() {
        DrawMatrix(_chip8Core.GetScreenMatrix());
    }

    private void DrawMatrix(byte[,] pixels) {
        int scale = Program.WindowWidth / 64;

        //If the resolution is not a multiple of the Chip-8 resolution this will center the game in the screen
        int xOffset = (Program.WindowWidth - (64 * scale)) / 2;
        int yOffset = (Program.WindowHeight - (32 * scale)) / 2;

        for (int pixelRow = 0; pixelRow < pixels.GetLength(0); pixelRow++) {
            for (int pixelColumn = 0; pixelColumn < pixels.GetLength(1); pixelColumn++) {
                if(pixels[pixelRow, pixelColumn] == 1) {
                    Graphics.DrawRectangle((pixelRow*scale) + xOffset, (pixelColumn*scale) + yOffset, scale, scale, Color.RayWhite);
                }
            }
        }
    }
}