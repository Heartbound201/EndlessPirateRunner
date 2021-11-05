using UnityEngine.Events;

public class PauseView : View
{
    public UnityAction OnResumeClicked;
    public UnityAction OnMenuClicked;
    
    public void ResumeClick()
    {
        OnResumeClicked?.Invoke();
    }
    public void MenuClick()
    {
        OnMenuClicked?.Invoke();
    }
}