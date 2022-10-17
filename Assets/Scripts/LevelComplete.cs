using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject _openDoor;
    [SerializeField] private GameObject _winPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_openDoor.activeInHierarchy)
        {
            _winPanel.SetActive(true);
            Debug.Log("Complete");
        }
    }
}
