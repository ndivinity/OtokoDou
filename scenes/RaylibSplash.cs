using Raylib_CsLo;
using Otoko.Amaransu;

namespace Otoko.Scenes;

public class RaylibSplash: Scene {
    private Rectangle square = new();
    private uint framecount = 0;

    override public void Initialise() {
        this.square.X = Raylib.GetRenderWidth() / 2f;
        this.square.Y = Raylib.GetRenderHeight() / 2f;
        this.square.width = 20f;
        this.square.height = 20f;
    }

    override public void Draw() {
        Raylib.DrawRectangleLinesEx(this.square, 15f, Raylib.BLACK);

        this.framecount++;
    }

    override public void Update(float deltatime) {
        Raylib.ClearBackground(Raylib.BEIGE);
    }

    override public void Deinitialise() {
        Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, $"RaylibSplash: there's nothing to deinitialise.");
    }
}