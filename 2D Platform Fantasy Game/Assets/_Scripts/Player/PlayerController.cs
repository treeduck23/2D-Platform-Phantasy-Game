using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerRigidbody;
    private void Awake()
    {
        playerRigidbody = GameObject.Find("PlayerRigidbody");
    }

    private void Update()
    {
        playerRigidbody.transform.position = transform.position;
    }
}
