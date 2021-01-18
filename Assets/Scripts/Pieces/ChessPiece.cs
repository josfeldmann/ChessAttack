using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class ChessPiece : MonoBehaviour
{

    public float moveSpeed = 5;
    public ChessPieceBase pieceBase;
    public int enemyLayer;
    [HideInInspector]
    public ChessController controller;
    public int x, y;
    public Vector3Int targetPos;
    [HideInInspector]
    public Vector3Int direction;    

    public bool moveOffBoardTarget;


    protected Vector3 prevPosition;
    protected Vector3 targetPosition;
    public bool isMoving;
    

    protected List<Vector3Int> currentMoves = new List<Vector3Int>();
    public void init(Vector3Int position, ChessController controller, Vector3Int direction, int layer) {
        this.controller = controller;
        this.direction = direction;
        InstantMove(position);
        enemyLayer = layer;
    }

    private void SetGridToTargetPos() {
        controller.chessGrid[x, y] = null;
        x = targetPos.x;
        y = targetPos.y;
        controller.chessGrid[x, y] = this;
    }

    public void InstantMove(Vector3Int pos) {
        targetPos = pos;
        SetGridToTargetPos();
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public Vector3Int GetVectorPos() {
        return new Vector3Int(x, y, 0);
    }


    Vector3 offboardpos;

    public void MoveOffGrid(Vector3Int pos) {
        moveOffBoardTarget = true;
        offboardpos = pos;
        StartCoroutine(MoveOffBoardNum());
    }

    IEnumerator MoveOffBoardNum() {
        while (transform.position != offboardpos) {
            transform.position = Vector3.MoveTowards(transform.position, offboardpos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        Destroy(gameObject);
    }

    public abstract Vector3Int GetMoveIfCantAttack(List<Vector3Int> moves);

    public abstract List<Vector3Int> GetPossibleMoves();
    public abstract List<Vector3Int> GetPossibleAttacks();
     

    public void StartMoving(Vector3Int pos) {
        isMoving = true;
        targetPos = pos;
        SetGridToTargetPos();
        prevPosition = transform.position;
        targetPosition = new Vector3(pos.x, pos.y);

    }

    public abstract void Move();

    public void Move(Vector2Int destination) {
        x = destination.x;
        y = destination.y;
    }


    public void DefaultMove() {
        if (transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        } else {
            isMoving = false;
            InstantMove(targetPos);
        }
    }



    public void GetVectorInDirection(ChessPiece piece, Vector3Int direction, List<Vector3Int> addHere) {

        int testx = x + direction.x;
        int testy = y + direction.y;
        bool haventHitPiece = true;

        while (testx > 0 && testx < piece.controller.boardWidth && testy > 0 && testy < piece.controller.boardHeight && haventHitPiece) {
            if (piece.controller.chessGrid[testx, testy]) haventHitPiece = false;
            print("here");
            addHere.Add(new Vector3Int(testx, testy, 0));
            testx += direction.x;
            testy += direction.y;
        }

    }


}



public abstract class State<T> {


    public abstract void Enter(T obj);

    public abstract void Exit(T obj);

    public abstract State<T> Update(T obj);


}


