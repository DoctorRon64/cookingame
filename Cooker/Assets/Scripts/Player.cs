using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float rotSpeed = 7f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    private Vector2 direction;
    private Vector3 movement;
    private bool isWalking = false;
    
    private void Update()
    {
        //input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        movement = CalcMove();
        if (Input.GetKey(KeyCode.LeftShift)) movement *= sprintMultiplier;
        
        isWalking = movement != Vector3.zero;
        if (!isWalking) return;
        Vector3 targetPosition = transform.position + movement;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        //change the postion
        transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.deltaTime * speed);
    }

    private Vector3 CalcMove () => new Vector3(direction.x, 0, direction.y) * speed;
    public bool IsWalking() => isWalking;
}