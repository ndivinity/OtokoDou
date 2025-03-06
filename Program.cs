namespace Otoko;

using System.Threading.Tasks;
using Raylib_CsLo;

class Dousa {
    private static void Main(string[] argv) {
        Raylib.InitWindow(1280, 720, "男らしい動作");
        while(!Raylib.WindowShouldClose()) {
            Raylib.ClearBackground(Raylib.GREEN);

            Raylib.BeginDrawing();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}