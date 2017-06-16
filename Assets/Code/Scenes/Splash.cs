using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public struct Score
{
    public static int m_iScore1 = 0;
    public static int m_iScore2 = 0;
    public static float m_fGameTime = 0;
};
public struct ShaderData
{
    public Vector4 aVect4;
}
public class GameInfo
{
    public const int Splash = 0;
    public const int Menu = 1;
    public const int Single = 2;
    public const int Local = 3;
    public const int Multi = 4;
    public static int GameType;
}
public struct GameGlobals
{
    public static int Players = 4;
    public static float MusicVolume = 100;
    public static float SoundVolume = 100;
    public static bool Corrupt = false;
    public static bool Paused = false;
    public static int ColorBlind = 0;
    public static int AdsOff = 0;
    public static float GameSpeed = (float)Screen.height * 0.05f;
    public static float ShakeTime = 0;
    public static Color Red, Yellow, Brown, Green, Blue, Purple, WhiteGray, BackGround;
    public static int iRed, iYellow, iBlue, iBrown, iGreen, iWhiteGray, iPurple, iBackGround = 0;
}
public class Splash : MonoBehaviour
{
    private Material Shaders;
    private Texture2D m_tBackground;
    private Texture2D m_tBlend;
    private float Timer;
    // Use this for initialization
    void Start()
    {
#if !UNITY_ANDROID
        Screen.SetResolution(540, 960, false);
#endif
        GameInfo.GameType = GameInfo.Splash;
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Shaders = ShaderLib.GetShader(ShaderLib.Blend);
        m_tBackground = SpriteLib.GetTexture(SpriteLib.Splash);
        Timer = 0.0f;
        IDrag.Random.SetSeed(System.Environment.TickCount);
        m_tBlend = SpriteLib.GetTexture(SpriteLib.Blend0 + IDrag.Random.GetRandom(0, 6));
        Shaders.mainTexture = m_tBackground;
        Shaders.SetTexture("_BumpMap", m_tBlend);
        GameGlobals.Corrupt = false;
        GameGlobals.iRed = ColorLib.Reds.Crismon;
        GameGlobals.iYellow = ColorLib.Yellows.Yellow;
        GameGlobals.iBrown = ColorLib.Browns.SaddleBrown;
        GameGlobals.iGreen = ColorLib.Greens.Lime;
        GameGlobals.iBlue = ColorLib.Blues.DarkTurqoise;
        GameGlobals.iPurple = ColorLib.Purples.HotPink;
        GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
        GameGlobals.iBackGround = ColorLib.Blues.DeepSkyBlue;
        GameGlobals.Red = ColorLib.GetColor(GameGlobals.iRed);
        GameGlobals.Yellow = ColorLib.GetColor(GameGlobals.iYellow);
        GameGlobals.Brown = ColorLib.GetColor(GameGlobals.iBrown);
        GameGlobals.Green = ColorLib.GetColor(GameGlobals.iGreen);
        GameGlobals.Blue = ColorLib.GetColor(GameGlobals.iBlue);
        GameGlobals.Purple = ColorLib.GetColor(GameGlobals.iPurple);
        GameGlobals.WhiteGray = ColorLib.GetColor(GameGlobals.iWhiteGray);
        GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
#if !UNITY_WEB
        if (!SaveLoadLib.Load())
#endif
        {
#if !UNITY_WEB && UNITY_EDITOR
            Debug.Log("Corrupted Save");
#endif
#if UNITY_EDITOR
            Debug.Log("Creating Defaults");
#endif
            GameGlobals.MusicVolume = 100;
            GameGlobals.SoundVolume = 100;
            GameGlobals.ColorBlind = 0;
            GameGlobals.AdsOff = 0;
#if !UNITY_WEB
            SaveLoadLib.Save();
#endif
        }
    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (ShaderLib.Init() && SpriteLib.Init() && (Timer > 2.75f || Input.anyKeyDown))
        {
            GameInfo.GameType = GameInfo.Menu;
            SceneManager.LoadScene(1);//menu
        }
    }
    void OnPostRender()
    {
        if (Timer >= 0)
        {
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);
            if (Screen.width > Screen.height)
            {
                GL.TexCoord2(0, 0);
                GL.Vertex3((Screen.width - Screen.height) * 0.5f * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
                GL.TexCoord2(0, 1);
                GL.Vertex3((Screen.width - Screen.height) * 0.5f * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
                GL.TexCoord2(1, 1);
                GL.Vertex3(((Screen.width - Screen.height) * 0.5f + Screen.height) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
                GL.TexCoord2(1, 0);
                GL.Vertex3(((Screen.width - Screen.height) * 0.5f + Screen.height) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
            }
            else if (Screen.height > Screen.width)
            {
                GL.TexCoord2(0, 0);
                GL.Vertex3(0, (Screen.height - Screen.width) * 0.5f * IDrag.D2Camera.PixelSize.y, 0.1F);
                GL.TexCoord2(0, 1);
                GL.Vertex3(0, ((Screen.height - Screen.width) * 0.5f + Screen.width) * IDrag.D2Camera.PixelSize.y, 0.1F);
                GL.TexCoord2(1, 1);
                GL.Vertex3(1, ((Screen.height - Screen.width) * 0.5f + Screen.width) * IDrag.D2Camera.PixelSize.y, 0.1F);
                GL.TexCoord2(1, 0);
                GL.Vertex3(1, (Screen.height - Screen.width) * 0.5f * IDrag.D2Camera.PixelSize.y, 0.1F);
            }
            else
            {
                GL.TexCoord2(0, 0);
                GL.Vertex3(0, 0, 0.1F);
                GL.TexCoord2(0, 1);
                GL.Vertex3(0, 1, 0.1F);
                GL.TexCoord2(1, 1);
                GL.Vertex3(1, 1, 0.1F);
                GL.TexCoord2(1, 0);
                GL.Vertex3(1, 0, 0.1F);
            }
            GL.End();
            GL.PopMatrix();
        }
    }
}