using UnityEngine;
using UnityEngine.Tilemaps;

public class ChessController : MonoBehaviour {


    
    public Vector3Int minStartPos = new Vector3Int(2,2,0), maxStartPos = new Vector3Int(5,5,0);
    public InputManager inputManager;
    public ChessPiece PlayerController;
    public Tilemap map;
    public Camera cam;
    public int boardWidth = 8, boardHeight = 8;

    public NumberText currentSpawnText;
    public int StartingSpawnInterval = 5;
    public int SpawnInterval;

    private int currentSpawnInterval = 0;

    [Header("UI Elements")]
    public GameObject StartText;
    


    public ChessPiece[,] chessGrid = new ChessPiece[8, 8];

    public State<ChessController> currentState;

    private void Awake() {
        Vector3Int startPos = new Vector3Int(Random.Range(minStartPos.x, maxStartPos.x), Random.Range(minStartPos.y, maxStartPos.y), 0);
        chessGrid = new ChessPiece[boardWidth, boardHeight];
        PlayerController.init(startPos, this);
        currentState = new PlayerIdleState();
        currentState.Enter(this);
        currentSpawnInterval = StartingSpawnInterval;
        currentSpawnText.SetNumber(currentSpawnInterval);
    }

    public void DecreaseSpawnInterval() {
        currentSpawnInterval--;
        currentSpawnText.SetNumber(currentSpawnInterval);
    }

    private void Update() {
        currentState = FSM<ChessController>.Update(currentState, this);
    }

    public void PlayErrorSound() {

    }



}


