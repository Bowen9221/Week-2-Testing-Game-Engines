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
    [SerializeField] public GameObject[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    private int _enemyCount;

    // WinCon references
    [SerializeField] private GameObject _triforce;
    private bool _hasWon = false;

    private void Start()
    {
        player = FindAnyObjectByType<P_Player>();
        SpawnEnemies();
        _enemyCount = _enemies.Length;
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
        for (int i = 0; i < _enemies.Length; i++)
        {
            Instantiate(_enemies[i], _spawnPoints[i].position, Quaternion.identity);
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
                    Debug.Log("You Win!");
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
