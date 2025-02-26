namespace Chip8.src.emulator;

interface ICore {
    string CurrentGamePath{get;}
    void LoadGame(string filepath);
    void Update();
    byte[,] GetScreenMatrix();
}