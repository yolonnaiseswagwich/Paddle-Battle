using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Modes : MonoBehaviour {
    Material Background;
    List<UI.Image> Buttons;
    AudioSource a;
    // Use this for initialization
    void Start () {
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
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        UI.CircleButton Temp;
        UI.SqrButton Temp2;
        Buttons = new List<UI.Image>();
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.125f, Screen.height * 0.915f, (int)(Screen.width * 0.2f), (int)(Screen.height * 0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp2 = new UI.SqrButton();
        Temp2.Init(Screen.width * 0.5f, Screen.height * 0.665f, (int)(Screen.width * 0.55f), (int)(Screen.height * 0.2f), "Single");
        Temp2.SetTex(SpriteLib.SinglePlayer);
        Temp2.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp2);
        Temp2 = new UI.SqrButton();
        Temp2.Init(Screen.width * 0.5f, Screen.height * 0.315f, (int)(Screen.width * 0.55f), (int)(Screen.height * 0.2f), "Local");
        Temp2.SetTex(SpriteLib.LocalPlayer);
        Temp2.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp2);
        //Temp2 = new UI.SqrButton();
        //Temp2.Init(Screen.width * 0.5f, Screen.height * 0.215f, (int)(Screen.width * 0.55f), (int)(Screen.height * 0.2f), "Multi");
        //Temp2.SetTex(SpriteLib.MultiPlayer);
        //Temp2.SetShader(ShaderLib.DefaultColorTexGray);
        //Buttons.Add(Temp2);
        //Temp3 = new UI.Image();
        //Temp2.Init(Screen.width * 0.5f, Screen.height * 0.915f, (int)(Screen.width * 0.5f), (int)(Screen.height * 0.15f), "Info");
        //Temp2.SetTex(SpriteLib.SelectM);
        //Temp2.SetShader(ShaderLib.DefaultColorTex);
        //Buttons.Add(Temp3);
    }
	
	// Update is called once per frame
	void Update () {

        foreach (UI.Image i in Buttons)
        {
            if (i.Update())
            {
                a.clip = i.GetClip();
                a.Play();
                switch (i.GetName())
                {
                    case "Single":
                    GameInfo.GameType = GameInfo.Single;
                    SceneManager.LoadScene(7);
                    break;
                    case "Local":
                    GameInfo.GameType = GameInfo.Local;
                    SceneManager.LoadScene(8);
                    break;
                    case "Multi":
                    //  GameInfo.GameType = GameInfo.Multi;
                    //  SceneManager.LoadScene(9);
                    break;
                    case "Menu":
                    GameInfo.GameType = GameInfo.Menu;
                    SceneManager.LoadScene(1);
                    break;
                }
            }
        }
}
    void OnPostRender() {
        GL.PushMatrix();
        Background.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
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
        foreach (UI.Image i in Buttons) {
            if (i.GetName() == "Multi") {
                i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
            } else
            {
                Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.37f), Mathf.Cos(Time.timeSinceLevelLoad * 0.71f), Mathf.Sin(Time.timeSinceLevelLoad * 1.11f), Mathf.Cos(Time.timeSinceLevelLoad * 0.63f));
                Color ColorVar = new Color(0.2f + (Mathf.Abs(Variables3.x) * 0.8f), 0.2f + (Mathf.Abs(Variables3.y) * 0.8f), 0.2f + (Mathf.Abs(Variables3.z) * 0.8f), 2.0f);
                i.Draw(ColorVar);
            }
        }
    }
}
