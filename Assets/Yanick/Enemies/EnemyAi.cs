using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float speed = 4f;
    public float rotationSpeed = 6f;
    public float detectionRange = 20000f;

    [Header("Obstacle Avoidance")]
    public float avoidDistance = 2f;
    public float avoidStrength = 5f;
    public LayerMask obstacleMask;

    Vector3 currentVelocity;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        Vector3 directionToPlayer =
            (player.position - transform.position).normalized;

        Vector3 avoidance = CalculateAvoidance();

        Vector3 finalDirection =
            (directionToPlayer + avoidance).normalized;

        // Smooth rotation
        if (finalDirection != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(finalDirection);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }

        // Smooth movement
        transform.position +=
            transform.forward * speed * Time.deltaTime;
    }

    Vector3 CalculateAvoidance()
    {
        Vector3 avoidance = Vector3.zero;

        Vector3[] directions =
        {
            transform.forward,
            Quaternion.AngleAxis(30, Vector3.up) * transform.forward,
            Quaternion.AngleAxis(-30, Vector3.up) * transform.forward
        };

        foreach (Vector3 dir in directions)
        {
            Ray ray = new Ray(transform.position, dir);

            if (Physics.Raycast(ray, out RaycastHit hit, avoidDistance, obstacleMask))
            {
                Vector3 awayFromObstacle =
                    transform.position - hit.point;

                avoidance +=
                    awayFromObstacle.normalized
                    * avoidStrength
                    * (avoidDistance - hit.distance);
            }
        }

        return avoidance;
    }
}
