using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");        
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
