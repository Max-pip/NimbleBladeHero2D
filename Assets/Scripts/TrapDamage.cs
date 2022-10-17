using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public PlayerController _playerController;
    [SerializeField] private GameObject _player;
    private int _damage = 40;
    private int _jumpForce = 4;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        _player.GetComponent<PlayerController>().TakeDamage(_damage);
        _player.GetComponent<PlayerController>().CatchTrap(_jumpForce);
    }
}
