using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public List<GameObject> EnemyPrefabs = new List<GameObject>();

    public Transform EnemyParent;

    private List<Enemy> _enemies = new List<Enemy>();

    bool _activecontrol = false;

    int _stageEnemyCount = 0;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < EnemyPrefabs.Count; j++)
            {
                Enemy enemy = Instantiate(EnemyPrefabs[j], EnemyParent).GetComponent<Enemy>();

                enemy.transform.position = LevelManager.Instance.Player.transform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20));

                enemy.SetCharacterState(CharacterStateFactory.PassiveState());

                _enemies.Add(enemy);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.InGame:
                _activecontrol = true;
                break;

            case GameState.LevelComplate:
                OnReset();
                break;

            case GameState.GameOver:
                OnReset();
                break;
        }
    }

    public Enemy GetEnemyFromPool()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState.Type == CharacterMainStateType.Passive)
            {
                _stageEnemyCount++;
                _enemies[i].SetCharacterState(CharacterStateFactory.ActiveState());
                return _enemies[i];
            }
        }

        return null;
    }

    public void ReturnEnemyPool()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState.Type != CharacterMainStateType.Passive)
            {
                _enemies[i].SetCharacterState(CharacterStateFactory.PassiveState());
            }
        }
    }

    public void ReturnEnemyPool(Enemy enemy)
    {
        enemy.SetCharacterState(CharacterStateFactory.PassiveState());
    }

    public List<Enemy> GetActiveEnemies()
    {
        List<Enemy> list = new List<Enemy>();

        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState.Type != CharacterMainStateType.Passive)
            {
                if (!list.Contains(_enemies[i]))
                {
                    list.Add(_enemies[i]);
                }
            }
        }

        return list;
    }

    public void EnemiesPause()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState.Type != CharacterMainStateType.Passive)
            {
                _enemies[i].SetCharacterState(CharacterStateFactory.PauseState());
            }
        }
    }

    public void EnemiesPlay()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState.Type == CharacterMainStateType.Pause)
            {
                _enemies[i].SetCharacterState(CharacterStateFactory.IdleState());
            }
        }
    }

    private void Update()
    {
        if (!_activecontrol)
            return;

        EnemyCountControl();

        List<Enemy> list = GetActiveEnemies();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetTarget(LevelManager.Instance.Player.transform);
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].CurrentState?.UpdateState();

            list[i].CurrentUpperState?.UpdateState();
        }

    }

    private void FixedUpdate()
    {
        if (!_activecontrol)
            return;

        List<Enemy> list = GetActiveEnemies();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].CurrentState?.FixedUpdateState();

            list[i].CurrentUpperState?.FixedUpdateState();
        }
    }

    void EnemyCountControl()
    {
        List<Enemy> enemylist = GetActiveEnemies();

        if ((_stageEnemyCount < LevelManager.Instance.GetStageClearEnemyCount()) && (enemylist.Count < LevelManager.Instance.ActiveEnemyCount))
        {
            int min = Mathf.Min((LevelManager.Instance.GetStageClearEnemyCount() - _stageEnemyCount), (LevelManager.Instance.ActiveEnemyCount - enemylist.Count));
            for (int i = 0; i < min; i++)
            {
                Enemy enemy = GetEnemyFromPool();
            }
        }
        else if (_stageEnemyCount == LevelManager.Instance.GetStageClearEnemyCount())
        {
            GameManager.Instance.SetState(GameState.LevelComplate);
        }
    }

    private void OnReset()
    {
        _activecontrol = false;
        _stageEnemyCount = 0;
        ReturnEnemyPool();
    }
}
