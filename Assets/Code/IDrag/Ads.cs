using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
public class Ads : MonoBehaviour {

    public float AdTimer = 0.0f;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        if (GameGlobals.AdsOff == 1) {
            Destroy(this);
        }
        Debug.Log(Time.timeSinceLevelLoad);
        AdTimer += Time.deltaTime;
        if (AdTimer > 180.0f && SceneManager.GetActiveScene().name != "SinglePlayer" && SceneManager.GetActiveScene().name != "Local" && Time.timeSinceLevelLoad > 1.0f)
        {
            ShowRewardedAd();
        }
    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
            Debug.Log("The ad was successfully shown.");

            AdTimer = 0;
            //YOUR CODE TO REWARD THE GAMER
            // Give coins etc.
            break;
            case ShowResult.Skipped:
            Debug.Log("The ad was skipped before reaching the end.");

            AdTimer = 0;
            break;
            case ShowResult.Failed:
            Debug.LogError("The ad failed to be shown.");

            AdTimer = 0;
            break;
        }
    }
}
