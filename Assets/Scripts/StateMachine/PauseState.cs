using UnityEngine;

public class PauseState : State
{
    public override void EnterState()
    {
        base.EnterState();
        
        owner.PauseView.OnResumeClicked += ResumeClicked;
        owner.PauseView.OnMenuClicked += MenuClicked;
        
        owner.PauseView.Show();
    }

    public override void ExitState()
    {
        owner.PauseView.Hide();
        owner.PauseView.OnResumeClicked -= ResumeClicked;
        owner.PauseView.OnMenuClicked -= MenuClicked;
        
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeClicked();
        }
    }

    private void ResumeClicked()
    {
        owner.ChangeState(new GameState());
    }
    private void MenuClicked()
    {
        owner.ChangeState(new MenuState());
    }

}