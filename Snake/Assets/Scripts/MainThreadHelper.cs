using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadHelper : MonoBehaviour
{
    public static readonly ConcurrentQueue<Action> MainQueue = new ConcurrentQueue<Action>();
    void Update()
    {
        if (!MainQueue.IsEmpty)
        {
            while (MainQueue.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        } 
    }
}
