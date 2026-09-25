using UnityEngine;

public class Stelaphos_Spawner : Base_EnemySpawner
{

    [SerializeField] GameObject _stelaphosPrefab;
    public override E_Base_Enemy SpawnEnemy()
    {
        GameObject stelaphosGO = Instantiate(_stelaphosPrefab, transform.position, Quaternion.identity);
        return stelaphosGO.GetComponent<E_Base_Enemy>();
    }
}
