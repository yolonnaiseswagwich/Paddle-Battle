using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LocalPlayer : BaseGame
{

    public Player mPlayer;
    public Local mEnemy;
    private float TinyTimer;
    protected override void Start()
    {
        base.Start();
        TinyTimer = 0;
        GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
        GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
        GameInfo.GameType = GameInfo.Local;
        mPlayer = new Player();
        mPlayer.Init(0, 0, 1, 1);
        mEnemy = new Local();
        mEnemy.Init(0, 0, 1, 1);
        Blocks = new Block[30];
        Balls = new List<Ball>();
        Spawn();
        mPlayer.Update();
        mEnemy.Update();

        Ball Temp = new Ball();
        Temp.Init(mPlayer.GetPos().x, mPlayer.GetPos().y, 1, 1);
        Temp.SetDirection(new Vector2(0.7071067811865475f, 0.7071067811865475f));
        Temp.Last = mPlayer;
        Balls.Add(Temp);
        Temp = new Ball();
        Temp.Init(mEnemy.GetPos().x, mEnemy.GetPos().y, 1, 1);
        Temp.SetDirection(new Vector2(0.7071067811865475f, -0.7071067811865475f));
        Temp.Last = mEnemy;
        Balls.Add(Temp);
    }
    protected override void Update()
    {
        base.Update();
        if (timer > 4.5f)
        {
            for (int i = 0; i < 30; ++i)
            {
                if (Blocks[i] != null && !Blocks[i].GetIsol() && BlockIsolated(i))
                {
                    Blocks[i].SetIsol(Balls.Count);
                }
                if (Blocks[i] != null && !Blocks[i].Update())
                {
                    if (Blocks[i].GetMake())
                    {
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
            foreach (Ball i in Balls)
            {
                i.Update();
            }
            DoCollision();
            bool AllDead = true;
            for (int i = 0; i < 30; ++i)
            {
                if (Blocks[i] != null)
                {
                    AllDead = false;
                }
            }
            if (AllDead)
            {
                if (SpawnTimer < 0)
                {
                    SpawnRow();
                    SpawnTimer = SpawnTime;
                }
                SpawnTimer -= Time.deltaTime;
            }
        }
        else
        {
            mPlayer.Update();
            mEnemy.Update();
            Balls[0].Update();
            Balls[1].Update();
            Balls[0].SetPos(mPlayer.GetPos() + new Vector2(0, mPlayer.GetSize().y * 0.5f + Balls[0].GetRad()));
            Balls[1].SetPos(mEnemy.GetPos() - new Vector2(0, mEnemy.GetSize().y * 0.5f + Balls[1].GetRad()));
            TinyTimer += Time.deltaTime;
            if (TinyTimer > 0.11f)
            {
                TinyTimer = 0;
                Spawn();
            }
        }
    }
    protected override void DrawGame(Color aColor = new Color())
    {
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
    }
    protected override void DoAudio()
    {

    }
    protected override void DoCollision()
    {
        for (int i = 0; i < Balls.Count; ++i)
        {
            if (Balls[i].Direction.x > 0)
            {
                if (Balls[i].GetCollider().Max.x >= Screen.width)
                {
                    Balls[i].Direction.x *= -1.0f;
                    //   Stats.Speed += Screen.height * 0.001f;
                    continue;
                }
            }
            if (Balls[i].Direction.x < 0)
            {
                if (Balls[i].GetCollider().Min.x <= 0)
                {
                    Balls[i].Direction.x *= -1.0f;
                    //  Stats.Speed += Screen.height * 0.001f;
                    continue;
                }
            }
            if (Balls[i].Direction.y > 0)
            {
                if (Balls[i].GetCollider().Max.y >= Screen.height)
                {
                    Balls[i].Direction.y *= -1.0f;
                    Balls.RemoveAt(i);
                    ++Score.m_iScore1;
                    continue;
                }
            }
            if (Balls[i].Direction.y < 0)
            {
                if (Balls[i].GetCollider().Min.y <= 0)
                {
                    Balls[i].Direction.y *= -1.0f;
                    Balls.RemoveAt(i);
                    ++Score.m_iScore2;
                    continue;
                }
            }
            if (Balls[i].Direction.y > 0 && IDrag.Collision.AABBvsCircle(mEnemy.GetCollider(), Balls[i].GetCollider()))
            {
                mEnemy.SetColor(Balls[i].GetColor());
                Balls[i].Direction.y *= -1.0f;
                Balls[i].Last = mEnemy;
                Balls[i].ReDirect(Balls[i].Last.GetPos());
                continue;
                //Stats.Speed += Screen.height * 0.001f;
            }
            if (Balls[i].Direction.y < 0 && IDrag.Collision.AABBvsCircle(mPlayer.GetCollider(), Balls[i].GetCollider()))
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
                if (k != null && k != Balls[i].Last && IDrag.Collision.AABBvsCircle(k.GetCollider(), Balls[i].GetCollider()))
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
                if (k != Balls[i].Last && k != Balls[i] && IDrag.Collision.CirclevsCircle(k.GetCollider(), Balls[i].GetCollider()))
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
    protected override bool Spawn()
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

}