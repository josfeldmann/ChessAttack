using UnityEngine;

public class GameManagerProxy : MonoBehaviour {

    public bool clearOnAwake = true;
    private void Awake() {
        if (clearOnAwake) PlayerPrefs.DeleteAll();
        GameManager.init();
    }
    public void GoToMainMenu() {
        GameManager.GoToMainMenu();
    }

    public void GoToGame() {
        GameManager.GoToGameScreen();
    }

}
