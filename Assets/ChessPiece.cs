using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    protected ChessController controller;
    public int x, y;
    public Vector3Int targetPos;

    protected List<Vector3Int> currentMoves = new List<Vector3Int>();
    public void init(Vector3Int position, ChessController controller) {
        this.controller = controller;
        InstantMove(position);
    }

    public void InstantMove(Vector3Int pos) {
        controller.chessGrid[x, y] = null;
        x = pos.x;
        y = pos.y;
        controller.chessGrid[x, y] = this;
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public abstract List<Vector3Int> GetPossibleMoves();
    public abstract Vector3Int PickMoveAI(ChessPiece target);

    public abstract void ExecuteMove();

    public void Move(Vector2Int destination) {
        x = destination.x;
        y = destination.y;
    }


}



public abstract class State<T> {


    public abstract void Enter(T obj);

    public abstract void Exit(T obj);

    public abstract State<T> Update(T obj);


}


