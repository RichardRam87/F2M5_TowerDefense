using UnityEngine;
using UnityEngine.Events;

public abstract class GameplayState : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnStateEnter;
    [SerializeField] protected UnityEvent OnStateExit;
    
    public virtual void StateEnter()
    {
        OnStateEnter?.Invoke();
    }

    public virtual void StateExit()
    {
        OnStateExit?.Invoke();
    }
}
