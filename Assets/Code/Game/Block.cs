using UnityEngine;
using System.Collections;

public class Block : Entity {
    
    float Timer;
    bool Isolated;
    bool MakeBall;
    bool ChangeColor;
    float ColorTimer;
    Color Color1;
    Color Color2;
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy))
        {
            MyColor = ColorLib.GetRandomNeon();
            Color1 = MyColor;
            Timer = 0.25f;
            ColorTimer = 0.0f;
            ChangeColor = false;
            MakeBall = false;
            Isolated = false;
            m_aRect.width = Screen.width * 0.1f;
            m_aRect.height = Screen.height * 0.05f;
            SetCollider();
            m_aTexture = SpriteLib.GetTexture(SpriteLib.Block0);
            Shaders = ShaderLib.GetShader(ShaderLib.Neon);
            return true;
        }
        return false;
    }
    public override bool Update()
    {
        if (base.Update())
        {
            if (Killed && Timer > 0)
            {
                m_aRect.width = Screen.width * 0.125f * Timer * 7.5f;
                m_aRect.height = Screen.height * 0.075f * Timer * 7.5f;
                Timer -= Time.deltaTime;
            }
            else if (Killed && Timer <= 0)
            {
                return false;
            }
            if (ChangeColor)
            {
                ColorTimer += Time.deltaTime;
                if (ColorTimer >= 0.25f)
                {
                    ColorTimer = 0.0f;
                    SetColor(ColorLib.GetRandomNeon());

                }
                else
                {
                    MyColor = new Color(Mathf.Lerp(Color1.r, Color2.r, ColorTimer * 4), Mathf.Lerp(Color1.g, Color2.g, ColorTimer * 4), Mathf.Lerp(Color1.b, Color2.b, ColorTimer * 4));
                }
            }
            SetCollider();
            return true;
        }
        return false;
    }
    public void SetIsol(int i)
    {
        Isolated = true;
        MakeBall = true;
        ChangeColor = true;
    }
    public bool GetIsol()
    {
        return Isolated;
    }
    public bool GetMake()
    {
        return MakeBall;
    }
    public override void SetColor(Color aColor)
    {
        Color1 = MyColor;
        Color2 = aColor;
    }
    public override Color GetColor()
    {
        return Color1;
    }
}
