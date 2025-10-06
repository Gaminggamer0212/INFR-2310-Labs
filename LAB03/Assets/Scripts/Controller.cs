using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
        }
    }
}
