using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;
public class Player : Paddle {
    public override bool Init(float x, float y, int sx, int sy) {
        if (base.Init(x, y, sx, sy)) {
            SetCollider();
            m_aTexture = SpriteLib.GetTexture(SpriteLib.Paddle0);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    public override bool Update() {
        if (base.Update()) {
#if UNITY_WEB || UNITY_EDITOR
            if (Input.mousePosition.y < Screen.height*0.4f) {
                m_aRect.x = Input.mousePosition.x;
                m_aRect.y = Input.mousePosition.y;
            }
#endif
            
            foreach (Touch i in Input.touches) {
                    if (i.position.y < Screen.height*0.4f) {
                    m_aRect.x = i.position.x;
                    m_aRect.y = i.position.y;
                    }
            }
            if (m_aRect.y > Screen.height*0.22f) {
                m_aRect.y = Screen.height*0.22f;
            }
            //temp Code
            //m_aRect.y = Screen.height * 0.05f;
            SetCollider();
            return true;
        }
        return false;
    }
    public override bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
    {
        //if (touching)
        {
            if (!base.Draw(aColor, aShaderData)) {
                return false;
            }
        }
        return true;
    }
}