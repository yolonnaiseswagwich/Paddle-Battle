using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BaseGame : MonoBehaviour
{
    protected Texture2D One;
    protected Texture2D Two;
    protected Texture2D Three;
    protected Material Background;
    protected Material Count;
    protected UI.SqrButton PauseButton;
    protected float AudioTimer;
    protected float SpawnTimer;
    protected float timer;
    protected float SpawnTime;
    public Block[] Blocks;
    public List<Ball> Balls;
    ///TEMPVAR
	// Use this for initialization
    protected virtual void Start()
    {
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Score.m_iScore1 = 0;
        Score.m_iScore2 = 0;
        Score.m_fGameTime = 0;
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Count = ShaderLib.GetShader(ShaderLib.TexColor);
        One = SpriteLib.GetTexture(SpriteLib.One);
        Two = SpriteLib.GetTexture(SpriteLib.Two);
        Three = SpriteLib.GetTexture(SpriteLib.Three);
        AudioTimer = 0;
        PauseButton = new UI.SqrButton();
        PauseButton.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "Pause");
        PauseButton.SetTex(SpriteLib.Pause);
        PauseButton.SetShader(ShaderLib.Overlay);
        timer = 0;
        SpawnTimer = SpawnTime = 5.0f;
    }
    protected virtual void Update()
    {
        if (GameGlobals.Paused)
        {
            if (PauseButton.Update())
            {
                foreach (AudioSource a in GetComponents<AudioSource>())
                {
                        a.UnPause();
                }
                GameGlobals.Paused = false;
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 4.5f) {
                Score.m_fGameTime += Time.deltaTime;
            }
            DoAudio();
        }
        if (/*Score.m_fGameTime > 300.0f || */Score.m_iScore2 >= 6 || Score.m_iScore1 >= 6 || Balls.Count == 0) {
            SceneManager.LoadScene(6);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameGlobals.Paused)
            {
                GameGlobals.Paused = false;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(1);
            }
            else
            {
                foreach (AudioSource a in GetComponents<AudioSource>())
                {
                    a.Pause();
                }
                GameGlobals.Paused = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    protected virtual void OnPostRender()
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
        if (timer > 4.5f)
        {
            DrawGame();
        }
        else
        {
            DrawGame(new Color(1, 1, 1, 0.4f));
            Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.37f), Mathf.Cos(Time.timeSinceLevelLoad * 0.71f), Mathf.Sin(Time.timeSinceLevelLoad * 1.11f), Mathf.Cos(Time.timeSinceLevelLoad * 0.63f));
            Color ColorVar = new Color(0.2f + (Mathf.Abs(Variables3.x) * 0.8f), 0.2f + (Mathf.Abs(Variables3.y) * 0.8f), 0.2f + (Mathf.Abs(Variables3.z) * 0.8f), 2.0f);
            int Temp = 3 - (int)(timer * 0.6667f);
            switch (Temp)
            {
                case 1:
                    Count.mainTexture = One;
                    ColorVar.a = 3 - timer * 0.6667f;
                    GL.PushMatrix();
                    Count.SetPass(0);
                    GL.LoadOrtho();
                    GL.Begin(GL.QUADS);
                    GL.Color(ColorVar);
                    //change it so that it draws in the right positon
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(0, 0, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(0, 1);
                    GL.Vertex3(0, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 1);
                    GL.Vertex3(1, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 0);
                    GL.Vertex3(1, 0, 0.1F);
                    GL.End();
                    GL.PopMatrix();
                    Color TempColor = new Color(1, 1, 1, 1 - (3.5f - timer * 0.6667f));
                    DrawGame(TempColor);
                    break;
                case 2:
                    Count.mainTexture = Two;
                    ColorVar.a = 2 - timer * 0.6667f;
                    GL.PushMatrix();
                    Count.SetPass(0);
                    GL.LoadOrtho();
                    GL.Begin(GL.QUADS);
                    GL.Color(ColorVar);
                    //change it so that it draws in the right positon
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(0, 0, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(0, 1);
                    GL.Vertex3(0, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 1);
                    GL.Vertex3(1, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 0);
                    GL.Vertex3(1, 0, 0.1F);
                    GL.End();
                    GL.PopMatrix(); ;
                    Count.mainTexture = One;
                    ColorVar.a = 1 - (2.5f - timer * 0.6667f);
                    GL.PushMatrix();
                    Count.SetPass(0);
                    GL.LoadOrtho();
                    GL.Begin(GL.QUADS);
                    GL.Color(ColorVar);
                    //change it so that it draws in the right positon
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(0, 0, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(0, 1);
                    GL.Vertex3(0, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 1);
                    GL.Vertex3(1, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 0);
                    GL.Vertex3(1, 0, 0.1F);
                    GL.End();
                    GL.PopMatrix();
                    break;
                case 3:
                    Count.mainTexture = Three;
                    ColorVar.a = 1 - timer * 0.6667f;
                    GL.PushMatrix();
                    Count.SetPass(0);
                    GL.LoadOrtho();
                    GL.Begin(GL.QUADS);
                    GL.Color(ColorVar);
                    //change it so that it draws in the right positon
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(0, 0, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(0, 1);
                    GL.Vertex3(0, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 1);
                    GL.Vertex3(1, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 0);
                    GL.Vertex3(1, 0, 0.1F);
                    GL.End();
                    GL.PopMatrix();
                    Count.mainTexture = Two;
                    ColorVar.a = 1 - (1.5f - timer * 0.6667f);
                    GL.PushMatrix();
                    Count.SetPass(0);
                    GL.LoadOrtho();
                    GL.Begin(GL.QUADS);
                    GL.Color(ColorVar);
                    //change it so that it draws in the right positon
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(0, 0, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(0, 1);
                    GL.Vertex3(0, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 1);
                    GL.Vertex3(1, 1, 0.1F);
                    GL.Color(ColorVar);
                    GL.TexCoord2(1, 0);
                    GL.Vertex3(1, 0, 0.1F);
                    GL.End();
                    GL.PopMatrix();
                    break;
            }
        }
        if (GameGlobals.Paused)
        {
            PauseButton.Draw();
        }
    }
    protected virtual void DrawGame(Color aColor = new Color())
    {
    }
    protected virtual void DoAudio()
    {
       
    }
    protected virtual bool Spawn()
    {
        return true;
    }
    protected virtual void Shake()
    {
        GameGlobals.ShakeTime -= Time.deltaTime;
        if (GameGlobals.ShakeTime > 0)
        {
            IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f + IDrag.Random.GetRandom((int)(Screen.width * -0.01f), (int)(Screen.width * 0.01f)), Screen.height * 0.5f + IDrag.Random.GetRandom((int)(Screen.height * -0.01f), (int)(Screen.height * 0.01f))));
        }
    }
    protected virtual void DoCollision()
    {
    }





}
