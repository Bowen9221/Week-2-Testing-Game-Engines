using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_InputSystemHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputSystem;
    [SerializeField] Rigidbody2D _rb;

    private P_Player player;

    private InputActionMap _playerActions;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private Vector2 _moveDir;

    [Header("Movement Variables")]
    [SerializeField] private float _moveSpeed;

    [Header("Attack Variables")]
    [SerializeField] private float _animationDuration = 0.1f;
    [SerializeField] private float _knockbackStrength = 1.5f;
    private bool _canAttack = true;
    private bool _isAttacking = false;

    private void Start()
    {
        player = GetComponent<P_Player>();
        _rb = GetComponent<Rigidbody2D>();
        _playerActions = InputSystem.actions.FindActionMap("Player");
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }
    private void OnEnable()
    {
        _playerActions.Enable();
    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }
    private void Update()
    {
        _moveDir = _moveAction.ReadValue<Vector2>();

        if (_moveDir.sqrMagnitude >= 0.0001)
        {
            Quaternion lookDir = Quaternion.LookRotation(Vector3.forward, _moveDir);
            transform.rotation = (lookDir);
        }


        Camera.main.transform.rotation = Quaternion.identity;
        
        if (_attackAction.WasCompletedThisFrame() && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    private void FixedUpdate()
    {
        _rb.AddForce(_moveDir * _moveSpeed, ForceMode2D.Force);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _isAttacking)
        {
            //Take Damage Enemy
            //Deal Knockback to Enemy so attacks can't hit multiple times
            Vector2 knockbackDir = this.transform.position - collision.transform.position;
            Rigidbody2D _enemyrb = collision.GetComponent<Rigidbody2D>();
            _enemyrb.AddForce((knockbackDir.normalized * -1) * _knockbackStrength, ForceMode2D.Impulse);

            _isAttacking = false;

        }
    }
    private IEnumerator Attack()
    {
        if (_isAttacking)
            yield break;
        _isAttacking = true;
        yield return new WaitForSeconds(_animationDuration);
        _isAttacking = false;

    }
}
