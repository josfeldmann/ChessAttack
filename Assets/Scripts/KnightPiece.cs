using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : ChessPiece {
    public override Vector3Int GetMoveIfCantAttack(List<Vector3Int> moves) {
        return moves.PickRandom();
    }

    public override List<Vector3Int> GetPossibleAttacks() {
        return GetPossibleMoves();
    }



    public override List<Vector3Int> GetPossibleMoves() {
        List<Vector3Int> moves = new List<Vector3Int>();
        GetKnightMoves(moves);

        return moves;
    }

    public void GetKnightMoves(List<Vector3Int> moves) {
        GetVectorInDirection(this, new Vector3Int(2, 1, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(2, -1, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(-2, 1, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(-2, -1, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(1, 2, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(1, -2, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(-1, 2, 0), moves, true);
        GetVectorInDirection(this, new Vector3Int(-1, -2, 0), moves, true);
    }


    public override void Move() {
        DefaultMove();
    }


    
}
