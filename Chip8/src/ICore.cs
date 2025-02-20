namespace  Chip8.src;

interface ICore {

    void LoadGame(string filepath);
    void Update();
    byte[,] GetScreenMatrix();
}