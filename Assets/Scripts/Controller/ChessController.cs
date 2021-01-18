using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class ChessController : MonoBehaviour {


    public Dictionary<ChessPieceBase, int> scoreKeeper;

    
    public Vector3Int minStartPos = new Vector3Int(2,2,0), maxStartPos = new Vector3Int(5,5,0);
    public InputManager inputManager;
    public ChessPiece PlayerController;
    public Tilemap map;
    public Camera cam;
    public List<ChessPiece> enemyPieces = new List<ChessPiece>();
    public int boardWidth = 8, boardHeight = 8;

    [Header("Layers")]
    public int enemyLayer = 7;
    public int playerLayer = 8;

    [Header("Spawning")]
    public List<ChessPiece> piecePrefabs = new List<ChessPiece>();
    public NumberText currentSpawnText;
    public int StartingSpawnInterval = 5;
    public int minSpawnInterval = 3;
    public int SpawnInterval;

    [Header("Difficulty")]
    public int difficultyincreaseInterval = 3;
    public int startingNumberPerSpawn = 1;
    public int maxNumberPerSpawn;

    private int currentSpawnNumber;
    private int difficultycounter;

    internal void CapturePiece(ChessPiece capturedPiece) {
        enemyPieces.Remove(capturedPiece);
        Destroy(capturedPiece.gameObject);
    }

    private int currentSpawnInterval = 0;

    [Header("UI Elements")]
    public Transform scorePrefabGroupingTransform;
    public ScoreDisplayText scorePrefab;
    public GameObject StartText;
    


    public ChessPiece[,] chessGrid = new ChessPiece[8, 8];

    public State<ChessController> currentState;

    private void Awake() {
        Vector3Int startPos = new Vector3Int(Random.Range(minStartPos.x, maxStartPos.x), Random.Range(minStartPos.y, maxStartPos.y), 0);
        chessGrid = new ChessPiece[boardWidth, boardHeight];
        PlayerController.init(startPos, this, Vector3Int.zero, enemyLayer);
        currentState = new PlayerIdleState();
        currentState.Enter(this);
        currentSpawnInterval = StartingSpawnInterval;
        currentSpawnText.SetNumber(currentSpawnInterval);
        currentSpawnNumber = startingNumberPerSpawn;
        difficultycounter = 0;
    }

    private List<ScoreDisplayText> scoresDisplayers = new List<ScoreDisplayText>();

    public void SetUpScoreMenu() {
        foreach (ChessPiece c in piecePrefabs) {
            scoreKeeper.Add(c.pieceBase, 0);
            ScoreDisplayText s = Instantiate(scorePrefab, scorePrefabGroupingTransform);

        }



    }

    public void SpawnNewChessPiece() {
        Vector3Int vec = pickSpawnPosition();
        ChessPiece p = Instantiate(piecePrefabs.PickRandom());
        Vector3Int direction = Vector3Int.zero;

        if (vec.x == 0) direction = Vector3Int.right;
        else if (vec.x == boardWidth - 1) direction = Vector3Int.left;
        else if (vec.y == 0) direction = Vector3Int.up;
        else if (vec.y == boardHeight - 1) direction = Vector3Int.down;
        else {
            Debug.LogError("Incorrect SpawnPosition: " + vec.ToString());
        }

        p.init(vec, this, direction, playerLayer);
        enemyPieces.Add(p);
    }

    public void DecreaseSpawnInterval() {
        currentSpawnInterval--;

        


        if (currentSpawnInterval == 0) {
            
            for (int i = 0; i < currentSpawnNumber; i++)
            SpawnNewChessPiece();
            currentSpawnInterval = SpawnInterval;

            difficultycounter++;
            if (difficultycounter == difficultyincreaseInterval) {
                difficultycounter = 0;
                if (currentSpawnNumber < maxNumberPerSpawn) currentSpawnNumber++;
                if (SpawnInterval > minSpawnInterval) SpawnInterval--;
            }

        }

        currentSpawnText.SetNumber(currentSpawnInterval);

    }


    public Vector3Int pickSpawnPosition() {
        List<Vector3Int> list = new List<Vector3Int>();
        for (int x =0 ; x < boardWidth; x++) {
            if (chessGrid[x, 0] == null) list.Add(new Vector3Int(x,0, 0));
            if (chessGrid[x, boardHeight - 1] == null) list.Add(new Vector3Int(x, boardHeight - 1, 0));
        }

        int bh = boardHeight - 1;
        int bw = boardWidth - 1;
        for (int y = 1; y < bh ; y++) {
            if (chessGrid[0, y] == null) list.Add(new Vector3Int(0, y, 0));
            if (chessGrid[bw, y] == null) list.Add(new Vector3Int(0, y, 0));
        }

        return list.PickRandom();
    }

    private void Update() {
        currentState = FSM<ChessController>.Update(currentState, this);
    }

    public void PlayErrorSound() {

    }

    public bool isGameOver = false;

    internal void GameOver() {
        isGameOver = true;
    }
}


