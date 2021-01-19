using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener {
    private string playstoreid = "3975863";
    private string appstoreid = "3975862";

    private string interstitialad = "video";
    private string rewardVideoAd = "rewardedVideo";

    public bool isTargetPlayStore = true;
    public bool isTestAd = true;

    public static AdManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            InitializeAdvertising();
        }
    }




    private void InitializeAdvertising() {
        if (isTargetPlayStore) {
            Advertisement.Initialize(playstoreid, isTestAd);
        } else {
            Advertisement.Initialize(appstoreid, isTestAd);
        }
        Advertisement.AddListener(this);
    }

    public void PlayInterstitialAd() {
        if (!Advertisement.IsReady(interstitialad)) { return; }
        Advertisement.Show(interstitialad);
    }

    public void PlayVideoRewardAd() {
        if (!Advertisement.IsReady(interstitialad)) { return; }
        Advertisement.Show(rewardVideoAd);
    }

    public void OnUnityAdsReady(string placementId) {

    }

    public void OnUnityAdsDidError(string message) {
        GameManager.UnMuteAll();
    }

    public void OnUnityAdsDidStart(string placementId) {
        GameManager.MuteAll();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        GameManager.UnMuteAll();
    }
}
