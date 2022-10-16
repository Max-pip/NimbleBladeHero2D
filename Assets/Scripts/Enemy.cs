using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _speed = 1.5f;
    private float _nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private bool _isMoving;
    private Vector2 _direction;

    Seeker seeker;
    Rigidbody2D rb;

    private EnemyAnimation _anim;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _playerLayer;
    private int _attackDamage = 35;
    private float _attackRate = 0.9f;
    private float _nextAttackTime = 0f;

    private int _maxHealth = 100;
    int currentHealth;
    void Start()
    {
        _anim = GetComponentInChildren<EnemyAnimation>();
        currentHealth = _maxHealth;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f); 
    }

    void UpdatePath()
    {
        if (rb != null)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, _target.position, OnPathComplete);
            }
        }
    }

    private void Update()
    {
        MoveAI();

        if (Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + 1f / _attackRate;
            if (Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _playerLayer))
            {
                LightAttack();
                //_nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void MoveAI()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        _direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.velocity = new Vector2(_direction.x * _speed, rb.velocity.y);
        _isMoving = transform.position.x != 0 ? true : false;

        if (_isMoving)
        {

            if (rb.velocity.x >= 0.01f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

        _anim.IsMoving = _isMoving;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < _nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void LightAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(_attackDamage);
        }
        _anim.Attack();
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange); ;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _nextAttackTime = Time.time + 1f / _attackRate;

        _anim.TakeDamage();

        if(currentHealth <= 0)
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
        Destroy(gameObject, 2f);
    }
}
