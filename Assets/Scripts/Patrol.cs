using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float TimeToWait;
    [SerializeField] private float Speed;

    public Transform[] _patrolPoints;
    public float VisionRange;

    private Animator animator;
    private int _currentPoint = 0;
    private float _timer = 0f;
    private Transform _player;

    void Start()
    {
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

        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);

        if(playerClose)
        {
            animator.SetBool("IsPatrolling", false);
            return;
        }
        if (Vector3.Distance(transform.position, _patrolPoints[_currentPoint].position) < 0.1f)
        {
            _timer += Time.deltaTime;
            //animator.SetBool("IsPatrolling", false);

            if (_timer >= TimeToWait)
            {
                _timer = 0f;
                NextPoint();
                //animator.SetBool("IsPatrolling", true);
            }
            else
            {
                //animator.SetBool("IsPatrolling", false);
            }
        }
        else
        {
            //animator.SetBool("IsPatrolling", true);
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
    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        return (dist < VisionRange);
    }
}