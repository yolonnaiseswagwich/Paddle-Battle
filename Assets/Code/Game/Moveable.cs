using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moveable : Entity
{
    public Vector2 Direction;
    protected float m_fSpeed;
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy))
        {
            m_fSpeed = Screen.height * 0.3f;
            Direction = new Vector2();
            SetCollider();
            return true;
        }
        return false;
    }
    public virtual void SetDirection(Vector2 a_Direction)
    {
        Direction = a_Direction;
    }
    public override bool Update()
    {
        if (base.Update())
        {
            m_aRect.x += Direction.x * m_fSpeed * Time.deltaTime;
            m_aRect.y += Direction.y * m_fSpeed * Time.deltaTime;
            SetCollider();
            return true;
        }
        return false;
    }
}