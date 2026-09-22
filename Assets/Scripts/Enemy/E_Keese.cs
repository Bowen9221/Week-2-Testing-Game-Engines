using System;
using System.Collections;
using UnityEngine;


public class E_Keese : E_Base_Enemy
{
    [Header("Keese Variables")]
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private float _attackCooloff = 0.5f;
    [SerializeField] private float _moveSpeedDelta; // Used to slow down Keese during attack delay, and to reset speed to normal after attacking
    [SerializeField] private float _dashForce;

    public override IEnumerator PerformAttack()
    {

        _moveSpeed = _moveSpeed / _moveSpeedDelta;
        _canAttack = false;

        yield return new WaitForSeconds(_attackDelay);
        _rb.AddForce(_currentDir * _dashForce, ForceMode2D.Impulse);
        if (Vector2.Distance(player.gameObject.transform.position, this.transform.position) <= _attackRange)
        {
            player.TakeDamage(_attackDamage);
        }
        yield return new WaitForSeconds(_attackCooloff);
        _moveSpeed = _moveSpeed * _moveSpeedDelta;
        _canAttack = true;
        enemyState = STATE.Chasing;

    }

}
