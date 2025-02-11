using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float TimeToWait;
    [SerializeField] private float Speed;
    [SerializeField] private LayerMask ObstacleMask;
    [SerializeField] private LayerMask PlayerMask;

    public Transform[] _patrolPoints;
    public float VisionRange;
    public float VisionAngle;
    public Collider2D killCollider;

    private Animator animator;
    private int _currentPoint = 0;
    private float _timer = 0f;
    private Transform _player;
    private EnemyAlarm enemyAlarm;

    void Start()
    {

        enemyAlarm = GetComponentInChildren<EnemyAlarm>();

        animator = GetComponent<Animator>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_patrolPoints.Length > 0)
        {
            MoveToNextPoint();
        }
    }

    void Update()
    {
        if (_patrolPoints.Length == 0)
        {
            return;
        }

        var playerClose = IsPlayerClose();
        animator.SetBool("IsChasing", playerClose);

        if(playerClose)
        {
            enemyAlarm.PlayerDetected();
            animator.SetBool("IsPatrolling", false);
            ChasePlayer();
        }
        else
        {
            enemyAlarm.PlayerLeft();
            Patroling();
        }
    }
    void Patroling()
    {
        if (Vector3.Distance(transform.position, _patrolPoints[_currentPoint].position) < 0.1f)
        {
            _timer += Time.deltaTime;
            animator.SetBool("IsPatrolling", false);

            if (_timer >= TimeToWait)
            {
                _timer = 0f;
                NextPoint();
                animator.SetBool("IsPatrolling", true);
            }
            else
            {
                animator.SetBool("IsPatrolling", false);
            }
        }
        else
        {
            animator.SetBool("IsPatrolling", true);
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_currentPoint].position, Speed * Time.deltaTime);
    }

    void NextPoint()
    {
        _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
    }
    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, Speed * Time.deltaTime);
    }
    private bool IsPlayerClose()
    {
        if (_player == null)
            return false;

        float dist = Vector2.Distance(transform.position, _player.position);
        if (dist >= VisionRange)
            return false;

        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.right, directionToPlayer);

        if (angle > VisionAngle / 2f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, dist, ObstacleMask);
        if (hit.collider != null)
            return false;

        return true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.Kill(); 
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, VisionRange);

        Vector2 forward = transform.right * VisionRange;
        Vector2 leftLimit = Quaternion.Euler(0, 0, -VisionAngle / 2) * forward;
        Vector2 rightLimit = Quaternion.Euler(0, 0, VisionAngle / 2) * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftLimit);
        Gizmos.DrawRay(transform.position, rightLimit);

        if (_player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _player.position);
        }
    }
}