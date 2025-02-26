namespace Chip8.src.emulator;

interface ICore {

    void LoadGame(string filepath);
    void Update();
    byte[,] GetScreenMatrix();
}