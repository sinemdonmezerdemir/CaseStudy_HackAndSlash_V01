using UnityEngine;
public class AICharacter : Character
{
    public Transform Target, LookAtTarget;

    public delegate void TargetChangeHandler();

    public event TargetChangeHandler OnTargetChanged;

    private void OnEnable()
    {
        OnTargetChanged += HandleTargetChanged;
        SetCharacterState(CharacterStateFactory.IdleState());
        SetCharacterUpperState(CharacterStateFactory.HandsFreeState());
    }


    private void OnDisable()
    {
        OnTargetChanged -= HandleTargetChanged;
    }

    public override bool CheckInput()
    {
        try
        {
            if (Target == null)
                return false;
            if (Mathf.Abs(transform.position.x - Target.position.x) < 0.1f && Mathf.Abs(transform.position.z - Target.position.z) < 0.1f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch { return false; }
    }
    public void SetTarget(Transform target, Transform lookTarget = null)
    {
        Target = target;

        LookAtTarget = lookTarget;

        OnTargetChanged?.Invoke();
    }
    void HandleTargetChanged()
    {
        if (CheckInput())
            SetCharacterState(CharacterStateFactory.AIMoveState());
    }

}
