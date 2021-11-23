using UnityEditor;
using UnityEngine;

public class MenuState : State
{
    public override void EnterState()
    {
        base.EnterState();

        owner.MenuView.OnStartClicked += StartClicked;
        owner.MenuView.OnShopClicked += ShopClicked;
        owner.MenuView.OnQuitClicked += QuitClicked;
        owner.MenuView.OnSettingsClicked += SettingsClicked;

        Time.timeScale = 0;
        ResetGame();
        owner.MenuView.Show();
    }

    public override void ExitState()
    {
        owner.MenuView.Hide();

        owner.MenuView.OnStartClicked -= StartClicked;
        owner.MenuView.OnShopClicked -= ShopClicked;
        owner.MenuView.OnQuitClicked -= QuitClicked;
        owner.MenuView.OnSettingsClicked -= SettingsClicked;

        base.ExitState();
    }
    private void ResetGame()
    {
        owner.Player.Reset();
        // ScrollingPlane.Instance.Reset();
    }
    private void StartClicked()
    {
        owner.ChangeState(new GameState());
    }

    private void ShopClicked()
    {
        owner.ChangeState(new ShopState());
    }

    private void QuitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SettingsClicked()
    {
        owner.ChangeState(new SettingsState());
    }
}