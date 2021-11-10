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
        
        Time.timeScale = 1;
        ScrollingPlane.Instance.Enabled = true;
        EntityGenerator.Instance.Enabled = true;
        if (_isNewGame)
        {
            ResetGame();
        }
        owner.GameView.OnPauseClicked += PauseClicked;
        owner.Player.playerShip.OnFatalHit += FinishClicked;

        owner.GameView.UpdateGold();
        owner.GameView.UpdateDistance();
        owner.GameView.Show();
    }

    private void ResetGame()
    {
        owner.Player.Reset();
        ScrollingPlane.Instance.Reset();
    }

    public override void ExitState()
    {
        owner.GameView.Hide();
        owner.GameView.OnPauseClicked -= PauseClicked;
        owner.Player.playerShip.OnFatalHit -= FinishClicked;
        ScrollingPlane.Instance.Enabled = false;
        EntityGenerator.Instance.Enabled = false;
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