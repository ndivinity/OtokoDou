namespace Otoko;

using System.Threading.Tasks;
using Otoko.Amaransu;
using Otoko.Scenes;
using Raylib_CsLo;

class Dousa {
    private static SceneManager sceneMgr = new();
    private static void Main(string[] argv) {
        sceneMgr.RegisterScene("RaylibSplash", new RaylibSplash());
        sceneMgr.ChangeScene("RaylibSplash");

        Raylib.InitWindow(1280, 720, "男らしい動作");
        while(!Raylib.WindowShouldClose()) {
            // Raylib.ClearBackground(Raylib.GREEN);

            sceneMgr.Update(Raylib.GetFrameTime());

            Raylib.BeginDrawing();
            sceneMgr.Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}