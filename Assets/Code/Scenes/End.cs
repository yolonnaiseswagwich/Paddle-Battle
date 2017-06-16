using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class End : MonoBehaviour
{
    bool Credits;
    UI.SqrButton CredOverlay;
    UI.SqrButton aWebsite;
    Material Background;
    List<UI.Image> Buttons;
    AudioSource a;
    protected bool InApp;
    void OnApplicationPause()
    {
        InApp = false;
    }
    // Use this for initialization
    void Start ()
    {
        InApp = true;
        a = GetComponent<AudioSource>();
        GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
        GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
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
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp;
        UI.Image Temp3;
        Temp3 = new UI.Image();
        Temp3.Init(Screen.width * 0.5f, Screen.height * 0.9425f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.25f), "PB");
        Temp3.SetTex(SpriteLib.SSE);
        Temp3.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp3);
        Temp3 = new UI.Image();
        Temp3.Init(Screen.width * 0.5f, Screen.height * 0.725f, (int)(Screen.width * 0.8f), (int)(Screen.height * 0.3f), "Result");
        if (GameInfo.GameType == GameInfo.Single || GameInfo.GameType == GameInfo.Multi) {
            if (Score.m_iScore2 == Score.m_iScore1)
            {
                Temp3.SetTex(SpriteLib.Draw);
            }
            else if (Score.m_iScore2 < Score.m_iScore1) {
                Temp3.SetTex(SpriteLib.Win);
            } else {
                Temp3.SetTex(SpriteLib.Lose);
            }
        } else
        {
            if (Score.m_iScore2 == Score.m_iScore1)
            {
                Temp3.SetTex(SpriteLib.Draw);
            }
            else if (Score.m_iScore2 < Score.m_iScore1)
            {
                Temp3.SetTex(SpriteLib.P1W);
            }
            else
            {
                Temp3.SetTex(SpriteLib.P2W);
            }
        }
        Temp3.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp3);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.375f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.4f), "Play");
        Temp.SetTex(SpriteLib.Play);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.12f, Screen.height * 0.935f, (int)(Screen.width * 0.18f), (int)(Screen.height * 0.08f), "Home");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.88f, Screen.height * 0.935f, (int)(Screen.width * 0.18f), (int)(Screen.height * 0.08f), "Credits");
        Temp.SetTex(SpriteLib.Credits);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "NA");
        Temp.SetTex(SpriteLib.NA);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.3f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Achievments");
        Temp.SetTex(SpriteLib.Achievments);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Settings");
        Temp.SetTex(SpriteLib.Settings);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.7f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Rate");
        Temp.SetTex(SpriteLib.Rate);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Like");
        Temp.SetTex(SpriteLib.Like);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        CredOverlay = new UI.SqrButton();
        CredOverlay.Init(Screen.width * 0.5f, Screen.height * 0.5f, (int)(Screen.width), (int)(Screen.height), "CredOverlay");
        CredOverlay.SetTex(SpriteLib.CredOverlay);
        CredOverlay.SetShader(ShaderLib.Overlay);
        aWebsite = new UI.SqrButton();
        aWebsite.Init(Screen.width * 0.5f, Screen.height * 0.54f, Screen.width, (int)(Screen.height * 0.1f), "Website");
        aWebsite.SetTex(SpriteLib.Website);
        aWebsite.SetShader(ShaderLib.OverlayNBG);
        Credits = false;
    }
    IEnumerator Like()
    {
        Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/like.html");
        yield return new WaitForSeconds(1);
        if (!InApp)
        {
            InApp = true;
        }
        else
        {
            Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/like.html");
        }
    }
    IEnumerator Rate()
    {
        Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/rate.html");
        yield return new WaitForSeconds(1);
        if (!InApp)
        {
            InApp = true;
        }
        else
        {
            Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/rate.html");
        }
    }
    // Update is called once per frame
    void Update () 
    {
        if (!Credits)
        {
            foreach (UI.Image i in Buttons) {
                if (i.Update())
                {
                    a.clip = i.GetClip();
                    a.Play();
                    switch (i.GetName()) {
                        case "Play":
                            switch (GameInfo.GameType) {
                                case GameInfo.Single:
                                    SceneManager.LoadScene(7);
                                    break;
                                case GameInfo.Local:
                                    SceneManager.LoadScene(8);
                                    break;
                                case GameInfo.Multi:
                                    //  GameInfo.GameType = GameInfo.Multi;
                                    //  SceneManager.LoadScene(9);
                                    break;
                            }
                            break;
                        case "Home":
                            GameInfo.GameType = GameInfo.Menu;
                            SceneManager.LoadScene(1);
                            break;
                        case "Achievments":
                            break;
                        case "NA":
                            // GameInfo.GameType = GameInfo.Menu;
                            //SceneManager.LoadScene(4);
                            break;
                        case "Settings":
                            GameInfo.GameType = GameInfo.Menu;
                            SceneManager.LoadScene(2);
                            break;
                        case "Rate":
                            StartCoroutine(Rate());
                            break;
                        case "Like":
                            StartCoroutine(Rate());
                            break;
                        case "Credits":
                            Credits = true;
                            break;
                    }
                }
            }
        }
        else
        {
            if (aWebsite.Update())
            {
                a.clip = aWebsite.GetClip();
                Application.OpenURL("https://www.implodingdragons.com.html");
            }
            else if (CredOverlay.Update())
            {
                a.clip = CredOverlay.GetClip();
                a.Play();
                Credits = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        //    Score.m_iColor = Score.m_iCount = Score.m_iScore = 0;
            SceneManager.LoadScene(1);
        }
	}
    void OnPostRender()
    {
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
        Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.37f), Mathf.Cos(Time.timeSinceLevelLoad * 0.71f), Mathf.Sin(Time.timeSinceLevelLoad * 1.11f), Mathf.Cos(Time.timeSinceLevelLoad * 0.63f));
        Color ColorVar = new Color(0.2f + (Mathf.Abs(Variables3.x) * 0.8f), 0.2f + (Mathf.Abs(Variables3.y) * 0.8f), 0.2f + (Mathf.Abs(Variables3.z) * 0.8f), 2.0f);
        foreach (UI.Image i in Buttons)
        {
            i.Draw(ColorVar);
        }
        if (Credits)
        {
            CredOverlay.Draw();
            aWebsite.Draw();
        }
    }
}
