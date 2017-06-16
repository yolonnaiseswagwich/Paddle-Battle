using UnityEngine;
using System.Collections;
public class TextChanger : MonoBehaviour {
    UnityEngine.UI.Text ThisText;
    int Type;
    void Start() {
        ThisText = GetComponent<UnityEngine.UI.Text>();
        ThisText.color = new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b);
        if (ThisText.name == "PE" || ThisText.name == "EE") {
            Type = 0;
        } else if (ThisText.name == "EP" || ThisText.name == "PP") {
            Type = 1;
        } else {
            Type = 2;
        }
    }
    // Update is called once per frame
    void Update() {
        switch (Type) {
            case 0:
                if (GameInfo.GameType == GameInfo.Single) {
                    ThisText.text = "Y:\n" + Score.m_iScore1.ToString();
                } else{
                ThisText.text = "P1:\n" + Score.m_iScore1.ToString();
            }
            break;
            case 1:
            if (GameInfo.GameType == GameInfo.Single)
            {
                ThisText.text = "E:\n" + Score.m_iScore2.ToString();
            }
            else
            {
                ThisText.text = "P2:\n" + Score.m_iScore2.ToString();
            }
            break;
            case 2:
            ThisText.text = "Time Left:\n" + ((int)(300.0f - Score.m_fGameTime)).ToString();
            break;
        }
    }
}
