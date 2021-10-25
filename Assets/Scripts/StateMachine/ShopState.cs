using UnityEngine;

public class ShopState : State
{
    public override void EnterState()
    {
        base.EnterState();
        
        owner.ShopView.Show();
    }

    public override void ExitState()
    {
        owner.ShopView.Hide();
        
        base.ExitState();
    }

}