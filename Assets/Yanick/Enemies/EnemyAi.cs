using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 5f;
    public float obstacleDistance = 1.5f;

    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, transform.forward, obstacleDistance))
        {
            AvoidObstacle();
        }

        else
        {
            RotateTowards(direction);
            MoveForward();
        }
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void AvoidObstacle()
    {
        transform.Rotate(Vector3.up * 120f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Hit!");
        }
    }
}
