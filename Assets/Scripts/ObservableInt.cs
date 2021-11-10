using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Observable Integer", fileName = "New variable")]
public class ObservableInt : ScriptableObject
{
    private int _value;
    public event Action OnChange = delegate { };

    
    public int Value 
    {
        get => _value;
        set
        {
            _value = value;
            OnChange();
        }
    }
}
