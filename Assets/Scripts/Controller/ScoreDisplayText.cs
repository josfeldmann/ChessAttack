using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayText : MonoBehaviour {

    public string prefix = "x ";
    public TextMeshProUGUI scoreText;
    public Image pieceImage;
    private ChessPieceBase pieceBase;
    private ChessController controller;

    public void init(ChessController cotnroller, ChessPieceBase pBase) {
        this.controller = cotnroller;
        this.pieceBase = pBase;
        pieceImage.sprite = pieceBase.spriteImage;
        UpdateValue();
    }

    public void UpdateValue() {
        scoreText.text = prefix + controller.scoreKeeper[pieceBase].ToString();
    }


} 


