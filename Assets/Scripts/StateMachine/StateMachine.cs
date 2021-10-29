using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;
    public Player Player;
    // TODO change to singleton
    public ScrollingPlane seaScrollingPlane;
    public MenuView MenuView;
    public GameView GameView;
    public PauseView PauseView;
    public GameOverView GameOverView;
    public ShopView ShopView;

    private void Start()
    {
        MenuView.Hide();
        GameView.Hide();
        PauseView.Hide();
        GameOverView.Hide();
        ShopView.Hide();
        ChangeState(new MenuState());
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }

    public void ChangeState(State newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.owner = this;
            _currentState.EnterState();
        }
    }
}