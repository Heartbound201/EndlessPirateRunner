﻿using UnityEngine;

public class PauseState : State
{
    public override void EnterState()
    {
        base.EnterState();
        
        owner.PauseView.OnResumeClicked += ResumeClicked;
        owner.PauseView.OnShopClicked += ShopClicked;
        
        owner.PauseView.Show();
    }

    public override void ExitState()
    {
        owner.PauseView.Hide();
        owner.PauseView.OnResumeClicked -= ResumeClicked;
        owner.PauseView.OnShopClicked -= ShopClicked;
        
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
        owner.ChangeState(new GameState(false));
    }
    private void ShopClicked()
    {
        owner.ChangeState(new ShopState());
    }

}