using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public GameObject projectileSpawn;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public float projectileUpForce = 3f;
    public float projectileForwardForce = 30f;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;


    void Start()
    {
        projectileSpawn = transform.GetChild(0).gameObject;
        Debug.Log(projectileSpawn.name);
    }

    private void Awake()
    {
        //gets player and navMeshAgent
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //decides what state to enter
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        //find walkpoint of none is set
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        //if walkpoint set walk to the walkpoint
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //set the walkpoint
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        //Enemy will follow the player's position
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //When in attack range the enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            //Spawns and fires a projectile at player

          
            Rigidbody rb = Instantiate(projectile, projectileSpawn.transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * projectileUpForce, ForceMode.Impulse);


            alreadyAttacked = true;
            //attacks again after cooldown
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //visualive the attack and sight range in unity scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
