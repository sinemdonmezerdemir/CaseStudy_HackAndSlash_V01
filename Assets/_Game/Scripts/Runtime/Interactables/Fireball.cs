
using UnityEngine;

public class Fireball : MonoBehaviour,IInteractable
{
    public void EndInteraction(Character character)
    {
    }

    public void Interaction(Character character)
    {
        if (character is Enemy)
        {
            Enemy enemy = (Enemy)character;

            enemy.TakeDamage(10);
        }
    }

    public void KeepInteracting(Character character)
    {
    }
}
