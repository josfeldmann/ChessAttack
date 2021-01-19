using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : ChessPiece {
    public override Vector3Int GetMoveIfCantAttack(List<Vector3Int> moves) {
        List<Vector3Int> l = moves;

        List<Vector3Int> results = new List<Vector3Int>();
        Vector3Int playerPos = controller.PlayerController.GetVectorPos();
        foreach (Vector3Int v in l) {
            if (Mathf.Abs(v.x - playerPos.x) == Mathf.Abs(v.y - playerPos.y)) {
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
        GetVectorInDirection(this, new Vector3Int(1,1,0), moves);
        GetVectorInDirection(this,new Vector3Int(1,-1,0), moves);
        GetVectorInDirection(this, new Vector3Int(-1,1,0), moves);
        GetVectorInDirection(this, new Vector3Int(-1,-1,0), moves);

        return moves;

    }

    public override void Move() {
        DefaultMove();
    }
}
