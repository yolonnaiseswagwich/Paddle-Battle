using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class ColorLib
{
    public enum Categories
    {
        Red,
        Yellow,
        Brown,
        Green,
        Blue,
        Purple,
        WhiteGray
    }
    private static Dictionary<int, Color> ColorList = new Dictionary<int, Color>();
    private const float Div = 0.0039215686274509803921568627451f;
    private static bool IsInit = false;
    //colors
    public const int Default = 0x0;
    public struct Reds
    {
        public const int IndianRed =                1;
        public const int Crismon =                  2;
        public const int FireBrick =                3;
        public const int Red =                      4;
        public const int Tomato =                   5;
        public const int Coral =                    6;
        public const int DarkOrange =               7;
        public const int Orange =                   8;
    }
    public struct Yellows
    {
        public const int Yellow =                   9;
        public const int Khaki =                    10;
        public const int Gold =                     11;
        public const int GoldenRod =                12;
    }
    public struct Browns
    {
        public const int Peru =                     13;
        public const int Chocolate =                14;
        public const int SaddleBrown =              15;
        public const int Sienna =                   16;
        public const int Brown =                    17;
    }
    public struct Greens
    {
        public const int LimeGreen =                18;
        public const int Lime =                     19;
        public const int Chartreuse =               20;
        public const int SpringGreen =              21;
        public const int LightGreen =               22;
        public const int ForestGreen =              23;
        public const int Green =                    24;
    }
    public struct Blues
    {
        public const int Cyan =                     25;
        public const int Aquamarine =               26;
        public const int DarkTurqoise =             27;
        public const int LightSeaGreen =            28;
        public const int DarkCyan =                 29;
        public const int SkyBlue =                  30;
        public const int DeepSkyBlue =              31;
        public const int DodgerBlue =               32;
        public const int RoyalBlue =                33;
        public const int Blue =                     34;
        public const int MidnightBlue =             35;
    }
    public struct Purples
    {
        public const int Pink =                     36;
        public const int HotPink =                  37;
        public const int DeepPink =                 38;
        public const int MediumVioletRed =          39;
        public const int Plum =                     40;
        public const int Violet =                   41;
        public const int Magenta =                  42;
        public const int MediumOrchid =             43;
        public const int MediumPurple =             44;
        public const int BlueViolet =               45;
        public const int DarkOrchid =               46;
        public const int Purple =                   47;
        public const int Indigo =                   48;
    }
    public struct WhiteGrays
    {
        public const int White =                    49;
        public const int Silver =                   50;
    }
    public struct BlackGrays
    {
        public const int DarkSlateGray =            51;
        public const int Black =                    52;
    }
    public struct Neon
    {
        public const int Pink = 53;
        public const int Green = 54;
        public const int Yellow = 55;
        public const int Orange = 56;
        public const int Red = 57;
        public const int Purple = 58;
        public const int DarkBlue = 59;
        public const int LightBlue = 60;

    }
    public static bool Init()
    {
        if (!IsInit)
        {
            ColorList.Add(Default, Color32(198.0f,133.0f,133.0f));
            //Reds
            ColorList.Add(Reds.IndianRed, Color32(205, 92, 92));
            ColorList.Add(Reds.Crismon, Color32(220, 20, 60));
            ColorList.Add(Reds.FireBrick, Color32(178, 34, 34));
            ColorList.Add(Reds.Red, Color32(255, 0, 0));
            ColorList.Add(Reds.Tomato, Color32(255, 99, 71));
            ColorList.Add(Reds.Coral, Color32(255, 127, 80));
            ColorList.Add(Reds.DarkOrange, Color32(255, 140, 0));
            ColorList.Add(Reds.Orange, Color32(255, 165, 0));
            //Yellows
            ColorList.Add(Yellows.Yellow, Color32(255, 255, 0));
            ColorList.Add(Yellows.Khaki, Color32(240, 230, 140));
            ColorList.Add(Yellows.Gold, Color32(255, 215, 0));
            ColorList.Add(Yellows.GoldenRod, Color32(218, 180, 32));
            //Browns
            ColorList.Add(Browns.Peru, Color32(205, 133, 63));
            ColorList.Add(Browns.Chocolate, Color32(150, 105, 30));
            ColorList.Add(Browns.SaddleBrown, Color32(139, 69, 19));
            ColorList.Add(Browns.Sienna, Color32(160, 82, 45));
            ColorList.Add(Browns.Brown, Color32(140, 65, 42));
            //Greens
            ColorList.Add(Greens.LimeGreen, Color32(50, 205, 50));
            ColorList.Add(Greens.Lime, Color32(0, 255, 0));
            ColorList.Add(Greens.Chartreuse, Color32(127, 255, 0));
            ColorList.Add(Greens.SpringGreen, Color32(0, 255, 127));
            ColorList.Add(Greens.LightGreen, Color32(144, 238, 144));
            ColorList.Add(Greens.ForestGreen, Color32(24, 150, 48));
            ColorList.Add(Greens.Green, Color32(0, 128, 0));
            //Blues
            ColorList.Add(Blues.Cyan, Color32(0, 255, 255));
            ColorList.Add(Blues.Aquamarine, Color32(127, 255, 212));
            ColorList.Add(Blues.DarkTurqoise, Color32(0, 206, 209));
            ColorList.Add(Blues.LightSeaGreen, Color32(32, 178, 170));
            ColorList.Add(Blues.DarkCyan, Color32(0, 139, 139));
            ColorList.Add(Blues.SkyBlue, Color32(135, 206, 235));
            ColorList.Add(Blues.DeepSkyBlue, Color32(0, 191, 255));
            ColorList.Add(Blues.DodgerBlue, Color32(30, 144, 255));
            ColorList.Add(Blues.RoyalBlue, Color32(65, 105, 225));
            ColorList.Add(Blues.Blue, Color32(0, 0, 255));
            ColorList.Add(Blues.MidnightBlue, Color32(0, 0, 112));
            //Purples
            ColorList.Add(Purples.Pink, Color32(255, 192, 203));
            ColorList.Add(Purples.HotPink, Color32(255, 105, 180));
            ColorList.Add(Purples.DeepPink, Color32(255, 20, 147));
            ColorList.Add(Purples.MediumVioletRed, Color32(199, 21, 133));
            ColorList.Add(Purples.Plum, Color32(221, 160, 221));
            ColorList.Add(Purples.Violet, Color32(238, 130, 238));
            ColorList.Add(Purples.Magenta, Color32(255, 0, 255));
            ColorList.Add(Purples.MediumOrchid, Color32(186, 85, 211));
            ColorList.Add(Purples.MediumPurple, Color32(147, 112, 219));
            ColorList.Add(Purples.BlueViolet, Color32(138, 43, 226));
            ColorList.Add(Purples.DarkOrchid, Color32(153, 50, 204));
            ColorList.Add(Purples.Purple, Color32(128, 0, 128));
            ColorList.Add(Purples.Indigo, Color32(75, 0, 130));
            //WhiteGrays
            ColorList.Add(WhiteGrays.White, Color32(255, 255, 255));
            ColorList.Add(WhiteGrays.Silver, Color32(192, 192, 192));
            //BlackGrays
            ColorList.Add(BlackGrays.DarkSlateGray, Color32(47, 79, 79));
            ColorList.Add(BlackGrays.Black, Color32(0, 0, 0));
            //Neon
            ColorList.Add(Neon.Pink, Color32(0, 255, 0));
            ColorList.Add(Neon.Green, Color32(255, 0, 255));
            ColorList.Add(Neon.Yellow, Color32(0, 0, 255));
            ColorList.Add(Neon.Orange, Color32(0, 90, 255));
            ColorList.Add(Neon.Red, Color32(0, 255, 255));
            ColorList.Add(Neon.Purple, Color32(90, 255, 0));
            ColorList.Add(Neon.DarkBlue, Color32(255, 255, 0));
            ColorList.Add(Neon.LightBlue, Color32(255, 0, 0));

            IsInit = true;
        }
        return IsInit;
    }
    private static Color Color32(float r, float g, float b)
    {
        return new Color(r * Div, g * Div, b * Div);
    }
    public static Color GetColor(int Key)
    {
        Color Temp;
        ColorList.TryGetValue(Key, out Temp);
        return Temp;
    }
    public static Color GetRandomColor()
    {
        return GetColor(IDrag.Random.GetRandom(1, ColorList.Count));
    }
    public static Color GetRandomNeon()
    {
        Color Temp = GetColor(IDrag.Random.GetRandom(Neon.Pink, Neon.LightBlue));
        Temp.a = 0.8f;
        return Temp;
    }
    public static Color GetRandomTypeColor(Categories aCategory)
    { 
        switch (aCategory)
        {
            case Categories.Red:
                return GetColor(IDrag.Random.GetRandom(Reds.IndianRed, Reds.Orange));
            case Categories.Yellow:
                return GetColor(IDrag.Random.GetRandom(Yellows.Yellow, Yellows.GoldenRod));
            case Categories.Brown:
                return GetColor(IDrag.Random.GetRandom(Browns.Peru, Browns.Brown));
            case Categories.Green:
                return GetColor(IDrag.Random.GetRandom(Greens.LimeGreen, Greens.Green));
            case Categories.Blue:
                return GetColor(IDrag.Random.GetRandom(Blues.Cyan, Blues.MidnightBlue));
            case Categories.Purple:
                return GetColor(IDrag.Random.GetRandom(Purples.Pink, Purples.Indigo));
            case Categories.WhiteGray:
                return GetColor(IDrag.Random.GetRandom(WhiteGrays.White, WhiteGrays.Silver));
            default:
                return GetColor(Default);
        }
    }

}
