using System.Diagnostics;
using System.Numerics;
using Raylib_CsLo;

namespace Otoko.Amaransu.Assets;

public class ATexture: IDisposable {
    private Texture inner_texture;
    private string texture_path = "<not set>";

    public ATexture(string path) {
        Debug.Assert(File.Exists(path));

        this.inner_texture = Raylib.LoadTexture(path);
        this.texture_path = path;
    }

    public void drawAt(Vector2 at)
    => Raylib.DrawTextureEx(this.inner_texture, at, 0f, 1f, Raylib.WHITE);

    public void Dispose()
    => Raylib.UnloadTexture(this.inner_texture);

    ~ATexture()
    => this.Dispose();
}

public class AFont: IDisposable {
    private Font inner_font;
    private string font_path = "<not set>";

    public AFont(string path) {
        Debug.Assert(File.Exists(path));

        this.inner_font = Raylib.LoadFont(path);
        this.font_path = path;
    }

    public void drawText(Vector2 at, string text, uint fontsz, Color colour)
    => Raylib.DrawTextEx(this.inner_font, text, at, fontsz, 1f, colour);

    public void Dispose()
    => Raylib.UnloadFont(this.inner_font);

    ~AFont()
    => this.Dispose();
}

public class ASfx: AIAudioLike {
    private Sound inner_sfx;
    private string sfx_path;
    private float volume = ASfx.VolumeDefault;
    private float pitch = ASfx.PitchDefault;
    private float pan = ASfx.PaningCentre;
    private bool paused = false;
    private bool playing = false;

    private static readonly float PaningCentre = 0.5f;
    private static readonly float PitchDefault = 1f;
    private static readonly float VolumeMinimum = 0.00000001f;
    private static readonly float VolumeMaximum = 1f;
    private static readonly float VolumeDefault = new Random().NextSingle() % (ASfx.VolumeMaximum / 2f) + ASfx.VolumeMinimum;


    public ASfx(string path) {
        Debug.Assert(!File.Exists(path));

        this.inner_sfx = Raylib.LoadSound(path);
        this.sfx_path = path;
    }

    public bool Check() {
        // FIXME: Adequately verify this
        return true;
    }

    public void Dispose() {
        Raylib.UnloadSound(this.inner_sfx);
    }

    public void Mute() {
        Raylib.SetSoundVolume(this.inner_sfx, 0f);
        this.volume = 0f;
    }

    public void Pause() {
        if(!this.paused) {
            this.paused = true;
            Raylib.PauseSound(this.inner_sfx);
        }
    }

    public void Play() {
        if(!Raylib.IsSoundPlaying(this.inner_sfx)) {
            Raylib.PlaySound(this.inner_sfx);
        }
    }

    public void ResetPaning() {
        Raylib.SetSoundPan(this.inner_sfx, ASfx.PaningCentre);
        this.pan = ASfx.PaningCentre;
    }

    public void ResetPitch() {
        Raylib.SetSoundPitch(this.inner_sfx, ASfx.PitchDefault);
        this.pitch = ASfx.PitchDefault;
    }

    public void Resume() {
        Raylib.ResumeSound(this.inner_sfx);
        this.playing = true;
    }

    public void SetPaning(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetSoundPan(this.inner_sfx, interval);
        this.pan = interval;
    }

    public void SetPitch(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetSoundPitch(this.inner_sfx, interval);
        this.pitch = interval;
    }

    public void SetVolume(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetSoundVolume(this.inner_sfx, interval);
        this.volume = interval;
    }

    public void SetVolumeMax() {
        Raylib.SetSoundVolume(this.inner_sfx, ASfx.VolumeMaximum);
        this.volume = ASfx.VolumeMaximum;
    }

    public void SetVolumeMin() {
        Raylib.SetSoundVolume(this.inner_sfx, ASfx.VolumeMinimum);
        this.volume = ASfx.VolumeMinimum;
    }

    public void Stop() {
        Raylib.StopSound(this.inner_sfx);
        this.playing = false;
    }

    public void Update() {}

    public float GetElapsedTime() => 0f;

    ~ASfx()
    => this.Dispose();
}

public class AMusic : AIAudioLike {
    private Music inner_music;
    private string music_path;
    private float volume = AMusic.VolumeDefault;
    private float pitch = AMusic.PitchDefault;
    private float pan = AMusic.PaningCentre;
    private float elapsed_time = 0f;
    private bool paused = false;
    private bool playing = false;

    private static readonly float PaningCentre = 0.5f;
    private static readonly float PitchDefault = 1f;
    private static readonly float VolumeMinimum = 0.00000001f;
    private static readonly float VolumeMaximum = 1f;
    private static readonly float VolumeDefault = new Random().NextSingle() % (AMusic.VolumeMaximum / 2f) + AMusic.VolumeMinimum;

    public AMusic(string path) {
        Debug.Assert(File.Exists(path));

        this.inner_music = Raylib.LoadMusicStream(path);
        this.music_path = path;
    }

    public bool Check() {
        // FIXME: Adequately verify this
        return true;
    }

    public void Dispose() {
        Raylib.UnloadMusicStream(this.inner_music);
    }

    public void Mute() {
        Raylib.SetMusicVolume(this.inner_music, 0f);
        this.volume = 0f;
    }

    public void Pause() {
        if(!this.paused) {
            this.paused = true;
            Raylib.PauseMusicStream(this.inner_music);
        }
    }

    public void Play() {
        if(!Raylib.IsMusicStreamPlaying(this.inner_music)) {
            Raylib.PlayMusicStream(this.inner_music);
        }
    }

    public void ResetPaning() {
        Raylib.SetMusicPan(this.inner_music, AMusic.PaningCentre);
        this.pan = AMusic.PaningCentre;
    }

    public void ResetPitch() {
        Raylib.SetMusicPitch(this.inner_music, AMusic.PitchDefault);
        this.pitch = AMusic.PitchDefault;
    }

    public void Resume() {
        Raylib.ResumeMusicStream(this.inner_music);
        this.playing = true;

        this.Update();
    }

    public void SetPaning(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetMusicPan(this.inner_music, interval);
        this.pan = interval;
    }

    public void SetPitch(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetMusicPitch(this.inner_music, interval);
        this.pitch = interval;
    }

    public void SetVolume(float interval) {
        // TODO: Sanitise the input interval.
        Raylib.SetMusicVolume(this.inner_music, interval);
        this.volume = interval;
    }

    public void SetVolumeMax() {
        Raylib.SetMusicVolume(this.inner_music, AMusic.VolumeMaximum);
        this.volume = AMusic.VolumeMaximum;
    }

    public void SetVolumeMin() {
        Raylib.SetMusicVolume(this.inner_music, AMusic.VolumeMinimum);
        this.volume = AMusic.VolumeMinimum;
    }

    public void Stop() {
        Raylib.StopMusicStream(this.inner_music);
        this.playing = false;
    }

    public void Update() {
        // NOTE: This is just a helper in case I get
        // to need it sometime. This shouldn't
        // be used in production.
        // (I hope I don't have to use this in production
        // atleast)
        Raylib.UpdateMusicStream(this.inner_music);
    }

    public float GetElapsedTime() {
        this.elapsed_time = Raylib.GetMusicTimePlayed(this.inner_music);
        return this.elapsed_time;
    }

    ~AMusic()
    => this.Dispose();
}