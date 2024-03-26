using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;
    }
    
    void Update()
    {
        
    }
}
