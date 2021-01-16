using UnityEngine;
using TMPro;

public class NumberText : MonoBehaviour {

    public TextMeshProUGUI text;

    public void SetNumber(int number) {
        text.SetText(number.ToString());
    }

}


