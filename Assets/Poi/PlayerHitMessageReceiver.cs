using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitMessageReceiver : MonoBehaviour
{
    public event UnityAction OnPlayerHit;

    void Awake()
    {
        MessageBroker.Default.Receive<HitMessage>().Subscribe(_ => 
        {
            OnPlayerHit?.Invoke();
        }).AddTo(this);
    }

    void OnDisable()
    {
        OnPlayerHit = null;
    }
}
