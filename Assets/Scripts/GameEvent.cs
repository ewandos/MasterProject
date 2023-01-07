using System;
using UnityEngine;

public class GameEvent<T>
{
    public bool wasRaisedOnce;
    public int raisedCount;
    private event Action<T> Event;
    private float lastRaiseTimestamp;

    public void Invoke(T value)
    {
        Event?.Invoke(value);
        wasRaisedOnce = true;
        lastRaiseTimestamp = Time.timeSinceLevelLoad;
        raisedCount++;
    }

    public void Bind(Action callback)
    {
        Event += obj => callback();
    }

    public void BindWithParameters(Action<T> callback)
    {
        Event += callback;
    }

    public bool TimePassedSinceLastRaise(float seconds, bool ignoreRaisedOnce = false)
    {
        float passedSeconds = Time.timeSinceLevelLoad - lastRaiseTimestamp;
        return (ignoreRaisedOnce && !wasRaisedOnce) || (wasRaisedOnce && passedSeconds >= seconds);
    }
}
