﻿using UnityEngine;
using System.Collections;

public class Multi : Paddle
{
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy))
        {
            SetCollider();
            return true;
        }
        return false;
    }
    // Update is called once per frame
    public override bool Update()
    {
        if (base.Update())
        {
            //if (m_aRect.y < Screen.height * 0.78f)
            //{
            //    m_aRect.y = Screen.height * 0.78f;
            //}
            SetCollider();
            return true;
        }
        return false;
    }
}
