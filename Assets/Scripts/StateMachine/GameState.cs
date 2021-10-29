using UnityEngine;

public class GameState : State
{

    private bool _isNewGame;

    public GameState(bool isNewGame)
    {
        _isNewGame = isNewGame;
    }

    public override void EnterState()
    {
        base.EnterState();
        
        owner.GameView.OnPauseClicked += PauseClicked;
        owner.Player.OnFatalHit += FinishClicked;

        Time.timeScale = 1;
        owner.seaScrollingPlane.IsScrolling = true;
        if (_isNewGame)
        {
            ResetGame();
        }
        owner.GameView.Show();
    }

    private void ResetGame()
    {
        owner.Player.Reset();
        owner.seaScrollingPlane.Reset();
    }

    public override void ExitState()
    {
        owner.GameView.Hide();
        owner.GameView.OnPauseClicked -= PauseClicked;
        owner.Player.OnFatalHit -= FinishClicked;
        owner.seaScrollingPlane.IsScrolling = false;
        Time.timeScale = 0;
        
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseClicked();
        }
    }

    private void PauseClicked()
    {
        owner.ChangeState(new PauseState());
    }

    private void FinishClicked()
    {
        owner.ChangeState(new GameOverState());
    }
    
    
}