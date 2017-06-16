using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SpriteLib
{
    private static bool IsInit = false;
    //colors
    public const int Default = 0;
    public const int Splash = 1;
    public const int SSE = 2;
    public const int SinglePlayer = 3;
    public const int MultiPlayer = 4;
    public const int Back = 5;
    public const int Credits = 6;
    public const int Achievments = 7;
    public const int Settings = 8;
    public const int Store = 9;
    public const int HTP = 10;
    public const int CredOverlay = 11;
    public const int HTPOverlay = 12;
    public const int CorruptOverlay = 13;
    public const int Pause = 14;
    public const int Reset = 15;
    public const int Settingsinfo = 16;
    public const int Rate = 17;
    public const int Like = 18;
    public const int NA = 19;
    public const int CircleBase = 20;
    public const int One = 21;
    public const int Two = 22;
    public const int Three = 23;
    public const int LocalPlayer = 24;
    public const int Website = 25;
    public const int Home = 26;
    public const int Play = 27;
    public const int MusicOff = 28;
    public const int MusicOn = 29;
    public const int SoundOff = 30;
    public const int SoundOn = 31;
    public const int ColorBlindOff = 32;
    public const int ColorBlindOn = 33;
    public const int Win = 34;
    public const int Lose = 35;
    public const int P1W = 36;
    public const int P2W = 37;
    public const int Draw = 38;



    public const int Ball0 = 100;

    public const int Block0 = 200;

    public const int Paddle0 = 300;
    public const int Paddle1 = 301;

    public const int Blend0 = 501;
    public const int Blend1 = 502;
    public const int Blend2 = 503;
    public const int Blend3 = 504;
    public const int Blend4 = 505;
    public const int Blend5 = 506;
    public const int Blend6 = 507;

    public const int MaxTexDots = 2;
    private static Dictionary<int, Texture2D> SpriteList = new Dictionary<int, Texture2D>();
    public static bool Init()
    {
        if (!IsInit)
        {
            Texture2D TextureToSave = Resources.Load("Default") as Texture2D;
            SpriteList.Add(Default, TextureToSave);
            TextureToSave = Resources.Load("Splash") as Texture2D;
            SpriteList.Add(Splash, TextureToSave);
            TextureToSave = Resources.Load("SSE") as Texture2D;
            SpriteList.Add(SSE, TextureToSave);
            TextureToSave = Resources.Load("SinglePlayer") as Texture2D;
            SpriteList.Add(SinglePlayer, TextureToSave);
            TextureToSave = Resources.Load("MultiPlayer") as Texture2D;
            SpriteList.Add(MultiPlayer, TextureToSave);
            TextureToSave = Resources.Load("LocalPlayer") as Texture2D;
            SpriteList.Add(LocalPlayer, TextureToSave);
            TextureToSave = Resources.Load("Back") as Texture2D;
            SpriteList.Add(Back, TextureToSave);
            TextureToSave = Resources.Load("Credits") as Texture2D;
            SpriteList.Add(Credits, TextureToSave);
            TextureToSave = Resources.Load("Achievments") as Texture2D;
            SpriteList.Add(Achievments, TextureToSave);
            TextureToSave = Resources.Load("Settings") as Texture2D;
            SpriteList.Add(Settings, TextureToSave);
            TextureToSave = Resources.Load("Store") as Texture2D;
            SpriteList.Add(Store, TextureToSave);
            TextureToSave = Resources.Load("HTP") as Texture2D;
            SpriteList.Add(HTP, TextureToSave);
            TextureToSave = Resources.Load("CredOverlay") as Texture2D;
            SpriteList.Add(CredOverlay, TextureToSave);
            TextureToSave = Resources.Load("HTPOverlay") as Texture2D;
            SpriteList.Add(HTPOverlay, TextureToSave);
            TextureToSave = Resources.Load("Corrupt") as Texture2D;
            SpriteList.Add(CorruptOverlay, TextureToSave);
            TextureToSave = Resources.Load("Pause") as Texture2D;
            SpriteList.Add(Pause, TextureToSave);
            TextureToSave = Resources.Load("Reset") as Texture2D;
            SpriteList.Add(Reset, TextureToSave);
            TextureToSave = Resources.Load("Settingsinfo") as Texture2D;
            SpriteList.Add(Settingsinfo, TextureToSave);
            TextureToSave = Resources.Load("Rate") as Texture2D;
            SpriteList.Add(Rate, TextureToSave);
            TextureToSave = Resources.Load("Like") as Texture2D;
            SpriteList.Add(Like, TextureToSave);
            TextureToSave = Resources.Load("CircleBase") as Texture2D;
            SpriteList.Add(CircleBase, TextureToSave);
            TextureToSave = Resources.Load("NA") as Texture2D;
            SpriteList.Add(NA, TextureToSave);
            TextureToSave = Resources.Load("1") as Texture2D;
            SpriteList.Add(One, TextureToSave);
            TextureToSave = Resources.Load("2") as Texture2D;
            SpriteList.Add(Two, TextureToSave);
            TextureToSave = Resources.Load("3") as Texture2D;
            SpriteList.Add(Three, TextureToSave);
            TextureToSave = Resources.Load("Website") as Texture2D;
            SpriteList.Add(Website, TextureToSave);
            TextureToSave = Resources.Load("Home") as Texture2D;
            SpriteList.Add(Home, TextureToSave);
            TextureToSave = Resources.Load("Play") as Texture2D;
            SpriteList.Add(Play, TextureToSave);
            TextureToSave = Resources.Load("MusicOff") as Texture2D;
            SpriteList.Add(MusicOff, TextureToSave);
            TextureToSave = Resources.Load("MusicOn") as Texture2D;
            SpriteList.Add(MusicOn, TextureToSave);
            TextureToSave = Resources.Load("SoundOff") as Texture2D;
            SpriteList.Add(SoundOff, TextureToSave);
            TextureToSave = Resources.Load("SoundOn") as Texture2D;
            SpriteList.Add(SoundOn, TextureToSave);
            TextureToSave = Resources.Load("ColorBlindOff") as Texture2D;
            SpriteList.Add(ColorBlindOff, TextureToSave);
            TextureToSave = Resources.Load("ColorBlindOn") as Texture2D;
            SpriteList.Add(ColorBlindOn, TextureToSave);
            TextureToSave = Resources.Load("Win") as Texture2D;
            SpriteList.Add(Win, TextureToSave);
            TextureToSave = Resources.Load("Lose") as Texture2D;
            SpriteList.Add(Lose, TextureToSave);
            TextureToSave = Resources.Load("P1W") as Texture2D;
            SpriteList.Add(P1W, TextureToSave);
            TextureToSave = Resources.Load("P2W") as Texture2D;
            SpriteList.Add(P2W, TextureToSave);
            TextureToSave = Resources.Load("Draw") as Texture2D;
            SpriteList.Add(Draw, TextureToSave);


            TextureToSave = Resources.Load("Ball0") as Texture2D;
            SpriteList.Add(Ball0, TextureToSave);


            TextureToSave = Resources.Load("Block0") as Texture2D;
            SpriteList.Add(Block0, TextureToSave);


            TextureToSave = Resources.Load("Paddle0") as Texture2D;
            SpriteList.Add(Paddle0, TextureToSave);
            TextureToSave = Resources.Load("Paddle1") as Texture2D;
            SpriteList.Add(Paddle1, TextureToSave);


            TextureToSave = Resources.Load("Blend/0") as Texture2D;
            SpriteList.Add(Blend0, TextureToSave);
            TextureToSave = Resources.Load("Blend/1") as Texture2D;
            SpriteList.Add(Blend1, TextureToSave);
            TextureToSave = Resources.Load("Blend/2") as Texture2D;
            SpriteList.Add(Blend2, TextureToSave);
            TextureToSave = Resources.Load("Blend/3") as Texture2D;
            SpriteList.Add(Blend3, TextureToSave);
            TextureToSave = Resources.Load("Blend/4") as Texture2D;
            SpriteList.Add(Blend4, TextureToSave);
            TextureToSave = Resources.Load("Blend/5") as Texture2D;
            SpriteList.Add(Blend5, TextureToSave);
            TextureToSave = Resources.Load("Blend/6") as Texture2D;
            SpriteList.Add(Blend6, TextureToSave);
            IsInit = true;
        }
        return IsInit;
    }
    public static Texture2D GetTexture(int Key)
    {
        Texture2D Temp;
        SpriteList.TryGetValue(Key, out Temp);
        return Temp;
    }
}
