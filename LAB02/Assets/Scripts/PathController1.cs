using System.Collections.Generic;
using UnityEngine;

public class PathController1 : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    private List<Waypoint> thePath;
    private Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;
    
    public Animator animator;
    bool isWalking;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
        
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;
        
        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void moveForward()
    {
        float stepSize = MoveSpeed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            return;
        }

        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }
        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
            return;
        }
        
        target = pathManager.GetNextTarget();
    }
}
