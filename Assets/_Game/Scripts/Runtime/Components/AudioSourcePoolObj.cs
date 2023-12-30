using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePoolObj : MonoBehaviour
{
    public AudioSource Source;

    public float DestroyDelay;

    public void ReturnPool() 
    {
        Invoke(nameof(Disable), DestroyDelay);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
