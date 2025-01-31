namespace Chip8;

public class Display
{
    public byte[,] Pixels = new byte[64,32];

    public void ClearDisplay() {
        Array.Clear(Pixels, 0, Pixels.Length);
    }

    public string PixelsToString() {

        string result = "";
        
        // Iterate through the rows
        for (int i = 0; i < Pixels.GetLength(0); i++) // GetLength(0) gives the number of rows
        {
            // Iterate through the columns
            for (int j = 0; j < Pixels.GetLength(1); j++) // GetLength(1) gives the number of columns
            {
                result += Pixels[i, j] + " "; // Append each element followed by a space
            }
            result = result.TrimEnd() + "\n"; // Remove the trailing space and add a newline after each row
        }
        
        return result.TrimEnd(); // Remove the last newline
    }


}