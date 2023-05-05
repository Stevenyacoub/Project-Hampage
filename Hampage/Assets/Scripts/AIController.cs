using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    // Created by Alan Robertson

    public NavMeshAgent agent;
    protected Transform player;
    protected LayerMask whatIsGround;
    protected LayerMask whatIsPlayer;
    GameObject projectileSpawn;

    //Patroling
    public Vector3 walkPoint;
    protected bool walkPointSet;
    public float walkPointRange = 5f;

    //Attacking
    float timeBetweenAttacks = 2f;
    public float projectileUpForce = 3f;
    public float projectileForwardForce = 30f;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange = 25f;
    public float attackRange = 13f;
    public bool playerInSightRange;
    public bool playerInAttackRange;


    void Start()
    {
        projectileSpawn = transform.GetChild(0).gameObject;
        player = GameManager.staticPlayer.transform;
    }

    private void Awake()
    {
        //gets player and navMeshAgent
        agent = GetComponent<NavMeshAgent>();
        whatIsGround = LayerMask.GetMask("Ground");
        whatIsPlayer = LayerMask.GetMask("Player");

    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //decides what state to enter
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    void Patroling()
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

    protected void SearchWalkPoint()
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

    void ChasePlayer()
    {
        //Enemy will follow the player's position
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        //When in attack range the enemy doesn't move
        agent.SetDestination(transform.position);

        //Make our transform look towards the player, except only rotate around y
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        //Make our projectile spawn look exactly at the player
        projectileSpawn.transform.LookAt(player);
        


        if (!alreadyAttacked)
        {
            //Spawns and fires a projectile at player

          
            Rigidbody rb = Instantiate(projectile, projectileSpawn.transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(projectileSpawn.transform.forward * projectileForwardForce, ForceMode.Impulse);
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
