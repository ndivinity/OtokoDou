namespace Otoko.Amaransu;

public abstract class Scene {
    public bool isActive { 
        get; set;
    } = false;

    protected bool isComplete = false;
    protected string nextSceneName = string.Empty;

    public virtual void Initialise() {}
    public abstract void Update(float deltatime);
    public abstract void Draw();
    public virtual void Deinitialise() {}

    public void Complete(string next_scene_name = null) {
        this.isComplete = true;
        this.nextSceneName = next_scene_name;
    }

    public bool IsCompleted(out string next_scene) {
        next_scene = this.nextSceneName;

        return this.isComplete;
    }
}