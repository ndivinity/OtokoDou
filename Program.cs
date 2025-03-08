namespace Otoko;

using System.Text;
using System.Threading.Tasks;
using Otoko.Amaransu;
using Otoko.Scenes;
using Raylib_CsLo;

class Dousa {
    private static SceneManager sceneMgr = new();
    private static string[] argv = [];
    private static (uint, uint) resolution = (1280, 720);

    private static void Main(string[] args) {
        argv = args;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "今の自分の人生が嫌いだｗｗｗ");
        Raylib.ChangeDirectory("../../..");

        if(argv.Length > 0) {
            for(uint i = 0; i <= argv.Length; ++i) {
                if(argv[i].Equals("/resX")) {
                    if(argv.Length < (i + 1)) {}

                    if(!uint.TryParse(argv[i++], out resolution.Item1)) {
                        Raylib.TraceLog(TraceLogLevel.LOG_FATAL, $"The argument next to \"/resX\" (= {argv[i]}) isn't a valid integer. Using default instead.");
                    }
                } else if(argv[i].Equals("/resY")) {
                    if(argv.Length < (i + 1)) {}

                    if(uint.TryParse(argv[i++], out resolution.Item2)) {
                        Raylib.TraceLog(TraceLogLevel.LOG_FATAL, $"The argument next to \"/resY\" (= {argv[i]}) isn't a valid integer. Using default instead.");
                    }
                } else {
                    Raylib.TraceLog(TraceLogLevel.LOG_WARNING, $"\"{argv[i]}\" isn't a valid argument. Ignoring.");
                }
            }
        }

        Raylib.InitWindow(800, 600, "男らしい動作");
        Raylib.SetTargetFPS(60);
        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT);
        Raylib.InitAudioDevice();

        sceneMgr.RegisterScene("RaylibSplash", new RaylibSplash(ref sceneMgr));
        sceneMgr.RegisterScene("DisclaimerSplash", new DisclaimerSplash(ref sceneMgr));
        sceneMgr.RegisterScene("DebugWarning", new DebugWarningScene(ref sceneMgr));
        sceneMgr.ChangeScene("DebugWarning");

        while(!Raylib.WindowShouldClose()) {
            // Raylib.ClearBackground(Raylib.GREEN);

            sceneMgr.Update(Raylib.GetFrameTime());

            Raylib.BeginDrawing();
            sceneMgr.Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}