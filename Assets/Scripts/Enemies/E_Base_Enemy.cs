using System;
using System.Collections;
using UnityEngine;


 public enum STATE 
{ 
    Spawning,
    Chasing,
    Attacking,
    Dying,

}


public class E_Base_Enemy : MonoBehaviour
{
    protected STATE enemyState;
    protected Rigidbody2D _rb;
    [SerializeField] protected P_Player player;
    [SerializeField] private Transform _player;
    [SerializeField] private M_Event_Manager _eventManager;
    protected Vector2 _currentDir;

    [Header("Attack Variables")]
    [SerializeField] protected float _attackRange;
    [SerializeField] private float _attackDuration;
    protected bool _canAttack = false;

    [Header("Enemy Variables")]
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _attackDamage;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        enemyState = STATE.Spawning;
        player = FindAnyObjectByType<P_Player>();
        _player = player.transform;
        _eventManager = FindAnyObjectByType<M_Event_Manager>();
    }

    private void Spawning()
    {
        enemyState = STATE.Chasing;
    }

    private void HandleMovement()
    {

        if (Vector2.Distance(_player.position, transform.position) <= _attackRange)
        {
            _canAttack = true;
            enemyState = STATE.Attacking;
        }
        else
        {
            Vector2 moveDir = -1 * (transform.position - _player.position);
            _currentDir = moveDir;
            Quaternion lookDir = Quaternion.LookRotation(Vector3.forward,moveDir);
            transform.rotation = lookDir;

            _rb.AddForce(moveDir * _moveSpeed, ForceMode2D.Force);
        }
    }
        
    private void Attack()
    {
        if (_canAttack)
        {
            StartCoroutine(PerformAttack());
            _canAttack = false;
        }
        else
        {
            return;
        }
    }

    public virtual IEnumerator PerformAttack()
    {
        player.TakeDamage(_attackDamage);
        yield return new WaitForSeconds(_attackDuration);
        _canAttack = true;
        enemyState = STATE.Chasing;

    }

    private void HandleDeath()
    {
        _eventManager.DecreaseEnemyCount();
        Destroy(this.gameObject);
    }

    private void Update()
    {
        
        if (_health <= 0)
        {
            enemyState = STATE.Dying;
        }

        switch (enemyState)
        {
            case STATE.Spawning:
                Spawning();
                break;
            case STATE.Chasing:
                HandleMovement();
                break;
            case STATE.Attacking:
                Attack();
                break;
            case STATE.Dying:
                HandleDeath();
                break;

        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public int GetHealth()
    {
        return _health;
    }

    

   
}
