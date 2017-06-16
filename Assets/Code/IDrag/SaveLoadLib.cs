using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
class SaveLoadLib
{
    public static bool Load()
    {
        string Data = IDrag.SaveLoad.Load();
        if (Data == "")
        {
#if UNITY_EDITOR
            Debug.Log("No File Detected");
#endif
            return false;
        }
        StringReader r = new StringReader(Data);
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 1)
            {
                GameGlobals.AdsOff = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting AdsOff");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.AdsOff = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting AdsOff");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.AdsOff = 0;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i == 0 || i == 100)
            {
                GameGlobals.MusicVolume = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Music Volume");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.MusicVolume = 100;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Music Volume");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.MusicVolume = 100;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i == 0 || i == 100)
            {
                GameGlobals.SoundVolume = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting Sound Volume");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.SoundVolume = 100;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting Sound Volume");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.SoundVolume = 100;
        }
        if (r.Peek() != -1)
        {
            int i = int.Parse(r.ReadLine());
            if (i >= 0 && i <= 1)
            {
                GameGlobals.ColorBlind = i;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Corrupt Save Defulting ColorBlind");
#endif
                GameGlobals.Corrupt = true;
                GameGlobals.ColorBlind = 0;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Corrupt Save Defulting ColorBlind");
#endif
            GameGlobals.Corrupt = true;
            GameGlobals.ColorBlind = 0;
        }
        if (GameGlobals.Corrupt)
        {
#if !UNITY_WEB
            SaveLoadLib.Save();
#endif
        }
        return true;
    }
    public static bool Save()
    {
       
        StringWriter w = new StringWriter();
        w.WriteLine(GameGlobals.AdsOff.ToString());
        w.WriteLine(GameGlobals.MusicVolume.ToString());
        w.WriteLine(GameGlobals.SoundVolume.ToString());
        w.WriteLine(GameGlobals.ColorBlind.ToString());
        string Data = w.ToString();
        if (!IDrag.SaveLoad.Save(Data))
        {
#if UNITY_EDITOR
            Debug.Log("Failed Saving");
#endif
            return false;
        }
        return true;
    }
}