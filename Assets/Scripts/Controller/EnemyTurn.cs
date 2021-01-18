using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurn : State<ChessController> {

    bool turnDone = true;
    bool playerKilled = false;

    List<ChessPiece> killingPieces = new List<ChessPiece>();

    public override void Enter(ChessController obj) {
        if (obj.enemyPieces.Count == 0) {
            turnDone = true;
            return;
        }

        List<ChessPiece> removeThisTurn = new List<ChessPiece>();
        foreach (ChessPiece c in obj.enemyPieces) {


            List<Vector3Int> possibleAttacks = c.GetPossibleAttacks();
            possibleAttacks.PrettyPrint();
            foreach (Vector3Int vec in possibleAttacks) {
                if (obj.chessGrid[vec.x, vec.y] && obj.chessGrid[vec.x, vec.y].gameObject.layer == c.enemyLayer) {
                    playerKilled = true;
                    killingPieces.Add(c);
                    c.StartMoving(vec);
                }
            }

            if (!playerKilled) {
                List<Vector3Int> possibleMoves = c.GetPossibleMoves();
                if (c.moveOffBoardTarget) {
                    removeThisTurn.Add(c);
                    obj.chessGrid[c.x, c.y] = null;
                } else if (possibleMoves.Count > 0) {
                    possibleMoves.RemoveAll(s => obj.chessGrid[s.x, s.y] != null);
                    Vector3Int move = c.GetMoveIfCantAttack(possibleMoves);
                    turnDone = false;
                    c.StartMoving(move);
                } else {

                }
            }
           


        }

        foreach (ChessPiece c in removeThisTurn) {
            obj.enemyPieces.Remove(c);
        }

    }

    public override void Exit(ChessController obj) {
        if (!obj.isGameOver) obj.DecreaseSpawnInterval();
    }


    bool movecheck = true;

    public override State<ChessController> Update(ChessController obj) {
     

        if (playerKilled) {

            movecheck = true;

            foreach (ChessPiece c in obj.enemyPieces) {
                if (c.isMoving) {
                    // Debug.Log("here" + Time.time);
                    movecheck = false;
                    c.Move();
                }
            }

            if (movecheck)
            return new GameOverState();

            return this;

        } else {
            if (turnDone) return new PlayerIdleState();


            movecheck = true;

            foreach (ChessPiece c in obj.enemyPieces) {
                if (c.isMoving) {
                    // Debug.Log("here" + Time.time);
                    movecheck = false;
                    c.Move();
                }
            }

            turnDone = movecheck;

            return this;


        }

      
    }
}


