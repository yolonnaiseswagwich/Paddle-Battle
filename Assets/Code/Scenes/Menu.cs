using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
public class Menu : MonoBehaviour
{
    private Material Background;
    List<UI.Image> Buttons;
    bool Credits;
    bool HTP;
    UI.SqrButton CredOverlay;
    UI.SqrButton HTPOverlay;
    UI.SqrButton CorruptSave;
    UI.SqrButton aWebsite;
    protected bool InApp;
    public playerai mPlayer;
    public Enemy mEnemy;
    public Block[] Blocks;
    public List<Ball> Balls;
    AudioSource a;
    private float Timer;
    protected float SpawnTimer;
    protected float SpawnTime;
    void OnApplicationPause()
    {
        InApp = false;
    }
    void Awake()
    {
        Advertisement.Initialize("1436704");
        InApp = true;
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
    }
    void Start()
    {
        GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
        GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume * 0.01f;
        a.loop = false;
        a.Pause();
        Score.m_iScore1 = 0;
        Score.m_iScore2 = 0;
        Score.m_fGameTime = 0;
        //Shaders = ShaderLib.GetShader(ShaderLib.Julia);
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Credits = false;
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp;
        UI.Image Temp3;
        Temp3 = new UI.Image();
        Temp3.Init(Screen.width * 0.5f, Screen.height * 0.9425f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.25f), "PB");
        Temp3.SetTex(SpriteLib.SSE);
        Temp3.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp3);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.5f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.45f), "Play");
        Temp.SetTex(SpriteLib.Play);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.935f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "HTP");
        Temp.SetTex(SpriteLib.HTP);
        Temp.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.935f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Credits");
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
        HTPOverlay = new UI.SqrButton();
        HTPOverlay.Init(Screen.width * 0.5f, Screen.height * 0.5f, (int)(Screen.width), (int)(Screen.height), "HTPOverlay");
        HTPOverlay.SetTex(SpriteLib.HTPOverlay);
        HTPOverlay.SetShader(ShaderLib.Overlay);
        CorruptSave = new UI.SqrButton();
        CorruptSave.Init(Screen.width * 0.5f, Screen.height * 0.5f, (int)(Screen.width), (int)(Screen.height), "Corrupt");
        CorruptSave.SetTex(SpriteLib.CorruptOverlay);
        CorruptSave.SetShader(ShaderLib.Overlay);
        aWebsite = new UI.SqrButton();
        aWebsite.Init(Screen.width * 0.5f, Screen.height * 0.54f, Screen.width, (int)(Screen.height * 0.1f), "Website");
        aWebsite.SetTex(SpriteLib.Website);
        aWebsite.SetShader(ShaderLib.OverlayNBG);
        Credits = HTP = false;
        if (GameGlobals.Corrupt)
        {
            a.clip = SoundLib.GetSound(SoundLib.Error);
            a.Play();
        }
        mPlayer = new playerai();
        mPlayer.Init(0, 0, 1, 1);
        mEnemy = new Enemy();
        mEnemy.Init(0, 0, 1, 1);
        Blocks = new Block[30];
        Balls = new List<Ball>();
        Spawn();
        mPlayer.Update();
        mEnemy.Update();

        Ball TBall = new Ball();
        TBall.Init(mPlayer.GetPos().x, mPlayer.GetPos().y, 1, 1);
        TBall.SetDirection(new Vector2(0.7071067811865475f, 0.7071067811865475f));
        TBall.Last = mPlayer;
        Balls.Add(TBall);
        TBall = new Ball();
        TBall.Init(mEnemy.GetPos().x, mEnemy.GetPos().y, 1, 1);
        TBall.SetDirection(new Vector2(0.7071067811865475f, -0.7071067811865475f));
        TBall.Last = mEnemy;
        Balls.Add(TBall);
        SpawnTimer = SpawnTime = 5.0f;
        Timer = 0;
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
    void Update() {
        Timer += Time.deltaTime;
        if (Timer > 300.0f) {
            Timer = 0;
            Balls.Clear();
            mPlayer = new playerai();
            mPlayer.Init(0, 0, 1, 1);
            mEnemy = new Enemy();
            mEnemy.Init(0, 0, 1, 1);
            Blocks = new Block[30];
            Balls = new List<Ball>();
            Spawn();
            mPlayer.Update();
            mEnemy.Update();

            Ball TBall = new Ball();
            TBall.Init(mPlayer.GetPos().x, mPlayer.GetPos().y, 1, 1);
            TBall.SetDirection(new Vector2(0.7071067811865475f, 0.7071067811865475f));
            TBall.Last = mPlayer;
            Balls.Add(TBall);
            TBall = new Ball();
            TBall.Init(mEnemy.GetPos().x, mEnemy.GetPos().y, 1, 1);
            TBall.SetDirection(new Vector2(0.7071067811865475f, -0.7071067811865475f));
            TBall.Last = mEnemy;
            Balls.Add(TBall);
            SpawnTimer = SpawnTime = 5.0f;
        }
        for (int i = 0; i < 30; ++i) {
            if (Blocks[i] != null && !Blocks[i].GetIsol() && BlockIsolated(i)) {
                Blocks[i].SetIsol(Balls.Count);
            }
            if (Blocks[i] != null && !Blocks[i].Update()) {
                if (Blocks[i].GetMake()) {
                    Ball TempBall = new Ball();
                    TempBall.Init(Blocks[i].GetPos().x, Blocks[i].GetPos().y, 1, 1);
                    TempBall.SetColor(Blocks[i].GetColor());
                    TempBall.SetDirection(new Vector2(IDrag.Random.GetRandom(0, 1) > 0 ? 0.7071067811865475f : -0.7071067811865475f, IDrag.Random.GetRandom(0, 1) > 0 ? 0.7071067811865475f : -0.7071067811865475f));
                    Balls.Add(TempBall);
                }
                Blocks[i] = null;
            }
        }
        mPlayer.Update();
        mEnemy.Update();
        foreach (Ball i in Balls) {
            i.Update();
        }
        DOAI();
        DoCollision();
        bool AllDead = true;
        for (int i = 0; i < 30; ++i) {
            if (Blocks[i] != null) {
                AllDead = false;
            }
        }
        if (AllDead) {
            if (SpawnTimer < 0) {
                SpawnRow();
                SpawnTimer = SpawnTime;
            }
            SpawnTimer -= Time.deltaTime;
        }
        if (!Credits && !HTP && !GameGlobals.Corrupt) {
            foreach (UI.Image i in Buttons) {
                if (i.Update()) {
                    a.clip = i.GetClip();
                    a.Play();
                    switch (i.GetName()) {
                        case "Play":
                            SceneManager.LoadScene(10);
                            break;
                        case "HTP":
                            HTP = true;
                            //GameInfo.GameType = GameInfo.Menu;
                            break;
                        case "Credits":
                            Credits = true;
                            //GameInfo.GameType = GameInfo.Menu;
                            //SceneManager.LoadScene();
                            break;
                        case "NA":
                            // GameInfo.GameType = GameInfo.Menu;
                            //SceneManager.LoadScene(4);
                            break;
                        case "Achievments":
                            // GameInfo.GameType = GameInfo.Menu;
                            //SceneManager.LoadScene(5);
                            break;
                        case "Settings":
                            GameInfo.GameType = GameInfo.Menu;
                            SceneManager.LoadScene(2);
                            break;
                        case "Rate":
                            StartCoroutine(Rate());
                            break;
                        case "Like":
                            StartCoroutine(Like());
                            break;
                    }
                }
            }
        } else {
            if (Credits) {
                if (aWebsite.Update()) {
                    a.clip = aWebsite.GetClip();
                    Application.OpenURL("https://www.implodingdragons.com.html");
                } else {
                    if (CredOverlay.Update()) {
                        a.clip = CredOverlay.GetClip();
                        a.Play();
                        Credits = false;
                    }
                }
            } else if (HTP) {
                if (HTPOverlay.Update()) {
                    a.clip = HTPOverlay.GetClip();
                    a.Play();
                    HTP = false;
                }
            } else if (GameGlobals.Corrupt) {
                if (CorruptSave.Update()) {
                    a.clip = CorruptSave.GetClip();
                    a.Play();
                    GameGlobals.Corrupt = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Credits || HTP) {
                Credits = false;
                HTP = false;
            } else {
                Application.Quit();
            }
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
        Color aColor = new Color(1.0f, 1.0f, 1.0f, 0.25f);
        for (int i = 0; i < 30; ++i)
        {
            if (Blocks[i] != null)
                Blocks[i].Draw(aColor);
        }
        foreach (Ball i in Balls)
        {
            i.Draw(aColor);
        }
        mPlayer.Draw(aColor);
        mEnemy.Draw(aColor);
        ///////////////////////////////////
        /// Julia shader background code///
        ///////////////////////////////////
        //Vector4 Variables1 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) * 0.02f + 0.37f, Mathf.Sin(Time.timeSinceLevelLoad * 1.22f) * 0.02f + 0.37f, 17.0f, 3.14159f);
        //Vector4 Variables2 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 0.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 0.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.87f));
        //Shaders.SetVector("MyVar", Variables1);
        //Shaders.SetVector("MyVarb", Variables2);
        //GL.PushMatrix();
        //Shaders.SetPass(0);
        //GL.LoadOrtho();
        //GL.Begin(GL.QUADS);
        //if (Screen.width > Screen.height)
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3(0, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3(0, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3(1, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3(1, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //}
        //else if (Screen.height > Screen.width)
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
        //}
        //else
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3(0, 0, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3(0, 1, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3(1, 1, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3(1, 0, 0.1F);
        //}
        //GL.End();
        //GL.PopMatrix();
        Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.37f), Mathf.Cos(Time.timeSinceLevelLoad * 0.71f), Mathf.Sin(Time.timeSinceLevelLoad * 1.11f), Mathf.Cos(Time.timeSinceLevelLoad * 0.63f));
        Color ColorVar = new Color(0.2f + (Mathf.Abs(Variables3.x) * 0.8f), 0.2f + (Mathf.Abs(Variables3.y) * 0.8f), 0.2f + (Mathf.Abs(Variables3.z) * 0.8f), 2.0f);
        foreach (UI.Image i in Buttons)
        {

            i.Draw(ColorVar);
            //if (i.GetName() == "Play" || i.GetName() == "PB" || i.GetName() == "Credits") {
            //}
            //else if ()
            //{
            //    i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
            //}
        }
        if (Credits)
        {
            CredOverlay.Draw();
            aWebsite.Draw();
        }
        if (HTP)
        {
            HTPOverlay.Draw();
        }
        if (GameGlobals.Corrupt)
        {
            CorruptSave.Draw();
        }
    }
    void DoCollision()
    {
        for (int i = 0; i < Balls.Count; ++i)
        {
            if (Balls[i].GetCollider().Max.x >= Screen.width)
            {
                if (Balls[i].Direction.x > 0)
                {
                    Balls[i].Direction.x *= -1.0f;
                    //   Stats.Speed += Screen.height * 0.001f;
                }
            }
            if (Balls[i].GetCollider().Min.x <= 0)
            {
                if (Balls[i].Direction.x < 0)
                {
                    Balls[i].Direction.x *= -1.0f;
                    //  Stats.Speed += Screen.height * 0.001f;
                }
            }
            if (Balls[i].GetCollider().Max.y >= Screen.height)
            {
                if (Balls[i].Direction.y > 0)
                {
                    Balls[i].Direction.y *= -1.0f;
                }
            }
            if (Balls[i].GetCollider().Min.y <= 0)
            {
                if (Balls[i].Direction.y < 0)
                {
                    Balls[i].Direction.y *= -1.0f;
                }
            }
            if (IDrag.Collision.AABBvsCircle(mEnemy.GetCollider(), Balls[i].GetCollider()) && Balls[i].Direction.y > 0)
            {
                mEnemy.SetColor(Balls[i].GetColor());
                Balls[i].Direction.y *= -1.0f;
                Balls[i].Last = mEnemy;
                Balls[i].ReDirect(Balls[i].Last.GetPos());
                continue;
                //Stats.Speed += Screen.height * 0.001f;
            }
            if (IDrag.Collision.AABBvsCircle(mPlayer.GetCollider(), Balls[i].GetCollider()) && Balls[i].Direction.y < 0)
            {
                mPlayer.SetColor(Balls[i].GetColor());
                Balls[i].Direction.y *= -1.0f;
                Balls[i].Last = mPlayer;
                Balls[i].ReDirect(Balls[i].Last.GetPos());
                continue;
                //Stats.Speed += Screen.height * 0.001f;
            }
            foreach (Block k in Blocks)
            {
                if (k != null && IDrag.Collision.AABBvsCircle(k.GetCollider(), Balls[i].GetCollider()) && k != Balls[i].Last)
                {
                    Balls[i].SetColor(k.GetColor());
                    Balls[i].BlockDirect(k.GetCollider());
                    k.SetKilled();
                    Balls[i].Last = k;
                    break;
                    //  ReDirect(Last.GetPos());
                    //     Stats.Speed += Screen.height * 0.001f;
                }
            }
            foreach (Ball k in Balls)
            {
                if (IDrag.Collision.CirclevsCircle(k.GetCollider(), Balls[i].GetCollider()) && k != Balls[i].Last && k != Balls[i])
                {
                    Balls[i].Last = k;
                    Balls[i].ReDirect(Balls[i].Last.GetPos());
                    break;
                }
            }
        }
    }
    void SpawnRow()
    {
        int i = 10;
        int j = 3;
        if (IDrag.Random.GetRandom(0, 1) % 2 == 0)
        {
            for (int x = 3; x < i - 3; ++x)
            {
                for (int y = 0; y < j; ++y)
                {
                    Blocks[x + y * i] = new Block();
                    Blocks[x + y * i].Init(Screen.width * 0.5f / i + Screen.width * x / i, Screen.height * 0.45f + Screen.height * 0.05f * y, 1, 1);
                }
            }
        }
        else
        {
            for (int x = 0; x < i; ++x)
            {
                for (int y = 0; y < j; ++y)
                {
                    if (x <= 2 || x >= i - 3)
                    {
                        Blocks[x + y * i] = new Block();
                        Blocks[x + y * i].Init(Screen.width * 0.5f / i + Screen.width * x / i, Screen.height * 0.45f + Screen.height * 0.05f * y, 1, 1);
                    }
                }
            }
        }

    }
    bool Spawn()
    {
        int i = 10;
        int j = 3;
        for (int x = 0; x < i; ++x)
        {
            for (int y = 0; y < j; ++y)
            {
                Blocks[x + y * i] = new Block();
                Blocks[x + y * i].Init(Screen.width * 0.5f / i + Screen.width * x / i, Screen.height * 0.45f + Screen.height * 0.05f * y, 1, 1);
            }
        }
        return true;
    }
    public bool BlockIsolated(int i)
    {
        switch (i)
        {
            case 0:
            {
                if (Blocks[1] == null && Blocks[10] == null && Blocks[11] == null)
                {
                    return true;
                }
                break;
            }
            case 1:
            {
                if (Blocks[0] == null && Blocks[2] == null && Blocks[10] == null && Blocks[11] == null && Blocks[12] == null)
                {
                    return true;
                }
                break;
            }
            case 2:
            {
                if (Blocks[1] == null && Blocks[3] == null && Blocks[11] == null && Blocks[12] == null && Blocks[13] == null)
                {
                    return true;
                }
                break;
            }
            case 3:
            {
                if (Blocks[2] == null && Blocks[4] == null && Blocks[12] == null && Blocks[13] == null && Blocks[14] == null)
                {
                    return true;
                }
                break;
            }
            case 4:
            {
                if (Blocks[3] == null && Blocks[5] == null && Blocks[13] == null && Blocks[14] == null && Blocks[15] == null)
                {
                    return true;
                }
                break;
            }
            case 5:
            {
                if (Blocks[4] == null && Blocks[6] == null && Blocks[14] == null && Blocks[15] == null && Blocks[16] == null)
                {
                    return true;
                }
                break;
            }
            case 6:
            {
                if (Blocks[5] == null && Blocks[7] == null && Blocks[15] == null && Blocks[16] == null && Blocks[17] == null)
                {
                    return true;
                }
                break;
            }
            case 7:
            {
                if (Blocks[6] == null && Blocks[8] == null && Blocks[16] == null && Blocks[17] == null && Blocks[18] == null)
                {
                    return true;
                }
                break;
            }
            case 8:
            {
                if (Blocks[7] == null && Blocks[9] == null && Blocks[17] == null && Blocks[18] == null && Blocks[19] == null)
                {
                    return true;
                }
                break;
            }
            case 9:
            {
                if (Blocks[8] == null && Blocks[18] == null && Blocks[19] == null)
                {
                    return true;
                }
                break;
            }
            case 10:
            {
                if (Blocks[0] == null && Blocks[1] == null && Blocks[20] == null && Blocks[11] == null && Blocks[21] == null)
                {
                    return true;
                }
                break;
            }
            case 11:
            {
                if (Blocks[0] == null && Blocks[1] == null && Blocks[2] == null && Blocks[10] == null && Blocks[12] == null && Blocks[20] == null && Blocks[21] == null && Blocks[22] == null)
                {
                    return true;
                }
                break;
            }
            case 12:
            {
                if (Blocks[1] == null && Blocks[2] == null && Blocks[3] == null && Blocks[11] == null && Blocks[13] == null && Blocks[21] == null && Blocks[22] == null && Blocks[23] == null)
                {
                    return true;
                }
                break;
            }
            case 13:
            {
                if (Blocks[2] == null && Blocks[3] == null && Blocks[4] == null && Blocks[12] == null && Blocks[14] == null && Blocks[22] == null && Blocks[23] == null && Blocks[24] == null)
                {
                    return true;
                }
                break;
            }
            case 14:
            {
                if (Blocks[3] == null && Blocks[4] == null && Blocks[5] == null && Blocks[13] == null && Blocks[15] == null && Blocks[23] == null && Blocks[24] == null && Blocks[25] == null)
                {
                    return true;
                }
                break;
            }
            case 15:
            {
                if (Blocks[4] == null && Blocks[5] == null && Blocks[6] == null && Blocks[14] == null && Blocks[16] == null && Blocks[24] == null && Blocks[25] == null && Blocks[26] == null)
                {
                    return true;
                }
                break;
            }
            case 16:
            {
                if (Blocks[5] == null && Blocks[6] == null && Blocks[7] == null && Blocks[15] == null && Blocks[17] == null && Blocks[25] == null && Blocks[26] == null && Blocks[27] == null)
                {
                    return true;
                }
                break;
            }
            case 17:
            {
                if (Blocks[6] == null && Blocks[7] == null && Blocks[8] == null && Blocks[16] == null && Blocks[18] == null && Blocks[26] == null && Blocks[27] == null && Blocks[28] == null)
                {
                    return true;
                }
                break;
            }
            case 18:
            {
                if (Blocks[7] == null && Blocks[8] == null && Blocks[9] == null && Blocks[17] == null && Blocks[19] == null && Blocks[27] == null && Blocks[28] == null && Blocks[29] == null)
                {
                    return true;
                }
                break;
            }
            case 19:
            {
                if (Blocks[8] == null && Blocks[9] == null && Blocks[18] == null && Blocks[28] == null && Blocks[29] == null)
                {
                    return true;
                }
                break;
            }
            case 20:
            {
                if (Blocks[21] == null && Blocks[10] == null && Blocks[11] == null)
                {
                    return true;
                }
                break;
            }
            case 21:
            {
                if (Blocks[20] == null && Blocks[22] == null && Blocks[10] == null && Blocks[11] == null && Blocks[12] == null)
                {
                    return true;
                }
                break;
            }
            case 22:
            {
                if (Blocks[21] == null && Blocks[23] == null && Blocks[11] == null && Blocks[12] == null && Blocks[13] == null)
                {
                    return true;
                }
                break;
            }
            case 23:
            {
                if (Blocks[22] == null && Blocks[24] == null && Blocks[12] == null && Blocks[13] == null && Blocks[14] == null)
                {
                    return true;
                }
                break;
            }
            case 24:
            {
                if (Blocks[23] == null && Blocks[25] == null && Blocks[13] == null && Blocks[14] == null && Blocks[15] == null)
                {
                    return true;
                }
                break;
            }
            case 25:
            {
                if (Blocks[24] == null && Blocks[26] == null && Blocks[14] == null && Blocks[15] == null && Blocks[16] == null)
                {
                    return true;
                }
                break;
            }
            case 26:
            {
                if (Blocks[25] == null && Blocks[27] == null && Blocks[15] == null && Blocks[16] == null && Blocks[17] == null)
                {
                    return true;
                }
                break;
            }
            case 27:
            {
                if (Blocks[26] == null && Blocks[28] == null && Blocks[16] == null && Blocks[17] == null && Blocks[18] == null)
                {
                    return true;
                }
                break;
            }
            case 28:
            {
                if (Blocks[27] == null && Blocks[29] == null && Blocks[17] == null && Blocks[18] == null && Blocks[19] == null)
                {
                    return true;
                }
                break;
            }
            case 29:
            {
                if (Blocks[28] == null && Blocks[18] == null && Blocks[19] == null)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
    void DOAI()
    {
        foreach (Ball i in Balls)
        {
            if (i.GetPos().y > Screen.height * 0.8f && i.Direction.y > 0.0f)
            {
                mEnemy.SetPos(i.GetPos() + new Vector2(IDrag.Random.GetRandom(-15, 15), Screen.height * 0.025f));
                break;
            }
            if (i.GetPos().y < Screen.height * 0.2f && i.Direction.y < 0.0f)
            {
                mPlayer.SetPos(i.GetPos() + new Vector2(IDrag.Random.GetRandom(-15, 15), Screen.height * -0.025f));
                break;
            }
        }
    }

   
}
