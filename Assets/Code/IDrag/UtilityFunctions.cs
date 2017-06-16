using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
namespace IDrag
{
    public class Globals
    {
        public const float PI2 = 6.283185307179586476925286766559f;//2 PI
        public const float PI2DIV = 0.15915494309189533576888376337251f;//Times by this to divide by 2 PI
        public const float PI = 3.1415926535897932384626433832795f;//3.14 pi der
        public const float PIDIV = 0.31830988618379067153776752674503f;//use this to divide by PI
    }
    //public class D2Camera // 2d Camera
    //{
    //    public static Vector2 PixelSize;
    //    public static bool Calibrate()
    //    {
    //        PixelSize = new Vector2(1.0f / Screen.width, 1.0f / Screen.height);
    //        return true;
    //    }
    //    public static Rect DrawPos(Rect ObjectRect)
    //    {
    //        return new Rect((ObjectRect.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - ObjectRect.height * 0.5f) * PixelSize.y, (ObjectRect.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - ObjectRect.height * 0.5f) * PixelSize.y);
    //    }
    //}
    public class D2Camera // 2d Camera
    {
        public static Vector2 PixelSize;
        static Vector2 CameraPos;
        public static Vector2 GetPos()
        {
            return CameraPos;
        }
        public static bool Calibrate()
        {
            PixelSize = new Vector2(1.0f / Screen.width, 1.0f / Screen.height);
            return true;
        }
        public static bool Update(Vector2 PlayerPos)
        {
            CameraPos = PlayerPos - new Vector2(Screen.width, Screen.height) * 0.5f;
            return true;
        }
        public static Vector2 GetPosRel(Rect ObjectRect)
        {
            return new Vector2(ObjectRect.x - CameraPos.x, ObjectRect.y - CameraPos.y);
        }
        public static Vector2 GetPosRel(Vector2 ObjectVector)
        {
            return new Vector2(ObjectVector.x - CameraPos.x, ObjectVector.y - CameraPos.y);
        }
        public static Rect rGetPosRel(Rect ObjectRect)
        {
            return new Rect(ObjectRect.x - CameraPos.x, ObjectRect.y - CameraPos.y, ObjectRect.width, ObjectRect.height);
        }
        public static Vector2 GetPosToPixel(Vector2 ObjectVector)
        {
            return new Vector2(ObjectVector.x * PixelSize.x, ObjectVector.y * PixelSize.y);
        }
        public static Vector2 GetPixelToPos(Vector2 ObjectVector)
        {
            return new Vector2(ObjectVector.x * Screen.width, ObjectVector.y * Screen.height);
        }
        public static Rect DrawPos(Rect ObjectRect)
        {
            return new Rect((ObjectRect.x - ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y - ObjectRect.height * 0.5f) * PixelSize.y, (ObjectRect.x + ObjectRect.width * 0.5f) * PixelSize.x, (ObjectRect.y + ObjectRect.height * 0.5f) * PixelSize.y);
        }
    }
    public class Random
    {
        private static int m_iNumber = 0;
        public static void SetSeed(int a_iNumber)
        {
            m_iNumber = a_iNumber;
        }
        public static int GetRandom(int low, int high)
        {
            m_iNumber = (m_iNumber * (int)22695477 + (int)1) % (int)0x7FFFFFFF;
            if (m_iNumber < 0) // check to see if its negetive if it is make it possitive
                return ((m_iNumber * -1) % (high + 1 - low)) + low;
            return (m_iNumber % (high + 1 - low)) + low; // we h - l = total range then you add the low to put it into perspective
        }
    }

    public class Math
    {
        public static float Sqr(float z)
        {
            return z * z;
        }
        public static float Sqrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.tmp = 0;
            u.f = z;
            u.tmp -= 1 << 23; /* Subtract 2^m. */
            u.tmp >>= 1; /* Divide by 2. */
            u.tmp += 1 << 29; /* Add ((b + 1) / 2) * 2^m. */
            return u.f;
        }
        [StructLayout(LayoutKind.Explicit)]
        private struct FloatIntUnion
        {
            [FieldOffset(0)]
            public float f;

            [FieldOffset(0)]
            public int tmp;
        }
    }
    public class SaveLoad
    {
        public static string Load()
        {
#if !UNITY_WEB 
            FileInfo aFile = new FileInfo(Application.persistentDataPath + "\\Settings.idf");
#endif
            string Data = "";
#if !UNITY_WEB
            if (aFile.Exists)
            {
                StreamReader r = aFile.OpenText();
                Data = r.ReadToEnd();
                r.Close();
            }
#endif
            return Data;
        }
        public static bool Save(string Data)
        {
#if !UNITY_WEB
            FileInfo aFile = new FileInfo(Application.persistentDataPath + "\\Settings.idf");
#endif
#if !UNITY_WEB
            StreamWriter w;
            if (aFile.Exists)
            {
                aFile.Delete();
            }
            w = aFile.CreateText();
            w.Write(Data);
            w.Close();
#endif
            return true;
        }
    }
    public class Shapes
    {
        public static void CreateLine(Rect aRect, Color aColor = new Color())
        {
            GL.Begin(GL.LINES);
            GL.Color(aColor);
            GL.TexCoord2(0, 0);
            GL.Vertex3(aRect.x, aRect.y, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 1);
            GL.Vertex3(aRect.width, aRect.height, 0.1F);
        }
        public static void CreateLine(Vector2 Pos1, Vector2 Pos2, Color aColor1 = new Color(), Color aColor2 = new Color())
        {
            GL.Begin(GL.LINES);
            GL.Color(aColor1);
            GL.TexCoord2(0, 0);
            GL.Vertex3(Pos1.x * D2Camera.PixelSize.x, Pos1.y * D2Camera.PixelSize.y, 0.1F);
            GL.Color(aColor2);
            GL.TexCoord2(1, 1);
            GL.Vertex3(Pos2.x * D2Camera.PixelSize.x, Pos2.y * D2Camera.PixelSize.y, 0.1F);
        }
        public static void CreateBox(Rect aRect, Color aColor = new Color())
        {
            GL.Begin(GL.QUADS);
            GL.Color(aColor);
            GL.TexCoord2(0, 0);
            GL.Vertex3(aRect.x, aRect.y, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(0, 1);
            GL.Vertex3(aRect.x, aRect.height, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 1);
            GL.Vertex3(aRect.width, aRect.height, 0.1F);
            GL.Color(aColor);
            GL.TexCoord2(1, 0);
            GL.Vertex3(aRect.width, aRect.y, 0.1F);
        }

    }
    public class Collision
    {
        public static bool AABBvsCircle(AABB rect, AABB circle)
        {
            Vector2 circleDistance = new Vector2();
            float cornerDistance_sq;
            circleDistance.x = Mathf.Abs((circle.Min.x + circle.Max.x) * 0.5f - (rect.Min.x + rect.Max.x) * 0.5f);
            circleDistance.y = Mathf.Abs((circle.Min.y + circle.Max.y) * 0.5f - (rect.Min.y + rect.Max.y) * 0.5f);
            if (circleDistance.x > ((rect.Max.x - rect.Min.x) * 0.5f + (circle.Max.x - circle.Min.x) * 0.5f))
            {
                return false;
            }
            if (circleDistance.y > ((rect.Max.y - rect.Min.y) * 0.5f + (circle.Max.y - circle.Min.y) * 0.5f))
            {
                return false;
            }
            if (circleDistance.x <= (rect.Max.x - rect.Min.x) * 0.5f)
            {
                return true;
            }
            if (circleDistance.y <= (rect.Max.y - rect.Min.y) * 0.5f)
            {
                return true;
            }
            cornerDistance_sq = Mathf.Pow((circleDistance.x - (rect.Max.y - rect.Min.y) * 0.5f), 2) + Mathf.Pow((circleDistance.y - (rect.Max.y - rect.Min.y) * 0.5f), 2);
            return (cornerDistance_sq <= ((circle.Max.x - circle.Min.x) * 0.5f) * ((circle.Max.x - circle.Min.x) * 0.5f));
        }
        public static bool AABBvsAABB(AABB BoxA, AABB BoxB)
        {

            // Exit with no intersection if found separated along an axis
            if (BoxA.Max.x < BoxB.Min.x || BoxA.Min.x > BoxB.Max.x)
            {
                return false;
            }
            if (BoxA.Max.y < BoxB.Min.y || BoxA.Min.y > BoxB.Max.y)
            {
                return false;
            }
            return true;
        }
        public static bool CirclevsCircle(AABB BoxA, AABB BoxB)
        {
            if ((((BoxA.Min + BoxA.Max) * 0.5f) - ((BoxB.Min + BoxB.Max) * 0.5f)).sqrMagnitude >
                (IDrag.Math.Sqr(((BoxA.Max - BoxA.Min) * 0.5f).x) + IDrag.Math.Sqr(((BoxB.Max - BoxB.Min) * 0.5f).x)))
            {
                return false;
            }
            return true;
        }
    }
}