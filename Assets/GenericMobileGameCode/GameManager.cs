using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public delegate void VoidDelegate();


public static class GameManager {

    public static bool cameFromMainMenu = false;
    private const string MAINMENUSCENE = "MainMenu";

    internal static float GetSavedVolume() {
        return PlayerPrefs.GetFloat(musicKey);
    }

    internal static int GetPermanentCurrency() {
        return numberOfCurrency;
    }

    private const string GAMESCENE = "Game";

    private static int currentScore = 0;
    private static int currentCurrencyAmount = 0;
    public static VoidDelegate onScoreUpdated;

 
    private static int numberOfCurrency = 0;
    public static VoidDelegate onChangeCurrency;
    public static int addCounter = 0;
    public static int adMax = 4;


    public static void adCheck() {
        addCounter++;
        if (addCounter >= adMax) {
            addCounter = 0;
            Debug.Log("Playing Ad");
            AdManager.instance.PlayInterstitialAd();

        }
    }

    internal static void MuteAll() {
        SetVolume(0, false);
    }

    internal static void UnMuteAll() {
        SetVolume(PlayerPrefs.GetFloat(musicKey), false);
    }

    internal static int GetHighScore() {
        return highScore;
    }

    private static int highScore = 0;
    public static float musicVolume = 1f;
    private static float soundVolume = 1f;


    public static void ResetScore() {
        currentScore = 0;
        onScoreUpdated = null;
    }

    public static void AddScore(int amount) {
        currentScore += amount;
        onScoreUpdated?.Invoke();
    }

    public static void SetScore(int amount) {
        currentScore = amount;
        onScoreUpdated?.Invoke();
    }

    public static int GetScore() {
        return currentScore;
    }

    public static void AddCurrency(int amount) {
        currentCurrencyAmount += amount;
    }

    public static bool HighScoreCheck() {
        if (currentScore > highScore) {
            highScore = currentScore;
            PlayerPrefs.SetInt(highScoreKey, currentScore);
            return true;
        }
        return false;
    }

    internal static void SpendFish(int cost) {
        numberOfCurrency -= cost;
        PlayerPrefs.SetInt(currencyKey, numberOfCurrency);
        onChangeCurrency?.Invoke();
    }

    public static void PermanentlyAddFish() {
        numberOfCurrency += currentCurrencyAmount;
        currentCurrencyAmount = 0;
        PlayerPrefs.SetInt(currencyKey, numberOfCurrency);
    }

    public static void PermantlyAddTripleFish() {
        numberOfCurrency += currentCurrencyAmount * 3;
        currentCurrencyAmount = 0;
        PlayerPrefs.SetInt(currencyKey, numberOfCurrency);
    }


    internal static int GetFish() {
        return currentCurrencyAmount;
    }

    public static void GoToMainMenu() {
        if (!cameFromMainMenu) {
            adCheck();
        } else {
            cameFromMainMenu = false;
        }
        ResetScore();
        SceneManager.LoadScene(MAINMENUSCENE);
    }


    public static void GoToGameScreen() {
        if (!cameFromMainMenu) {
            adCheck();
        } else {
            cameFromMainMenu = false;
        }
        ResetScore();
        SceneManager.LoadScene(GAMESCENE);
    }

    public static bool initYet = false;


    private static int difficulty = 0;









   









    public static void init() {
        //PlayerPrefs.DeleteAll();
        if (initYet) return;


        initYet = true;


  


       
       


      
        if (!PlayerPrefs.HasKey(musicKey)) PlayerPrefs.SetFloat(musicKey, 1);
        musicVolume = PlayerPrefs.GetFloat(musicKey);
        soundVolume = PlayerPrefs.GetFloat(soundKey);
        highScore = PlayerPrefs.GetInt(highScoreKey);
        numberOfCurrency = PlayerPrefs.GetInt(currencyKey);
        SetVolume(musicVolume, false);
      

    

    }

    public static VoidDelegate onVolumeChanged;



    public static void SetVolume(float f, bool affectSave) {
        if (f > 1) {
            Debug.LogError("Volume must be less than 1"); return;
        }

        musicVolume = f;
        if (affectSave) {
            PlayerPrefs.SetFloat(musicKey, f);
        }
        // Debug.LogError("Here");
        onVolumeChanged?.Invoke();
    }



    private const string saveCheck = "Check";
    private const string musicKey = "Music", soundKey = "Sound";
    private const string currencyKey = "Currency", highScoreKey = "High Score";


}
