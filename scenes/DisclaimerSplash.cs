using System.Diagnostics;
using System.Numerics;

using Otoko.Amaransu;
using Otoko.Amaransu.Assets;
using Raylib_CsLo;

namespace Otoko.Scenes;

public class DisclaimerSplash: Scene {
    private AFont font;
    private uint framecount = 0;
    private byte stage = 1;
    private SceneManager sceneMgr_instance;

    private readonly uint stage1time = (uint)(Raylib.GetFPS() * 2u);
    private readonly uint stage2time = (uint)(Raylib.GetFPS() * 6u);
    private readonly uint stage3time = (uint)(Raylib.GetFPS() * 10u);
    private static readonly Vector2 draw_start_coord = new(
        Raylib.GetRenderWidth() / 2 - 100f,
        Raylib.GetRenderHeight() / 2 - 200f
    );

    private (string, Vector2)[] dialogues = [
        (
            "This game is not officially rated by",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 15f)
        ),
        (
            "the Pan European Game Information (PEGI) nor",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 30f)
        ),
        (
            "by the Mexican General Directorate of Radio, Television and Cinematography",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 45f)
        ),
        (
            "(Dirección general de Radio, Televisión y Cinematografía)",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 60f)
        ),
        (
            "yet, but this game is expected to follow the PEGI16 and B15 ratings",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 75f)
        ),
        (
            "respectively, since this covers topics such as mental issues,",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 90f)
        ),
        (
            "historical political radical movements, sexism, low",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 105f)
        ),
        (
            "to none tolerance to minorities, vulgar language,",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 120f)
        ),
        (
            "mature humour and complex social issues that mightn't be",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 135f)
        ),
        (
            "correctly understood by people younger than 16 years old,",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 150f)
        ),
        (
            "or it's exposure might be disruptive to the normal development",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 165f)
        ),
        (
            "of the morality of mentioned peoples.",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 180f)
        ),

        // Second part
        (string.Empty, new()),
        (
            "Therefore, continuing the consuption of this material is",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 15f)
        ),
        (
            "responsability of the consumer and/or it's parents or",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 30f)
        ),
        (
            "tutors in charge.",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 45f)
        ),
        (
            "$skipline",
            new()
        ),
        (
            "Viewer's discretion is advised.",
            new(DisclaimerSplash.draw_start_coord.X, DisclaimerSplash.draw_start_coord.Y + 80f)
        )
    ];

    public DisclaimerSplash(ref SceneManager scenemgrptr) {
        if(scenemgrptr == null)
            throw new NullReferenceException();
        
        this.sceneMgr_instance = scenemgrptr;
    }

    override public void Initialise() {
        // TODO: Change using Comic Sans to another better font.
        this.font = new("C:\\Windows\\Fonts\\comic.ttf");
    }

    override public void Update(float deltatime) {
        Raylib.ClearBackground(Raylib.RAYWHITE);

        if(this.framecount <= this.stage1time) {
            this.stage = 1;
        } else if(this.framecount > this.stage1time && this.framecount <= this.stage2time) {
            this.stage = 3;
        } else {
            this.stage = 5;
        }

        if(this.stage == 5) {
            this.sceneMgr_instance.ChangeScene("RaylibSplash");
        }

        ++this.framecount;
    }

    override public void Draw() {
        switch(this.stage) {
            case 1: {
                foreach((string, Vector2) dialogue in this.dialogues) {
                    this.font.drawText(dialogue.Item2, dialogue.Item1, 15, Raylib.BLACK);
                    Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, $"Drawing string \"{dialogue.Item1}\" at X {dialogue.Item2.X} Y {dialogue.Item2.Y}");
                    if(dialogue.Item1.Equals(string.Empty)) this.stage = 2;
                }
            } break;

            case 2:
            case 4: {
                return;
            };

            case 3: {} break;

            case 5:
            default: {
                return;
            };
        }
    }

    override public void Deinitialise() {
        this.font.Dispose();
    }
}