using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class Settings : MonoBehaviour {
    Material Background;
    List<UI.Image> Buttons;
    AudioSource a;
    // Use this for initialization
    void Awake() {
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width*0.5f, Screen.height*0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
    }
    void Start() {
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume*0.01f;
        a.loop = false;
        a.Pause();
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.125f, Screen.height*0.9f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.875f, Screen.height*0.9f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Reset");
        Temp.SetTex(SpriteLib.Reset);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.775f, Screen.height*0.125f, (int) (Screen.width*0.4f), (int) (Screen.height*0.225f), "MusicVolume");
        if (GameGlobals.MusicVolume > 0.0f) {
            Temp.SetTex(SpriteLib.MusicOn);
        } else
        {
            Temp.SetTex(SpriteLib.MusicOff);
        }
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.225f, Screen.height*0.125f, (int) (Screen.width*0.4f), (int) (Screen.height*0.225f), "SoundVolume");
        if (GameGlobals.SoundVolume > 0.0f)
        {
            Temp.SetTex(SpriteLib.SoundOn);
        }
        else
        {
            Temp.SetTex(SpriteLib.SoundOff);
        }
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.25f, Screen.height*0.7f, (int) (Screen.width*0.4f), (int) (Screen.height*0.225f), "ColorBlind");
        if (GameGlobals.ColorBlind > 0)
        {
            Temp.SetTex(SpriteLib.ColorBlindOn);
        }
        else
        {
            Temp.SetTex(SpriteLib.ColorBlindOff);
        }
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
    }
    // Update is called once per frame
    void Update() {
        foreach (UI.Image i in Buttons) {
            if (i.Update()) {
                a.clip = i.GetClip();
                a.Play();
                switch (i.GetName()) {
                    case "Menu":
#if !UNITY_WEB
                        SaveLoadLib.Save();
#endif
                        SceneManager.LoadScene(1);
                        break;
                    case "Reset":
                        GameGlobals.MusicVolume = 100;
                    GameGlobals.SoundVolume = 100;
                    GameGlobals.ColorBlind = 0;
                    foreach (UI.Image k in Buttons)
                    {
                        switch (k.GetName())
                        {
                            case "MusicVolume":
                            if (GameGlobals.MusicVolume > 0.0f)
                            {
                                k.SetTex(SpriteLib.MusicOn);
                            }
                            else
                            {
                                k.SetTex(SpriteLib.MusicOff);
                            }
                            break;
                            case "SoundVolume":
                            a.volume = GameGlobals.SoundVolume * 0.01f;
                            if (GameGlobals.SoundVolume > 0.0f)
                            {
                                k.SetTex(SpriteLib.SoundOn);
                            }
                            else
                            {
                                k.SetTex(SpriteLib.SoundOff);
                            }
                            break;
                            case "ColorBlind":
                            if (GameGlobals.ColorBlind > 0)
                            {
                                k.SetTex(SpriteLib.ColorBlindOn);
                            }
                            else
                            {
                                k.SetTex(SpriteLib.ColorBlindOff);
                            }
                            break;
                        }
                    }
                    break;
                    case "MusicVolume":
                        GameGlobals.MusicVolume += 100;
                        if (GameGlobals.MusicVolume > 100) {
                            GameGlobals.MusicVolume = 0;
                        }
                        if (GameGlobals.MusicVolume > 0.0f)
                        {
                            i.SetTex(SpriteLib.MusicOn);
                        }
                        else
                        {
                            i.SetTex(SpriteLib.MusicOff);
                        }
                        break;
                    case "SoundVolume":
                        GameGlobals.SoundVolume += 100;
                        if (GameGlobals.SoundVolume > 100) {
                            GameGlobals.SoundVolume = 0;
                        }
                        a.volume = GameGlobals.SoundVolume*0.01f;
                        if (GameGlobals.SoundVolume > 0.0f)
                        {
                            i.SetTex(SpriteLib.SoundOn);
                        }
                        else
                        {
                            i.SetTex(SpriteLib.SoundOff);
                        }
                        break;
                    case "ColorBlind":
                        ++GameGlobals.ColorBlind;
                        if (GameGlobals.ColorBlind > 1) {
                        GameGlobals.ColorBlind = 0;
                        }
                        if (GameGlobals.ColorBlind > 0)
                        {
                            i.SetTex(SpriteLib.ColorBlindOn);
                        }
                        else
                        {
                            i.SetTex(SpriteLib.ColorBlindOff);
                        }
                    break;
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(1);
        }
    }
    void OnPostRender() {
        GL.PushMatrix();
        Background.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        //change it so that it draws in the right positon
        GL.Color(GameGlobals.BackGround);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0.1F);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, 1, 0.1F);
        GL.TexCoord2(1, 1);
        GL.Vertex3(1, 1, 0.1F);
        GL.TexCoord2(1, 0);
        GL.Vertex3(1, 0, 0.1F);
        GL.End();
        GL.PopMatrix();
        Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.37f), Mathf.Cos(Time.timeSinceLevelLoad * 0.71f), Mathf.Sin(Time.timeSinceLevelLoad * 1.11f), Mathf.Cos(Time.timeSinceLevelLoad * 0.63f));
        Color ColorVar = new Color(0.2f + (Mathf.Abs(Variables3.x) * 0.8f), 0.2f + (Mathf.Abs(Variables3.y) * 0.8f), 0.2f + (Mathf.Abs(Variables3.z) * 0.8f), 2.0f);
        foreach (UI.Image i in Buttons) {
            i.Draw(ColorVar);
        }
    }
}
