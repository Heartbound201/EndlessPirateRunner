public class GameOverState : State
{
    public override void EnterState()
    {
        base.EnterState();
        owner.GameOverView.OnRetryClicked += RetryClicked;
        owner.GameOverView.OnMenuClicked += MenuClicked;
        
        owner.GameOverView.FillRecap(owner.Player);
        SaveManager.Instance.Save();

        owner.GameOverView.Show();
    }

    public override void ExitState()
    {
        owner.GameOverView.Hide();
        owner.GameOverView.OnRetryClicked -= RetryClicked;
        owner.GameOverView.OnMenuClicked -= MenuClicked;

        base.ExitState();
    }

    private void RetryClicked()
    {
        owner.ChangeState(new GameState());
    }
    private void MenuClicked()
    {
        owner.ChangeState(new MenuState());
    }

}