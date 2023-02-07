using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.6f;
    public float attackDamage = 10f;
    public float detectionRange = 15f;

    public float moveSpeed = 3f;

    private Transform player;

    public Animation animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animation>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
            FaceTarget();
            
            if (distanceToPlayer <= attackRange && attackCooldown <= 0f)
            {

                Attack();
                animator.Play("attack");
            }
            else
            {
                animator.Play("run");
            }
        }
        animator.PlayQueued("idle");
    }

    void Attack()
    {
        player.GetComponent<PlayerManager>().TakeDamage(attackDamage);

        attackCooldown = 1f / attackSpeed;
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        if(collision.collider.tag == "Player")
        {
            Debug.Log("Player");
        }
    }

}