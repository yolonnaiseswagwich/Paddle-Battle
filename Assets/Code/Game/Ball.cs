using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : Moveable {
    public class Trail : Entity
    {
        private const float TrailLife = 0.25f;
        private float Timer;
        public override bool Init(float x, float y, int sx, int sy)
        {
            if (base.Init(x, y, sx, sy))
            {
                //m_aTexture = SpriteLib.GetTexture(SpriteLib.Line);
                Shaders = ShaderLib.GetShader(ShaderLib.Neon);
                Timer = TrailLife;
                m_aRect.x = x * IDrag.D2Camera.PixelSize.x;
                m_aRect.y = y * IDrag.D2Camera.PixelSize.y;
                m_aRect.width = sx * IDrag.D2Camera.PixelSize.x;
                m_aRect.height = sy * IDrag.D2Camera.PixelSize.y;
                return true;
            }
            return false;
        }
        public override bool Update()
        {
            if (base.Update())
            {
                Timer -= Time.deltaTime;
                if (Timer < 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public override bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
        {
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            IDrag.Shapes.CreateLine(m_aRect, MyColor);
            GL.End();
            GL.PopMatrix();
            return true;
        }
    }

    // private List<Trail> Trails;
    public Entity Last;
    bool Created;
    float Timer;
    bool ChangeColor;
    float ColorTimer;
    Color Color1;
    Color Color2;
    public override void SetColor(Color aColor)
    {
        Color1 = MyColor;
        Color2 = aColor;
        ChangeColor = true;
    }
    public float GetDirection()
    {
        return Direction.y;
    }
    public float GetSpeed()
    {
        return m_fSpeed;
    }
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy))
        {
            MyColor = ColorLib.GetRandomColor();
            Color1 = MyColor;
            Created = false;
            ChangeColor = false;
            Timer = 0.2f;
            ColorTimer = 0.0f;
            m_aTexture = SpriteLib.GetTexture(SpriteLib.Ball0);
            Shaders = ShaderLib.GetShader(ShaderLib.Neon);
            m_aRect.width = Screen.width * 0.04f;
            m_aRect.height = Screen.height * 0.04f;
            m_fSpeed = Screen.height * 0.25f;
            if (m_aRect.width > m_aRect.height)
            {
                m_aRect.width = m_aRect.height;
            }
            else
            {
                m_aRect.height = m_aRect.width;
            }
            Direction = new Vector2(0, 0);
            Last = this;
            SetCollider();
            m_aRect.width = 0;
            m_aRect.height = 0;
            //Trails = new List<Trail>();
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
            if (Created)
            {
                SetCollider();
                //for (int i = 0; i < Trails.Count; ++i)
                //{
                //    if (!Trails[i].Update())
                //    {
                //        Trail aTrail = Trails[i];
                //        Trails.Remove(aTrail);
                //        aTrail.Destroy();
                //        --i;
                //    }
                //}
                //Rect Temp = m_aRect;
                //MakeTrail(Temp);
                return true;
            }
            else
            {
                m_aRect.width += Screen.width * 0.04f * 0.09f;
                m_aRect.height += Screen.height * 0.04f * 0.09f;
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                {
                    m_aRect.width = Screen.width * 0.04f;
                    m_aRect.height = Screen.height * 0.04f;
                    Created = true;
                    if (m_aRect.width > m_aRect.height)
                    {
                        m_aRect.width = m_aRect.height;
                    }
                    else
                    {
                        m_aRect.height = m_aRect.width;
                    }
                }
            }
        }
        return false;
    }
    protected virtual void MakeTrail(Rect Old)
    {
        Trail Temp = new Trail();
        Temp.Init(m_aRect.x, m_aRect.y, (int)Old.x, (int)Old.y);
        Temp.SetColor(MyColor);
       // Trails.Add(Temp);
    }
    public override bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
    {
        //foreach (Trail i in Trails)
        //{
        //    i.Draw(aColor, aShaderData);
        //}
        return base.Draw(aColor, aShaderData);
    }
    private bool isOnUpperSideOfLine(Vector2 corner1, Vector2 oppositeCorner, Vector2 ballCenter)
    {
        return ((oppositeCorner.x - corner1.x) * (ballCenter.y - corner1.y) - (oppositeCorner.y - corner1.y) * (ballCenter.x - corner1.x)) > 0;
    }
    public void ReDirect(Vector2 other)
    {
        Direction = (((GetPos() - other).normalized + Direction) * 0.5f).normalized;
    }
    public void BlockDirect(AABB Other)
    {
        bool isAboveAC = isOnUpperSideOfLine(Other.Max, Other.Min, GetPos());
        bool isAboveDB = isOnUpperSideOfLine(new Vector2(Other.Max.x, Other.Min.y), new Vector2(Other.Min.x, Other.Max.y), GetPos());

        if (isAboveAC)
        {
            if (isAboveDB)
            {
                //top edge has intersected
                Direction.y *= -1.0f;
            }
            else
            {
                //right edge intersected
                Direction.x *= -1.0f;
            }
        }
        else
        {
            if (isAboveDB)
            {
                //left edge has intersected
                Direction.x *= -1.0f;
            }
            else
            {
                //bottom edge intersected
                Direction.y *= -1.0f;
            }
        }
    }
}
