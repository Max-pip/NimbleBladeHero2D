using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { private get; set; }
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
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
