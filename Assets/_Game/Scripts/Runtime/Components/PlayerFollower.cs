using DG.Tweening;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(LevelManager.Instance.Player.transform.position.x, 0.7f, LevelManager.Instance.Player.transform.position.z);
    }
}
