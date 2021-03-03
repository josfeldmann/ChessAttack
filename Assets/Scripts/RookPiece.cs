using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece {
    public override Vector3Int GetMoveIfCantAttack(List<Vector3Int> moves) {
        List<Vector3Int> l = moves;

        List<Vector3Int> results = new List<Vector3Int>();
        foreach (Vector3Int v in l) {
            if (v.x == controller.PlayerController.x || v.y == controller.PlayerController.y) {
                results.Add(v);
            }
        }

        if (results.Count == 0) {
            if (l.Count == 0) return new Vector3Int(x, y, 0);
            return l.PickRandom();
        } else {
            return results.PickRandom();
        }
    }

    public override List<Vector3Int> GetPossibleAttacks() {
        return GetPossibleMoves();

    }

    public override List<Vector3Int> GetPossibleMoves() {

        List<Vector3Int> moves = new List<Vector3Int>();
        GetVectorInDirection(this, Vector3Int.right, moves, false);
        GetVectorInDirection(this, Vector3Int.left, moves, false);
        GetVectorInDirection(this, Vector3Int.down, moves, false);
        GetVectorInDirection(this, Vector3Int.up, moves, false);

        return moves;

    }

    public override void Move() {
        DefaultMove();
    }


  


}
