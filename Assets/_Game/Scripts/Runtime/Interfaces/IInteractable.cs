using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public void Interaction(Character character);

    public void KeepInteracting(Character character);
    public void EndInteraction(Character  character);
} 