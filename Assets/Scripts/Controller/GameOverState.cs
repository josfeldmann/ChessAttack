public class GameOverState : State<ChessController> {
    public override void Enter(ChessController obj) {
        obj.GameOver();
    }

    public override void Exit(ChessController obj) {
        
    }

    public override State<ChessController> Update(ChessController obj) {
        return this;
    }
}


