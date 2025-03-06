using Raylib_CsLo;

namespace Otoko.Amaransu;

public class SceneManager {
    private Dictionary<string, Scene> scenes = new();
    private string defaultSceneName = string.Empty;
    private Scene currentScene = null;

    public string currentSceneName {
        get; private set;
    } = string.Empty;

    public bool shouldExit {
        get; private set;
    } = false;

    public void RegisterScene(string scene_name, Scene scene) {
        if(!this.scenes.ContainsKey(scene_name)) {
            this.scenes.Add(scene_name, scene);
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"Scene \"{scene_name}\" registered.");

            if(this.defaultSceneName.Equals(string.Empty))
                this.defaultSceneName = scene_name;
        } else {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Scene \"{scene_name}\" already exists!");
        }
    }

    public void SetDefaultScene(string scene_name) {
        if(this.scenes.ContainsKey(scene_name))
            this.defaultSceneName = scene_name;
        else
            Raylib.TraceLog(TraceLogLevel.LOG_FATAL, $"Cannot set \"{scene_name}\" as default - Doesn't exist?");
    }

    public void ChangeScene(string scene_name) {
        if(this.scenes.ContainsKey(scene_name)) {
            if(this.currentScene != null) {
                this.currentScene.isActive = false;
                this.currentScene.Deinitialise();
            }

            this.currentScene = this.scenes[scene_name];
            this.currentSceneName = scene_name;
            this.currentScene.isActive = true;
            this.currentScene.Initialise();

            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"Changed to scene \"{scene_name}\"!");
        } else {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Scene \"{scene_name}\" not found!!!");
        }
    }

    public void ExitGame()
    => this.shouldExit = true;

    public void Update(float deltatime) {
        if(this.currentScene != null) {
            this.currentScene.Update(deltatime);

            if(this.currentScene.IsCompleted(out string nextScene)) {
                if(nextScene != null)
                    this.ChangeScene(nextScene);
                else if(this.defaultSceneName != string.Empty && !this.defaultSceneName.Equals(this.currentSceneName))
                    this.ChangeScene(this.defaultSceneName);
                else
                    this.ExitGame();
            }
        }
    }

    public void Draw()
    => this.currentScene?.Draw();
}