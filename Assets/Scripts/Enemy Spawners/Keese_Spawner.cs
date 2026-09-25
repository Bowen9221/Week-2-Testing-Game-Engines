using System.Security.Cryptography;
using UnityEngine;

public class Keese_Spawner : Base_EnemySpawner
{

    [SerializeField] GameObject _keesePrefab;
    public override E_Base_Enemy SpawnEnemy()
    {
        GameObject keeseGO = Instantiate(_keesePrefab, transform.position, Quaternion.identity);
        return keeseGO.GetComponent<E_Base_Enemy>();
    }
}
