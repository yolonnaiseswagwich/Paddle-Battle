using UnityEngine;
using System.Collections;
public struct AABB
{
    public Vector2 Min;
    public Vector2 Max;
};
public class Entity {
    protected bool Killed, Dead;
    protected Color MyColor;
    protected Texture2D m_aTexture; //this is the texture protected because we load on runtime.
    protected Rect m_aTexRect; //protected Tex Cords
    protected Rect m_aRect; //protected because this is the base of the objects (its the positon and size).
    protected AABB CollisionBox;
    protected Material Shaders;
    public virtual bool Init(float x, float y, int sx, int sy)
    {
        Shaders = ShaderLib.GetShader(ShaderLib.DefaultColorTex);
        m_aRect = new Rect(x, y, sx, sy);
        MyColor = ColorLib.GetRandomNeon();
        m_aTexture = SpriteLib.GetTexture(SpriteLib.Default);
        return true;
    }
    public virtual bool Update()
    {
        if (!Killed)
        {
        }
        else
        {
        }
        if (Dead)
        {
            return false;
        }
        return true;
    }
    public virtual bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
    {
        Rect Temp = new Rect(m_aRect.x - m_aRect.width * 0.5f, m_aRect.y - m_aRect.height * 0.5f, m_aRect.width, m_aRect.height);
        Temp = IDrag.D2Camera.rGetPosRel(Temp);
        if (Temp.y - Temp.height * 1.1f <= Screen.height * 1.1f && Temp.y + Temp.height * 1.1f >= 0 && Temp.x - Temp.width * 1.1f <= Screen.width && Temp.x + Temp.width * 1.1f >= 0)
        {
            if (aColor == new Color())
            {
                aColor = MyColor;
            }
            else
            {
                aColor *= MyColor;
            }
            //do shaderdataspecificstuff
            {
                Shaders.mainTexture = m_aTexture;
            }
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            //change it so that it draws in the right positon
            if (GameGlobals.ColorBlind == 0)
            {
                IDrag.Shapes.CreateBox(IDrag.D2Camera.DrawPos(m_aRect), aColor);
            }
            else
            {
                IDrag.Shapes.CreateBox(IDrag.D2Camera.DrawPos(m_aRect), Color.white);
            }
            GL.End();
            GL.PopMatrix();
            return true;
        }
        return false;
    }
    public void SetShader(int i)
    {
        Shaders = ShaderLib.GetShader(i);
    }
    public Texture2D GetTex()
    {
        return m_aTexture;
    }
    public void SetTex(int i)
    {
        m_aTexture = SpriteLib.GetTexture(i);
    }
    public virtual void SetPos(float x, float y)
    {
        m_aRect.x = x;
        m_aRect.y = y;
    }
    public virtual void SetPos(Vector2 aVec)
    {
        m_aRect.x = aVec.x;
        m_aRect.y = aVec.y;
    }
    public virtual Vector2 GetPos()
    {
        return new Vector2(m_aRect.x, m_aRect.y);
    }
    public virtual Vector2 GetSize()
    {
        return new Vector2(m_aRect.width, m_aRect.height);
    }
    public virtual void SetSize(float x, float y)
    {
        m_aRect.width = x;
        m_aRect.height = y;
    }
    public virtual void SetSize(Vector2 aVec)
    {
        m_aRect.width = aVec.x;
        m_aRect.height = aVec.y;
    }
    public virtual Color GetColor()
    {
        return MyColor;
    }
    public virtual void SetColor(Color aColor)
    {
        MyColor = aColor;
    }
    public virtual void SetKilled()
    {
        Killed = true;
    }
    public virtual bool GetKilled()
    {
        return Killed;
    }
    public virtual float GetRad()
    {
        return m_aRect.width * 0.5f;
    }
    public virtual AABB GetCollider()
    {
        return CollisionBox;
    }
    protected virtual void SetCollider()
    {
        CollisionBox.Min.x = m_aRect.x - m_aRect.width * 0.5f;
        CollisionBox.Max.x = m_aRect.x + m_aRect.width * 0.5f;
        CollisionBox.Min.y = m_aRect.y - m_aRect.height * 0.5f;
        CollisionBox.Max.y = m_aRect.y + m_aRect.height * 0.5f;
    }









}
