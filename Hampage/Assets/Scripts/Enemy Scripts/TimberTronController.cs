using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberTronController : AIController
{

    // Created by Giovanni Quevedo

    bool delayAttacks = false;
    float attackDelay = 2f;

    [SerializeField]
    Animator timbertronAnimator;

    void Start()
    {
        player = GameManager.staticPlayer.transform;
        walkPointRange = 5;
        sightRange = 10f;
        attackRange = 1.5f;
    }


    // This is here because the superclass's attackPlayer was being called
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

    void AttackPlayer()
    {
        //When in attack range the enemy doesn't move
        agent.SetDestination(transform.position);

        //Make our transform look towards the player, except only rotate around y
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));        

        if(timbertronAnimator.GetBool("isWalking")){
             timbertronAnimator.SetBool("isWalking",false);
        }


        if (!delayAttacks)
        {
            // Mele Attack player

            // TO-DO: Activate hitbox
            timbertronAnimator.SetTrigger("startAttack");

            delayAttacks = true;
            //Allows an attack after a cooldown
            Invoke(nameof(ResetAttack), attackDelay);
        }

        stuckFlag = false;
    }
    public bool stuckFlag = false;
    Vector3 oldPos;
    private void ChasePlayer()
    {
        //Enemy will follow the player's position
        agent.SetDestination(new Vector3(player.position.x, transform.position.y, player.position.z));
        //Debug.Log("Chase");

        //If we're stuck, move a little bit next to the player instead to right where they are 
        
        if(!stuckFlag){
            //This is our first call, so take note of transform and check if we're stuck later
            stuckFlag = true;
             oldPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Invoke(nameof(FixStuck), 2f);
        }

        //Tell our animator we're moving
        timbertronAnimator.SetBool("isWalking",true);

       
    }

     void Patroling()
    {
        stuckFlag = false;

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

    void FixStuck(){
        if(transform.position == oldPos){
            Debug.Log("Stuck");
            Patroling();
        }
    }

    void ResetAttack()
    {
        delayAttacks = false;
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
