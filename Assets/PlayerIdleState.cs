using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State<ChessController> {

    List<Vector3Int> possiblePlayerMoves = new List<Vector3Int>();

    public override void Enter(ChessController obj) {
        possiblePlayerMoves = obj.PlayerController.GetPossibleMoves();
    }

    public override void Exit(ChessController obj) {
        obj.DecreaseSpawnInterval();
    }

    public override State<ChessController> Update(ChessController obj) {
        //Debug.Log("Here");
        if (obj.inputManager.inputTouch) {
            Vector3Int clickedPos = obj.map.WorldToCell(obj.cam.ScreenToWorldPoint(Input.mousePosition));
            //Debug.Log(clickedPos);
            //Debug.Break();
            if (possiblePlayerMoves.Contains(clickedPos)) {
                obj.PlayerController.InstantMove(clickedPos);
                return new EnemyTurn();
            } else {
                obj.PlayErrorSound();
            }
        }

        return this; 

    }
}


