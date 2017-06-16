using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SoundLib
{
    private static bool IsInit = false;
    //colors
    public const int Splash = 0;
    public const int Menu = 1;
    public const int Game = 2;
    public const int Click = 3;
    public const int Pause = 4;
    public const int Error = 5;
    private static Dictionary<int, AudioClip> SoundList = new Dictionary<int, AudioClip>();
    public static bool Init()
    {
        if (!IsInit)
        {
            AudioClip SoundToSave = Resources.Load("Music/Splash") as AudioClip;
            SoundList.Add(Splash, SoundToSave);
            SoundToSave = Resources.Load("Music/Menu") as AudioClip;
            SoundList.Add(Menu, SoundToSave);
            SoundToSave = Resources.Load("Music/Game") as AudioClip;
            SoundList.Add(Game, SoundToSave);
            SoundToSave = Resources.Load("Music/Click") as AudioClip;
            SoundList.Add(Click, SoundToSave);
            SoundToSave = Resources.Load("Music/Pause") as AudioClip;
            SoundList.Add(Pause, SoundToSave);
            SoundToSave = Resources.Load("Music/Error") as AudioClip;
            SoundList.Add(Error, SoundToSave);
            IsInit = true;
        }
        return IsInit;
    }
    public static AudioClip GetSound(int Key)
    {
        AudioClip Temp;
        SoundList.TryGetValue(Key, out Temp);
        return Temp;
    }
}
