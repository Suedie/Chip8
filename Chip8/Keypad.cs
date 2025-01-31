using Raylib_CSharp.Interact;

namespace Chip8;

public class Keypad {

    private Dictionary<int, byte> _keyToHex = new Dictionary<int, byte> {
        {(int) KeyboardKey.One, 0x1},
        {(int) KeyboardKey.Two, 0x2},
        {(int) KeyboardKey.Three, 0x3},
        {(int) KeyboardKey.Four, 0xC},
        {(int) KeyboardKey.Q, 0x4},
        {(int) KeyboardKey.W, 0x5},
        {(int) KeyboardKey.E, 0x6},
        {(int) KeyboardKey.R, 0xD},
        {(int) KeyboardKey.A, 0x7},
        {(int) KeyboardKey.S, 0x8},
        {(int) KeyboardKey.D, 0x9},
        {(int) KeyboardKey.F, 0xE},
        {(int) KeyboardKey.Z, 0xA},
        {(int) KeyboardKey.X, 0x0},
        {(int) KeyboardKey.C, 0xB},
        {(int) KeyboardKey.V, 0xF}
    };
    
    private Dictionary<byte, int> _hexToKey = new Dictionary<byte, int> {
        {0x1, (int) KeyboardKey.One},
        {0x2, (int) KeyboardKey.Two},
        {0x3, (int) KeyboardKey.Three},
        {0xC, (int) KeyboardKey.Four},
        {0x4, (int) KeyboardKey.Q},
        {0x5, (int) KeyboardKey.W},
        {0x6, (int) KeyboardKey.E},
        {0xD, (int) KeyboardKey.R},
        {0x7, (int) KeyboardKey.A},
        {0x8, (int) KeyboardKey.S},
        {0x9, (int) KeyboardKey.D},
        {0xE, (int) KeyboardKey.F},
        {0xA, (int) KeyboardKey.Z},
        {0x0, (int) KeyboardKey.X},
        {0xB, (int) KeyboardKey.C},
        {0xF, (int) KeyboardKey.V}
    };

    public int HexToKey(byte hex) {
        return _hexToKey[hex];
    }

    public byte KeyToHex(int key) {
        return _keyToHex[key];
    }

    public bool ContainsHex(byte hex) {
        return _hexToKey.ContainsKey(hex);
    }

    public bool ContainsKey(int key) {
        return _keyToHex.ContainsKey(key);
    }
    

}