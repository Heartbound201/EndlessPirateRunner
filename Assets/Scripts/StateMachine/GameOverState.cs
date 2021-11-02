public class GameOverState : State
{
    public override void EnterState()
    {
        base.EnterState();
        owner.GameOverView.OnRetryClicked += RetryClicked;
        owner.GameOverView.OnShopClicked += QuitClicked;
        
        owner.GameOverView.FillRecap(owner.Player);
        owner.Player.Save();

        owner.GameOverView.Show();
    }

    public override void ExitState()
    {
        owner.GameOverView.Hide();
        owner.GameOverView.OnRetryClicked -= RetryClicked;
        owner.GameOverView.OnShopClicked -= QuitClicked;

        base.ExitState();
    }

    private void RetryClicked()
    {
        owner.ChangeState(new GameState(true));
    }
    private void QuitClicked()
    {
        owner.ChangeState(new MenuState());
    }

}