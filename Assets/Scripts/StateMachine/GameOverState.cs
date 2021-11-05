public class GameOverState : State
{
    public override void EnterState()
    {
        base.EnterState();
        owner.GameOverView.OnRetryClicked += RetryClicked;
        owner.GameOverView.OnMenuClicked += MenuClicked;
        
        owner.GameOverView.FillRecap(owner.Player);
        owner.Player.Save();

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
        owner.ChangeState(new GameState(true));
    }
    private void MenuClicked()
    {
        owner.ChangeState(new MenuState());
    }

}