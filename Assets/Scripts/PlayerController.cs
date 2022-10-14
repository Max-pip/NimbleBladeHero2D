using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    //Health
    private int _maxHealth = 100;
    int currentHealth;

    //Attack
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    private int _attackDamage = 40;
    private float _attackRate = 2f;
    private float _nextAttackTime = 0f;

    //For ladder
    [SerializeField] private Transform _checkLadder;
    [SerializeField] private float _checkRadiusLadder;
    [SerializeField] private bool _checkedLadder;
    [SerializeField] private LayerMask _ladderMask;
    [SerializeField] private float _ladderSpeed;
    private Vector2 _moveVector;

    //CheckGround
    private float _moveInput;
    private bool _isMoving;
    private bool _isGrounded;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _groundMask;

    private PlayerAnimations _anim;
    private Rigidbody2D _Rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<PlayerAnimations>();
        currentHealth = _maxHealth;
    }


    void Update()
    {
        Move();

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LightAttack();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }

        CheckLadder();
        LadderMechanics();
        LadderUpDown();

        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        _Rigidbody.velocity = new Vector2(_moveInput * _speed, _Rigidbody.velocity.y);
        _isMoving = _moveInput != 0 ? true : false;

        if (_isMoving)
        {
            _spriteRenderer.flipX = _moveInput > 0 ? false : true;
        }

        _anim.IsMoving = _isMoving;
        _anim.IsFlying = IsFlying();
    }

    private void Jump()
    {
        _Rigidbody.velocity = Vector2.up * _jumpForce;
        _anim.Jump();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _groundMask);
    }

    private void CheckLadder()
    {
        _checkedLadder = Physics2D.OverlapPoint(_checkLadder.position, _ladderMask);
        _anim.CheckedLadder = _checkedLadder;
    }

    private void LadderMechanics()
    {
        if (_checkedLadder)
        {
            _Rigidbody.gravityScale = 0;
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, _moveVector.y * _ladderSpeed);
        }
        else
        {
            _Rigidbody.gravityScale = 1;
        }
    }

    private void LadderUpDown()
    {
        _moveVector.y = Input.GetAxis("Vertical");
    }

    private bool IsFlying()
    {
        return _Rigidbody.velocity.y < 0;
    }

    private void LightAttack()
    {
        _anim.Attack();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange); ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_checkLadder.position, _checkRadiusLadder);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _nextAttackTime = Time.time + 1f / _attackRate;

        if (currentHealth > 0)
        {
            _anim.TakeDamage();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _anim.Death();

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
