using UnityEngine;
using System.Collections;

public class Paddle : Entity
{
    public bool Launched;
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
            ChangeColor = false;
            ColorTimer = 0.0f;
            m_aRect.width = Screen.width * 0.35f;
            m_aRect.height = Screen.height * 0.025f;
            m_aTexture = SpriteLib.GetTexture(SpriteLib.Paddle1);
            Shaders = ShaderLib.GetShader(ShaderLib.Neon);
            return true;
        }
        return false;
    }
    public override bool Update()
    {
        if (base.Update())
        {
            if (ChangeColor)
            {
                ColorTimer += Time.deltaTime;
                if (ColorTimer >= 0.25f)
                {
                    // MyColor = Color2;
                    ColorTimer = 0.0f;
                    ChangeColor = false;
                }
                else
                {
                    MyColor = new Color(Mathf.Lerp(Color1.r, Color2.r, ColorTimer * 4), Mathf.Lerp(Color1.g, Color2.g, ColorTimer * 4), Mathf.Lerp(Color1.b, Color2.b, ColorTimer * 4));
                }
            }
            return true;
        }
        return false;
    }
    public override void SetColor(Color aColor)
    {
        Color1 = MyColor;
        Color2 = aColor;
        ChangeColor = true;
    }
}
