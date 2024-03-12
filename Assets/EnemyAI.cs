using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private float _chaseRadius = 10f;
    [SerializeField] private LayerMask _whatIsPlayer;
    // Attack, run etc states to each own interface
    private bool playerInAttackRange = false;
    private bool playerInSightRange = false;
    private bool isDead = false;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        AgentUpdate();
    }

    private async void AgentUpdate(float waitTime = 0.1f)
    {
        while (true)
        {
            Debug.Log("Checking");
            if (isDead)
            {
                break;
            }

            playerInAttackRange = Physics.CheckSphere(transform.position, _attackRadius, _whatIsPlayer);
            playerInSightRange = Physics.CheckSphere(transform.position, _chaseRadius, _whatIsPlayer);
                
            //change to switch case for basic states than interfaces
           
            if (playerInAttackRange)
            {
                _agent.SetDestination(transform.position);
            }
            else if (playerInSightRange)
            {
                Debug.Log("Found Player");
                _agent.SetDestination(player.position);
            }
            else
            {
                //idle
            }


            await UniTask.WaitForSeconds(waitTime);
        }
    }

}
