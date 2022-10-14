using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyAnimation _anim;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _playerLayer;
    private int _attackDamage = 40;
    private float _attackRate = 0.7f;
    private float _nextAttackTime = 0f;

    private int _maxHealth = 100;
    int currentHealth;
    void Start()
    {
        _anim = GetComponentInChildren<EnemyAnimation>();
        currentHealth = _maxHealth;
    }

    private void Update()
    {
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

    private void LightAttack()
    {
        _anim.Attack();
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(_attackDamage);
        }

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
    }
}
