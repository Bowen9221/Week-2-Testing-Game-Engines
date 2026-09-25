using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;


public class M_Event_Manager : MonoBehaviour
{
    // Player References
    P_Player player;
    private int _playerHealth;

    // Enemy references
    public Base_EnemySpawner keeseSpawner;
    public Base_EnemySpawner stelaphosSpawner;
    [SerializeField] private Transform[] _spawnPoints;
    private int _enemyCount = 0;

    // WinCon references
    [SerializeField] private GameObject _triforce;
    private bool _hasWon = false;

    private void Awake()
    {
        player = FindAnyObjectByType<P_Player>();
        SpawnEnemies();
    }

    private void HandleLose()
    {
        if (_playerHealth <= 0)
        {
            Debug.Log("You Lose");
            StartCoroutine(HandleWinLoseCon());
        }
    }
    private IEnumerator HandleWinLoseCon()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Loading Scene");
        SceneManager.LoadScene(0);
    }

    private void SpawnEnemies()
    {

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int _spawnerSelect;
            _spawnerSelect = Random.Range(0, 3);

            if (_spawnerSelect == 0 || _spawnerSelect == 2)
            {
                keeseSpawner.SpawnEnemy();
            }
            else
            {
                stelaphosSpawner.SpawnEnemy();
            }

            _enemyCount++;
        }

    }

    private void HandleWin()
    {
        if (_hasWon) return;

        if (_enemyCount <= 0)
        {
            if (!_triforce.activeSelf)
            {
                _triforce.SetActive(true);
            }

            if (_triforce != null && _triforce.activeSelf)
            {
                float distance = Vector2.Distance(player.transform.position, _triforce.transform.position);

                if (distance <= 1f)
                {
                    StartCoroutine(HandleWinLoseCon());
                }
            }
        }
    }

    private void Update()
    {
        _playerHealth = player.GetPlayerHealth(_playerHealth);

        HandleLose();
        HandleWin();
    }

    public void DecreaseEnemyCount()
    {
        _enemyCount -= 1;
    }
}



