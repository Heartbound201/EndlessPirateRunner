﻿using UnityEngine;

public class MenuState : State
{
    public override void EnterState()
    {
        base.EnterState();

        owner.MenuView.OnStartClicked += StartClicked;
        owner.MenuView.OnShopClicked += ShopClicked;

        Time.timeScale = 0;
        
        owner.MenuView.Show();
    }

    public override void ExitState()
    {
        owner.MenuView.Hide();

        owner.MenuView.OnStartClicked -= StartClicked;
        owner.MenuView.OnShopClicked -= ShopClicked;

        base.ExitState();
    }

    private void StartClicked()
    {
        owner.ChangeState(new GameState(true));
    }
    private void ShopClicked()
    {
        owner.ChangeState(new ShopState());
    }

}