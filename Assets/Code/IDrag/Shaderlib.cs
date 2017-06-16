using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShaderLib {
    public const int Default = 0;
    public const int Julia = 1;
    public const int Mandelbrot = 2;
    public const int CellShader = 3;
    public const int TexColor = 4;
    public const int Blend = 5;
    public const int Circle = 6;
    public const int Blank = 7;
    public const int Neon = 8;
    public const int Planet = 9;
    public const int Spotlight = 10;
    public const int CircleForce = 11;
    public const int CircleTex = 12;
    public const int Overlay = 13;
    public const int DefaultAlpha = 14;
    public const int DefaultColorTex = 15;
    public const int InvColorTex = 16;
    public const int DefaultColorTexGray = 17;
    public const int OverlayNBG = 18;
    public const int NeonPlanet = 19;
    private static bool IsInit = false;
    private static Dictionary<int, Material> ShaderList = new Dictionary<int, Material>();
    public static bool Init()
    {
        if (!IsInit)
        {   //Temp to be reused as a loader/inputer into the list
            Shader TempShader;
            Material TempMaterial;
            //Default Shader
            TempShader = Resources.Load("Shaders/Default") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Default, TempMaterial);
            //Julia Shader
            TempShader = Resources.Load("Shaders/Julia") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Julia, TempMaterial);
            //Mandelbrot Shader
            TempShader = Resources.Load("Shaders/Mandelbrot") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Mandelbrot, TempMaterial);
            //Cell Shader
            TempShader = Resources.Load("Shaders/CellShader") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(CellShader, TempMaterial);
            //TexColor Shader
            TempShader = Resources.Load("Shaders/TexColor") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(TexColor, TempMaterial);
            //Blend Shader
            TempShader = Resources.Load("Shaders/Blend") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Blend, TempMaterial);
            //Circle Shader
            TempShader = Resources.Load("Shaders/Circle") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Circle, TempMaterial);
            //blank Shader
            TempShader = Resources.Load("Shaders/Blank") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Blank, TempMaterial);
            //Neon Shader
            TempShader = Resources.Load("Shaders/NeonShader") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Neon, TempMaterial);
            //Planet Shader
            TempShader = Resources.Load("Shaders/Planet") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Planet, TempMaterial);
            IsInit = true;
            //Spotlight Shader
            TempShader = Resources.Load("Shaders/Spotlight") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Spotlight, TempMaterial);
            //CircleForce Shader
            TempShader = Resources.Load("Shaders/CircleForce") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(CircleForce, TempMaterial);
            //CircleTex Shader
            TempShader = Resources.Load("Shaders/CircleTex") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(CircleTex, TempMaterial);
            //Overlay Shader
            TempShader = Resources.Load("Shaders/Overlay") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(Overlay, TempMaterial);
            IsInit = true;
            //DefaultAlpha Shader
            TempShader = Resources.Load("Shaders/DefaultAlpha") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(DefaultAlpha, TempMaterial);
            //DefaultColorTex Shader
            TempShader = Resources.Load("Shaders/DefaultColorTex") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(DefaultColorTex, TempMaterial);
            //InvColorTex Shader
            TempShader = Resources.Load("Shaders/InvColorTex") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(InvColorTex, TempMaterial);
            IsInit = true;
            //DefaultColorTexGray Shader
            TempShader = Resources.Load("Shaders/DefaultColorTexGray") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(DefaultColorTexGray, TempMaterial);
            IsInit = true;
            //OverlayNBG Shader
            TempShader = Resources.Load("Shaders/OverlayNBG") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(OverlayNBG, TempMaterial);
            //NeonPlanet Shader
            TempShader = Resources.Load("Shaders/NeonPlanet") as Shader;
            TempMaterial = new Material(TempShader);
            ShaderList.Add(NeonPlanet, TempMaterial);
        }
        return IsInit;
    }
    public static Material GetShader(int Key)
    {
        Material Temp;
        ShaderList.TryGetValue(Key, out Temp);
        return Temp;
    }
}
