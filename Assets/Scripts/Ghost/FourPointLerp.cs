using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FourPointLerp : MonoBehaviour
{
    public Transform point1, point2, point3, point4;
    public float castRadius = 1f;
    public float maxDistance = 1f;
    public Animator loseAnimator;
    public ReloadLevel reload;
    private NavMeshAgent ghostAgent;
    private int currentPointIndex = 0;
    private Transform[] points;

    private void Awake()
    {
        ghostAgent = GetComponent<NavMeshAgent>(); // Initialize NavMeshAgent
        points = new Transform[] { point1, point2, point3, point4 }; // Store points in an array
        transform.position = point1.position; // Start at point1
    }

    private void Start()
    {
        MoveToNextPoint();
    }

    private void Update()
    {
        CheckForPlayer();
        if (ghostAgent.remainingDistance <= 0.1f)
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % points.Length; // Cycle through points
        ghostAgent.SetDestination(points[currentPointIndex].position);
    }

    private void CheckForPlayer()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(transform.position, transform.position + transform.up * 2, castRadius, transform.forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                loseAnimator.SetTrigger("PlayerLost");
                reload.ReloadScene();
            }
        }
    }
}
