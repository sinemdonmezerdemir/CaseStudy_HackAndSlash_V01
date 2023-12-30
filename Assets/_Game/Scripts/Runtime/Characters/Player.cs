using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float Speed;

    public int FireballLevel;

    public Transform Back;

    public List<Fireball> Fireballs = new List<Fireball>();

    [SerializeField]
    List<Transform> EnemyTransform = new List<Transform>();

    public Ground CurrentGround;

    public Transform MoneyParent;

    private void Awake()
    {
        Speed = Data.MoveSpeed;
    }

    private void OnEnable()
    {
        User.Data.OnFireballLevelChanged += OnFireballLevelChanged;
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        User.Data.OnFireballLevelChanged -= OnFireballLevelChanged;
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    protected override void Start()
    {
        base.Start();

        CurrentHealt = Data.MaxHealth;
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdateState();

        CurrentUpperState?.FixedUpdateState();
    }

    protected virtual void Update()
    {
        CurrentState?.UpdateState();

        CurrentUpperState?.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space))
            User.Data.UpdateFireballLevel();
    }

    void OnFireballLevelChanged()
    {
        if (User.Data.GetFireballLevel() <= Fireballs.Count)
        {
            for (int i = 0; i < User.Data.GetFireballLevel(); i++)
            {
                Fireballs[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState) 
        {
            case GameState.MainMenu:
                OnReset();
                break;
            case GameState.LevelComplate:
                SetCharacterState(CharacterStateFactory.PauseState());
                break;
        }
    }

    public Transform GetRandomPos()
    {
        int i = Random.Range(0, EnemyTransform.Count);

        return EnemyTransform[i];
    }
    private void OnReset()
    {
        CurrentHealt = Data.MaxHealth;
        FireballLevel = 1;
        GetFireballs();
        Speed = Data.MoveSpeed;
        SetCharacterUpperState(CharacterStateFactory.HandsFreeState());
        SetCharacterState(CharacterStateFactory.IdleState());
    }

    public override void SetMoveSpeed(int i)
    {
        Speed++;

        SetAnimSpeed();
    }

    public override void SetAnimSpeed()
    {
        Animator.speed = ((Speed + 8) / 10.0f);
    }

    public void SetFireballLevel()
    {
        if (FireballLevel < Fireballs.Count)
        {
            FireballLevel++;

            for (int i = 0; i < FireballLevel; i++)
            {
                Fireballs[i].gameObject.SetActive(true);
            }
        }
    }

    void GetFireballs() 
    {
        for (int i = 0; i < Fireballs.Count; i++)
            Fireballs[i].gameObject.SetActive(false);

        for (int i = 0; i < FireballLevel; i++)
        {
            Fireballs[i].gameObject.SetActive(true);
        }
    }

    public void SetHealth()
    {
        if (CurrentHealt < Data.MaxHealth)
        {
            CurrentHealt+=5;

            CurrentHealt = Mathf.Clamp(CurrentHealt, 0, Data.MaxHealth);
        }
    }

}