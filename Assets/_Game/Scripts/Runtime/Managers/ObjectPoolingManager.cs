using Managers;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : SingletonMonoBehaviour<ObjectPoolingManager>
{
    public void OnGameStateChanged(GameState newState)
    {
    }

    protected override void Awake()
    {
        base.Awake();
    }

    
}
