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