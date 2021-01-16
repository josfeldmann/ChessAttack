public class EnemyTurn : State<ChessController> {
    public override void Enter(ChessController obj) {

    }

    public override void Exit(ChessController obj) {
    }

    public override State<ChessController> Update(ChessController obj) {
        return new PlayerIdleState();
    }
}


