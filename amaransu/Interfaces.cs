using System.Runtime.CompilerServices;

namespace Otoko.Amaransu;

interface AIAudioLike: IDisposable {
    public bool Check();
    public void Update();

    public void Play();
    public void Pause();
    public void Resume();
    public void Stop();

    public void SetVolume(float interval);
    public void SetVolumeMax();
    public void SetVolumeMin();
    public void Mute();

    public void SetPaning(float interval);
    public void ResetPaning();

    public void SetPitch(float interval);
    public void ResetPitch();

    public float GetElapsedTime();
}