using Otoko.Amaransu;
using Raylib_CsLo;

namespace Otoko.Scenes;

public class DebugWarningScene: Scene {
    private SceneManager sceneMgr_instance;
    private uint framecount = 0;
    private byte confirms = 0;

    private readonly static string[] dialogues = [
        "This game hasn't been released yet. Expect something incomplete and completely bug-filled.",
        "Okey, continue dismissing, don't say I didn't warn you. lol",
        "Are you sure you want to continue?"
    ];
    private string dialogue = DebugWarningScene.dialogues[0];


    public DebugWarningScene(ref SceneManager scenemgrptr) {
        if(scenemgrptr == null)
            throw new NullReferenceException();
        
        this.sceneMgr_instance = scenemgrptr;
    }

    override public void Initialise() {}

    override public void Update(float deltatime) {
        if(this.confirms == 3)
            this.sceneMgr_instance.ChangeScene("DisclaimerSplash");

        ++this.framecount;
    }

    override public void Draw() {
        int result = -1;

        if(this.confirms < 3) {
            result = RayGui.GuiMessageBox(
                new(
                    Raylib.GetRenderWidth() / 2f - 250f,
                    Raylib.GetRenderHeight() / 2f - 60f,
                    550, 100
                ),
                "#193#Game not released yet!",
                dialogue,
                "#112#Oke;#113#Exit"
            );

            if(result == 1) {
                this.dialogue = DebugWarningScene.dialogues[2];
                ++this.confirms;
            } else if(result == 2) {
                Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Pressed Exit");
            } else if(result == 0) {
                dialogue = DebugWarningScene.dialogues[1];
                ++this.confirms;
            }
        } else {
            if(RayGui.GuiMessageBox(
                new(
                    Raylib.GetRenderWidth() / 2f,
                    Raylib.GetRenderHeight() / 2f,
                    400f, 400f
                ),
                "#191#What the fuck",
                "Sending you into oblivion...",
                "#152#Just fucking do it already"
            ) >= 0) {
                this.confirms = 3;
            }
        }
    }

    override public void Deinitialise() {
        
    }
}