using System;
using System.Collections;
using UnityEngine;


 enum STATE 
{ 
    Spawning,
    Chasing,
    Attacking,
    Dying,

}


public class E_Base_Enemy : MonoBehaviour
{
    private STATE enemyState;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _player;

    [Header("Attack Variables")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDuration;
    private bool _canAttack = false;

    [Header("Enemy Variables")]
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackDamage;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        enemyState = STATE.Spawning;
    }

    private void Spawning()
    {
        enemyState = STATE.Chasing;
    }

    public virtual void HandleMovement()
    {

        if (Vector2.Distance(_player.position, transform.position) <= _attackRange)
        {
            if (_canAttack)
            {
                return;
            }
            _canAttack = true;
            enemyState = STATE.Attacking;
        }
        else
        {
            Vector2 moveDir = -1 * (transform.position - _player.position);
            Quaternion lookDir = Quaternion.LookRotation(Vector3.forward,moveDir);
            transform.rotation = lookDir;

            _rb.AddForce(moveDir * _moveSpeed, ForceMode2D.Force);
        }
    }
        
    public virtual void Attack()
    {
        if (_canAttack)
        {
            StartCoroutine(PerformAttack());
        }
        else
        {
            return;
        }
    }

    private IEnumerator PerformAttack()
    {
        // Do Attack Stuff
        _canAttack = false;
        yield return new WaitForSeconds(_attackDuration);
        enemyState = STATE.Chasing;

    }

    private void HandleDeath()
    {
        Debug.Log("I'm Dying, Please Help!");
        this.gameObject.SetActive(false);
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
        Debug.Log(enemyState);
    }

   
}
