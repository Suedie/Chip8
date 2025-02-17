namespace  Chip8.src;

interface IFacade {

    void LoadGame(string filepath);
    void Update();
    byte[,] GetScreenMatrix();
}