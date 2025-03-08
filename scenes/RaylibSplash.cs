using Raylib_CsLo;
using Otoko.Amaransu;
using Otoko.Amaransu.Assets;
using System.Diagnostics;

namespace Otoko.Scenes;

public class RaylibSplash: Scene {
    private Rectangle square = new();
    private uint framecount = 0;
    private ATexture raylib_logo;
    private SceneManager sceneMgr_instance;

    public RaylibSplash(ref SceneManager scenemgrptr) {
        if(scenemgrptr == null)
            throw new NullReferenceException();

        this.sceneMgr_instance = scenemgrptr;
    }

    override public void Initialise() {
        this.square.X = Raylib.GetRenderWidth() / 2f;
        this.square.Y = Raylib.GetRenderHeight() / 2f;
        this.square.width = 20f;
        this.square.height = 20f;

        this.raylib_logo = new("assets/raylib_256x256.png");
    }

    override public void Draw() {
        Raylib.DrawRectangleLinesEx(this.square, 15f, Raylib.BLACK);
        this.raylib_logo.drawAt(new(15, 15));

        this.framecount++;
    }

    override public void Update(float deltatime) {
        Raylib.ClearBackground(Raylib.BEIGE);
    }

    override public void Deinitialise() {
        Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, $"RaylibSplash: there's nothing to deinitialise.");
    }
}