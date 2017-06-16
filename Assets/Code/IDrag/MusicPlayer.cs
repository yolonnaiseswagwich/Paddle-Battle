using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    AudioSource a;
    AudioClip b;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start ()
    {
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        a = GetComponent<AudioSource>();
        a.clip = SoundLib.GetSound(SoundLib.Menu);
        a.volume = GameGlobals.MusicVolume * 0.01f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (a.volume != GameGlobals.MusicVolume * 0.01f)
        {
            a.volume = GameGlobals.MusicVolume * 0.01f;
        }
        switch (GameInfo.GameType)
        {
            case GameInfo.Single:
                a.loop = true;
                b = SoundLib.GetSound(SoundLib.Menu);
                if (a.clip.name != b.name)
                {
                    a.clip = b;
                    a.Play();
                }
                if (GameGlobals.Paused && a.isPlaying)
                {
                    a.Pause();
                }
                else if (!GameGlobals.Paused && !a.isPlaying)
                {
                    a.UnPause();
                }
                break;
            case GameInfo.Local:
                a.loop = true;
                b = SoundLib.GetSound(SoundLib.Menu);
                if (a.clip.name != b.name)
                {
                    a.clip = b;
                    a.Play();
                }
                if (GameGlobals.Paused && a.isPlaying)
                {
                    a.Pause();
                }
                else if (!GameGlobals.Paused && !a.isPlaying)
                {
                    a.UnPause();
                }
                break;
            case GameInfo.Multi:
                a.loop = true;
                b = SoundLib.GetSound(SoundLib.Menu);
                if (a.clip.name != b.name)
                {
                    a.clip = b;
                    a.Play();
                }
                if (GameGlobals.Paused && a.isPlaying)
                {
                    a.Pause();
                }
                else if (!GameGlobals.Paused && !a.isPlaying)
                {
                    a.UnPause();
                }
                break;
            case GameInfo.Menu:
                a.loop = true;
                b = SoundLib.GetSound(SoundLib.Menu);
                if (a.clip.name != b.name)
                {
                    a.clip = b;
                    a.Play();
                }
                break;
            case GameInfo.Splash:
                if (Time.timeSinceLevelLoad > 0.0f)
                {
                    a.loop = false;
                    b = SoundLib.GetSound(SoundLib.Splash);
                    if (a.clip.name != b.name)
                    {
                        a.clip = b;
                        a.Play();
                    }
                }
                break;
        }
	}
}
