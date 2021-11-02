using UnityEditor.SceneManagement;
using UnityEngine;

public class ShopState : State
{
    public override void EnterState()
    {
        base.EnterState();
        
        owner.ShopView.OnMenuClicked += MenuClicked;
        
        owner.ShopView.FillShopItems();
        owner.ShopView.Show();
    }

    public override void ExitState()
    {
        owner.ShopView.Hide();
        owner.ShopView.ClearShopItems();
        
        owner.ShopView.OnMenuClicked -= MenuClicked;
        
        base.ExitState();
    }

    private void MenuClicked()
    {
        owner.ChangeState(new MenuState());
    }
}