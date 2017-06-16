using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
    Material Background;
    List<UI.Image> Buttons;
    AudioSource a;
    UI.SqrButton Selected;
    // Use this for initialization
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume * 0.01f;
        a.loop = false;
        a.Pause();
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        float Width = 0.16f;
        float Height = 0.08f;
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Buttons = new List<UI.Image>();
        UI.SqrButton Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.125f, Screen.height * 0.915f, (int)(Screen.width * 0.2f), (int)(Screen.height * 0.15f), "Menu");
        Temp.SetTex(SpriteLib.Back);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Selected = new UI.SqrButton();
        Selected.Init(Screen.width * 0.5f, Screen.height * 0.865f, (int)(Screen.width * 0.4f), (int)(Screen.height * 0.25f), "Selected");
        Selected.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Selected);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.15f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "0");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.325f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "1");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp); Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "2");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.675f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "3");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.85f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "4");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.15f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "5");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.325f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "6");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "7");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.675f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "8");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.85f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "9");
        Temp.SetTex(SpriteLib.Ball0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Selected.SetTex(SpriteLib.Ball0);

    }

    // Update is called once per frame
    void Update()
    {
        foreach (UI.Image i in Buttons)
        {
            if (i.Update())
            {
                a.clip = i.GetClip();
                a.Play();
                switch (i.GetName())
                {
                    case "Menu":
#if !UNITY_WEB
                        SaveLoadLib.Save();
#endif
                        GameInfo.GameType = GameInfo.Menu;
                        SceneManager.LoadScene(1);
                        break;
                    case "0":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "1":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "2":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "3":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "4":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "5":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "6":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "7":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "8":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                    case "9":
                        Selected.SetTex(SpriteLib.Ball0);
                        break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if !UNITY_WEB
            SaveLoadLib.Save();
#endif
            GameInfo.GameType = GameInfo.Menu;
            SceneManager.LoadScene(1);
        }
    }
    void OnPostRender()
    {
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
        foreach (UI.Image i in Buttons)
        {
            i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
        }
    }
}
