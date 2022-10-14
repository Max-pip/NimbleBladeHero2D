using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { private get; set; }
    public bool IsFlying { private get; set; }

    public bool CheckedLadder { private get; set; }
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);
        _animator.SetBool("OnLadder", CheckedLadder);
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
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
