public class SettingsState : State
{
    public override void EnterState()
    {
        base.EnterState();
        
        owner.SettingsView.OnMenuClicked += MenuClicked;
        owner.SettingsView.Show();
    }

    public override void ExitState()
    {
        owner.SettingsView.Hide();
        owner.SettingsView.OnMenuClicked -= MenuClicked;
        
        base.ExitState();
    }
    
    private void MenuClicked()
    {
        owner.ChangeState(new MenuState());
    }
}