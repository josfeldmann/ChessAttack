using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece {
    public override Vector3Int GetMoveIfCantAttack(List<Vector3Int> moves) {
        if (moves.Count == 0) return GetVectorPos();
        return moves.PickRandom();
    }

    public override List<Vector3Int> GetPossibleAttacks() {
        List<Vector3Int> possibleAttacks = new List<Vector3Int>();
        int testx = x + direction.x;
        int testy = y + direction.y;
        
        if (direction.x != 0) {
            if (testx >= 0 && testx < controller.boardWidth) {
                if (testy > 0) possibleAttacks.Add(new Vector3Int(testx, testy - 1, 0));
                if (testy < controller.boardHeight-1) possibleAttacks.Add(new Vector3Int(testx, testy + 1, 0));
            }
        }

        if (direction.y != 0) {
            if (testy >= 0 && testy < controller.boardHeight) {
                if (testx > 0) possibleAttacks.Add(new Vector3Int(testx - 1, testy, 0));
                if (testx < controller.boardWidth - 1) possibleAttacks.Add(new Vector3Int(testx + 1, testy, 0));
            }
        }

        return possibleAttacks;

    }

    public override List<Vector3Int> GetPossibleMoves() {
        int testx = x + direction.x;
        int testy = y + direction.y;
        if (testx < 0 || testy < 0 || testx == controller.boardWidth || testy == controller.boardHeight) {
            MoveOffGrid(new Vector3Int(testx, testy, 0));
            return new List<Vector3Int>();
        } else if (controller.chessGrid[testx, testy]) {
            return new List<Vector3Int>();
        } else {
            return new List<Vector3Int>() { new Vector3Int(testx, testy, 0) };
        }
    }

    public override void Move() {
        DefaultMove();
    }

    
}
