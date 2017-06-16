using UnityEngine;
using System.Collections;

public class Enemy : Paddle {
    private Ball Target;
    private float Mutiplier;
    private float Speed;
    public void SetTarget(Ball aBall, int Balls) {
        Target = aBall;
        Mutiplier = 1 + Balls * 0.12f;
    }
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy)) {
            Speed = Screen.width*0.375f;
            SetCollider();
            return true;
        }
        return false;
    }
    public override bool Update()
    {
        if (base.Update()) {
            if (Target != null)
            {
                Vector2 Temp = GetPos();
                Vector2 Location = Target.GetPos();
                Vector2 Direction = new Vector2(Temp.x - Location.x, Temp.y - Location.y);
                Direction.Normalize();
                Temp -= Direction * Speed * Mutiplier * Time.deltaTime;
                SetPos(Temp);
            }
        if (m_aRect.y < Screen.height * 0.78f)
            {
                m_aRect.y = Screen.height * 0.78f;
            }
            if (m_aRect.y > Screen.height)
            {
                m_aRect.y = Screen.height;
            }
            SetCollider();
            return true;
        }
        return false;
    }
}
