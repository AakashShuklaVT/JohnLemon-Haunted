using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public ReloadLevel reload;
    public Animator loseAnimator;
    private NavMeshAgent ghostAgent;
    private bool movingForward = true;

    private void Start()
    {
        ghostAgent = GetComponent<NavMeshAgent>();
        ghostAgent.stoppingDistance = 0f; // Ensures exact target reaching
        ghostAgent.SetDestination(point2.position);
    }

    private void Update()
    {
        MoveGhost();
        DetectPlayer();
    }

    private void MoveGhost()
    {
        if (ghostAgent.remainingDistance <= 0.1f && !ghostAgent.pathPending) // Ensures agent has truly reached the target
        {
            if (movingForward)
            {
                movingForward = false;
                ghostAgent.SetDestination(point1.position);
            }
            else
            {
                movingForward = true;
                ghostAgent.SetDestination(point2.position);
            }
        }
    }

    private void DetectPlayer()
    {
        RaycastHit hit;
        Vector3 start = transform.position;
        Vector3 end = start + transform.up;

        if (Physics.CapsuleCast(start, end, 0.5f, transform.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player died");
                loseAnimator.SetTrigger("PlayerLost");
                reload.ReloadScene();
            }
        }
    }
}
