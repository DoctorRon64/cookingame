using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    private Vector2 direction;

    private void Update()
    {
        //input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector3 movement = new Vector3(direction.x, 0, direction.y) * speed;
        if (Input.GetKey(KeyCode.LeftShift)) movement *= sprintMultiplier;
        
        //change the postion
        transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.deltaTime * speed);
    }
}