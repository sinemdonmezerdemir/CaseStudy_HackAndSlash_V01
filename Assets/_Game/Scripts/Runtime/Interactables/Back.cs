using UnityEngine;

public class Back : MonoBehaviour, IInteractable
{
    public void EndInteraction(Character character)
    {
    }

    public void Interaction(Character character)
    {
        if (character is Enemy) 
        {
            Enemy enemy = (Enemy)character;
            EnemyManager.Instance.ReturnEnemyPool(enemy);
        }
    }

    public void KeepInteracting(Character character)
    {
    }
}
