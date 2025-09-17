using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] points;
    private int DestPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[DestPoint].position;
        DestPoint = (DestPoint + 1) % points.Length;

    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = GameObject.Find("Player").transform.position;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }
}
