using DG.Tweening;
using Managers;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Character : MonoBehaviour
{
    /*--------------------------------------------------------------*/

    public CharacterData Data;

    public int CurrentHealt;

    public Rigidbody Rigidbody;

    public CharacterMainBaseState CurrentState;

    public CharacterUpperBaseState CurrentUpperState;

    public Animator Animator;

    public IInteractable Interactable;

    public ParticleSystem VfxDeath, VfxDamage;

    /*--------------------------------------------------------------*/

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interaction(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.EndInteraction(this);
        }
    }

    protected virtual void Start()
    {
        SetCharacterState(CharacterStateFactory.IdleState());
        SetCharacterUpperState(CharacterStateFactory.HandsFreeState());
        SetAnimSpeed();
    }

    public void TakeDamage(int i)
    {
        if (CurrentHealt > 0)
        {
            CurrentHealt -= i;

            VfxDamage.Play();

            if (CurrentHealt <= 0)
                SetCharacterState(CharacterStateFactory.DeathState());
        }
        else
        {
            SetCharacterState(CharacterStateFactory.DeathState());
        }
    }

    /*--------------------------------------------------------------*/

    public virtual void SetMoveSpeed(int i)
    {
        Data.MoveSpeed = i;

        SetAnimSpeed();
    }

    public virtual void SetAnimSpeed()
    {
        Animator.speed = ((Data.MoveSpeed+ 8) / 10.0f);
    }

    public void SetAnimState(string s, bool b)
    {
        Animator.SetBool(s, b);
    }

    public void SetCharacterState(CharacterMainBaseState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    public void SetCharacterUpperState(CharacterUpperBaseState newState)
    {
        CurrentUpperState?.ExitState();
        CurrentUpperState = newState;
        CurrentUpperState.EnterState(this);

    }
    /*--------------------------------------------------------------*/

    public virtual bool CheckInput()
    {
        float horizontal = UIManager.Instance.InGameGroup.Joystick.Horizontal;
        float vertical = UIManager.Instance.InGameGroup.Joystick.Vertical;


        if ((horizontal != 0 || vertical != 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*--------------------------------------------------------------*/

}
