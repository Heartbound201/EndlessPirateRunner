using UnityEngine;

public class GameState : State
{

    public override void EnterState()
    {
        base.EnterState();
        
        Time.timeScale = 1;
        // ScrollingPlane.Instance.Enabled = true;
        EntityGenerator.Instance.Enabled = true;
        WeatherGenerator.Instance.Enabled = true;
        
        owner.GameView.OnPauseClicked += PauseClicked;
        owner.Player.playerShip.OnFatalHit += FinishClicked;

        owner.MoveJoystick.SetActive(true);
        owner.ShootJoystick.SetActive(owner.Player.playerShip.cannonSystem);
        owner.GameView.UpdateGold();
        owner.GameView.UpdateScore();
        owner.GameView.UpdateLives();
        owner.GameView.Show();
        
        owner.Player.playerShip.Sail();
    }

    public override void ExitState()
    {
        owner.GameView.Hide();
        owner.MoveJoystick.SetActive(false);
        owner.ShootJoystick.SetActive(false);
        owner.GameView.OnPauseClicked -= PauseClicked;
        owner.Player.playerShip.OnFatalHit -= FinishClicked;
        // ScrollingPlane.Instance.Enabled = false;
        EntityGenerator.Instance.Enabled = false;
        WeatherGenerator.Instance.Enabled = false;
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