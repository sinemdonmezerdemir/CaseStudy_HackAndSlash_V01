using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public void FootStepsSound() 
    {
        SoundManager.PlaySound(SoundManager.Sound.Walk);
    }
}
