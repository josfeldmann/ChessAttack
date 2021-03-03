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
    protected Vector3 Curve1;
    protected Vector3 Curve2;
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
        Curve1 = new Vector3(0, 0.66f, 0);
        Curve2 = new Vector3(0, 0.66f, 0);


    }

    public abstract void Move();

    public void Move(Vector2Int destination) {
        x = destination.x;
        y = destination.y;
    }

    private Vector3 GetPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) {
        float cx = 3 * (p1.x - p0.x);
        float cy = 3 * (p1.y - p0.y);
        float bx = 3 * (p2.x - p1.x) - cx;
        float by = 3 * (p2.y - p1.y) - cy;
        float ax = p3.x - p0.x - cx - bx;
        float ay = p3.y - p0.y - cy - by;
        float Cube = t * t * t;
        float Square = t * t;

        float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.x;
        float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.y;

        return new Vector3(resX, resY, 0);
    }

    private float timer = 1;

    public void BezierMove() {
        if (timer < 1) {
            timer += Time.deltaTime;
            if (timer > 1) timer = 1;
            transform.position = GetPoint(timer, prevPosition, Curve1, Curve2, targetPosition);
        } else {
            transform.position = targetPosition;
            InstantMove(targetPos);
        }   
        }


    public void DefaultMove() {
        if (transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        } else {
            isMoving = false;
            InstantMove(targetPos);
        }
    }



    public void GetVectorInDirection(ChessPiece piece, Vector3Int direction, List<Vector3Int> addHere, bool doneAfterOne) {

        int testx = x + direction.x;
        int testy = y + direction.y;
        bool haventHitPiece = true;

        while (testx > 0 && testx < piece.controller.boardWidth && testy > 0 && testy < piece.controller.boardHeight && haventHitPiece) {
            if (piece.controller.chessGrid[testx, testy]) haventHitPiece = false;
            print("here");
            addHere.Add(new Vector3Int(testx, testy, 0));
            testx += direction.x;
            testy += direction.y;
            if (doneAfterOne) return;
        }

    }


}



public abstract class State<T> {


    public abstract void Enter(T obj);

    public abstract void Exit(T obj);

    public abstract State<T> Update(T obj);


}


