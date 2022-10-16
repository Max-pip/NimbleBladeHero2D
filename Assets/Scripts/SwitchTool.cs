using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTool : MonoBehaviour
{

    [SerializeField] private GameObject _switchToolLocked;
    [SerializeField] private GameObject _switchToolActivated;
    [SerializeField] private GameObject _closeDoor;
    [SerializeField] private GameObject _openDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _switchToolLocked.SetActive(false);
        _switchToolActivated.SetActive(true);
        _closeDoor.SetActive(false);
        _openDoor.SetActive(true);
    }
}
