using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Animator animator;
    public NavMeshAgent agent;
    public ItemController controller;
    public Transform player;
    public EnemyUI ui;
    public float maxHealth;
    public float currentHealth;
    public LayerMask whatIsGround, whatIsPlayer;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controller = GetComponent<ItemController>();
    }
    private void Start()
    {
        controller.items.ResetHp();
        maxHealth = controller.items.maxHealth;
        currentHealth = controller.items.maxHealth;
        ui.UpdateHealthBar(currentHealth,maxHealth);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetFloat("Speed", 0f, .1f, Time.deltaTime);
        }
    }
    private void SearchWalkPoint()
    {
        
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            animator.SetFloat("Speed", .5f, .1f, Time.deltaTime);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", 1f, .1f, Time.deltaTime);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        animator.SetTrigger("Attack");

        if (!alreadyAttacked)
        {
            ///Attack code here
            
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        animator.ResetTrigger("Attack");
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        controller.items.health -= damage;
        currentHealth = controller.items.health;
        ui.UpdateHealthBar(currentHealth,maxHealth);
        if (controller.items.health <= 0)
        {
            animator.SetTrigger("Die");

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void StartDealDamage()
    {
        GetComponentInChildren<WeaponScript>().StartAttack();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<WeaponScript>().EndAttack();
    }
}

