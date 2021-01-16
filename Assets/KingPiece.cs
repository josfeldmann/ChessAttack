using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece {
    public override void ExecuteMove() {
        throw new System.NotImplementedException();
    }

    public override List<Vector3Int> GetPossibleMoves() {
        currentMoves = new List<Vector3Int>();
        if (x > 0) {
            currentMoves.Add(new Vector3Int(x - 1, y, 0));
        }
        if (y > 0)  {
            currentMoves.Add(new Vector3Int(x, y - 1, 0));
        }
        if (x < controller.boardWidth-1) {
            currentMoves.Add(new Vector3Int(x + 1, y, 0));
        }
        if (y < controller.boardWidth-1) {
            currentMoves.Add(new Vector3Int(x, y + 1, 0));
        }

        return currentMoves;
    }

    public override Vector3Int PickMoveAI(ChessPiece target) {
        if (currentMoves == null || currentMoves.Count == 0) {
            return new Vector3Int(x, y,0);
        }
        return new Vector3Int(x, y, 0);

    }
}


