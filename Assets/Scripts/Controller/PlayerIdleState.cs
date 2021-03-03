using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State<ChessController> {

    List<Vector3Int> possiblePlayerMoves = new List<Vector3Int>();

    public override void Enter(ChessController obj) {
        obj.IncreaseTurnCounter();
        possiblePlayerMoves = obj.PlayerController.GetPossibleMoves();
    }

    public override void Exit(ChessController obj) {
        
    }

    private ChessPiece capturedPiece = null;

    public override State<ChessController> Update(ChessController obj) {
        
        if (obj.PlayerController.isMoving) {
            obj.PlayerController.Move();
            if (!obj.PlayerController.isMoving) {
                if (capturedPiece) obj.CapturePiece(capturedPiece);
                return new EnemyTurn();
            }
            return this;
        }
        
        
        if (obj.inputManager.inputTouch) {
            Vector3Int clickedPos = obj.map.WorldToCell(obj.cam.ScreenToWorldPoint(Input.mousePosition));
      
            if (possiblePlayerMoves.Contains(clickedPos)) {
                capturedPiece = obj.chessGrid[clickedPos.x, clickedPos.y];
                obj.PlayerController.StartMoving(clickedPos);
                


                return this;
            } else {
                obj.PlayErrorSound();
            }
        }

        return this; 

    }
}


