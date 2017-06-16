using UnityEngine;
using System.Collections;

namespace UI
{
    public class LableResize
    {
        public static bool Resize(Rect aRect, string aString)
        {
            return true;
        }
    }
    public class Image
    {
        protected Rect m_aRect; //protected because this is the base of the objects (its the positon and size).
        protected Material Shaders;
        protected Texture2D m_aTexture; //this is the texture protected because we load on runtime.
        protected Rect m_aTexRect; //protected Tex Cords
        protected int Clip;
        protected string Name;
        public virtual bool Init(float x, float y, int sx, int sy, string aName)
        {
            m_aTexture = SpriteLib.GetTexture(SpriteLib.Default);
            Shaders = ShaderLib.GetShader(ShaderLib.Default);
            m_aRect = new Rect(x, y, sx, sy);
            Name = aName;
            Clip = SoundLib.Click;
            return true;
        }
        public virtual bool Update()
        {
            return false;
        }
        public virtual bool Draw(ShaderData aShaderData = new ShaderData())
        {
            //do shaderdataspecificstuff
            {
                Shaders.mainTexture = m_aTexture;
            }
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            //change it so that it draws in the right positon
            IDrag.Shapes.CreateBox(IDrag.D2Camera.DrawPos(m_aRect));
            GL.End();
            GL.PopMatrix();
            return true;
        }
        public virtual bool Draw(Color aColor, ShaderData aShaderData = new ShaderData())
        {
            //do shaderdataspecificstuff
            {
                Shaders.mainTexture = m_aTexture;
            }
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            //change it so that it draws in the right positon
            IDrag.Shapes.CreateBox(IDrag.D2Camera.DrawPos(m_aRect), aColor);
            GL.End();
            GL.PopMatrix();
            return true;
        }
        public virtual bool DrawColor(Color aColor, ShaderData aShaderData = new ShaderData())
        {
            //do shaderdataspecificstuff
            {
                Shaders.mainTexture = m_aTexture;
            }
            GL.PushMatrix();
            Shaders.SetPass(0);
            GL.LoadOrtho();
            //change it so that it draws in the right positon
            Rect Temp = IDrag.D2Camera.DrawPos(m_aRect);
            GL.Begin(GL.QUADS);
            aColor.a = Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) + Time.timeSinceLevelLoad - 0.23f;
            GL.Color(aColor);
            GL.TexCoord2(0, 0);
            GL.Vertex3(Temp.x, Temp.y, 0.1F);
            aColor.a = Mathf.Cos(Time.timeSinceLevelLoad * 2.79f) * 3.13f + Time.timeSinceLevelLoad - 1.3f;
            GL.Color(aColor);
            GL.TexCoord2(0, 1);
            GL.Vertex3(Temp.x, Temp.height, 0.1F);
            aColor.a = Mathf.Cos(Time.timeSinceLevelLoad * 1.31f) * 2.43f + Time.timeSinceLevelLoad - 3.41f;
            GL.Color(aColor);
            GL.TexCoord2(1, 1);
            GL.Vertex3(Temp.width, Temp.height, 0.1F);
            aColor.a = Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) * 2.13f + Time.timeSinceLevelLoad - 3.32f;
            GL.Color(aColor);
            GL.TexCoord2(1, 0);
            GL.Vertex3(Temp.width, Temp.y, 0.1F);
            GL.End();
            GL.PopMatrix();
            return true;
        }
        public AudioClip GetClip()
        {
            return SoundLib.GetSound(Clip);
        }
        public void SetClip(int i)
        {
            Clip = i;
        }
        public Texture2D GetTex()
        {
            return m_aTexture;
        }
        public void SetTex(int i)
        {
            m_aTexture = SpriteLib.GetTexture(i);
        }
        public void SetShader(int i)
        {
            Shaders = ShaderLib.GetShader(i);
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
        public virtual float GetRad()
        {
            return m_aRect.width * 0.5f;
        }
        public virtual string GetName()
        {
            return Name;
        }

    }
    public class SqrButton : Image
    {
        public override bool Init(float x, float y, int sx, int sy, string aName)
        {
            if (base.Init(x, y, sx, sy, aName))
            {
                Shaders = ShaderLib.GetShader(ShaderLib.Default);
                return true;
            }
            return false;
        }
        public override bool Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector2 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (IDrag.D2Camera.GetPosRel(GetPos()).x - m_aRect.width * 0.5f <= MousePos.x && MousePos.x <= IDrag.D2Camera.GetPosRel(GetPos()).x + m_aRect.width * 0.5f && IDrag.D2Camera.GetPosRel(GetPos()).y - m_aRect.height * 0.5f <= MousePos.y && MousePos.y <= IDrag.D2Camera.GetPosRel(GetPos()).y + m_aRect.height * 0.5f)
                {
                    return true;
                }
            }
            return base.Update();
        }
    }
    public class CircleButton : Image
    {
        public override bool Init(float x, float y, int sx, int sy, string aName)
        {
            if (base.Init(x, y, sx, sy, aName))
            {
                m_aRect.height = sx;
                //Shaders = ShaderLib.GetShader(ShaderLib.CircleTex);
                Shaders = ShaderLib.GetShader(ShaderLib.Default);
                return true;
            }
            return false;
        }
        public override bool Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector2 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if ((IDrag.D2Camera.GetPosRel(GetPos()) - MousePos).sqrMagnitude < GetRad() * GetRad())
                {
                    return true;
                }
            }
            return base.Update();
        }
    }
    public class Text
    {
        protected Rect m_aRect; //protected because this is the base of the objects (its the positon and size).
        protected Material Shaders;
    }
}