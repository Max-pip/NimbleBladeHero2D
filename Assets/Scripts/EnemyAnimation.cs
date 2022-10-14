using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void TakeDamage()
    {
        _animator.SetTrigger("Hurt");
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
    }
}
