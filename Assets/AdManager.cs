using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string playStoreID = "3976059";

    public bool isTest = true;
    public static AdManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            InitAds();
        }
    }

    public void InitAds() {
        Advertisement.AddListener(this);
        Advertisement.Initialize(playStoreID, isTest);
    }


    public void OnUnityAdsDidError(string message) {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        
    }

    public void OnUnityAdsDidStart(string placementId) {
        
    }

    public void OnUnityAdsReady(string placementId) {
        
    }
}
