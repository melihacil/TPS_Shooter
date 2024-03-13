using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class EnemyAI : MonoBehaviour
{

    enum State
    {
        chase,
        attack
    }

    private State EnemyState = State.chase;
    [SerializeField] private Transform player;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private float _chaseRadius = 10f;
    [SerializeField] private LayerMask _whatIsPlayer;
    // Attack, run etc states to each own interface
    private bool playerInAttackRange = false;
    private bool playerInSightRange = false;
    private bool isDead = false;

    private float _navAgentSpeed = 0.0f;

    [SerializeField] private string _animSpeedID;
    [SerializeField] private string _animMotionID;
    private Vector3 _lastPosition;
    private Animator _animator;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        AgentUpdate();
    }

    private void DeathFunc()
    {

        SpawnController.instance.DecreaseEnemyCount();
        Destroy(gameObject);
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

        
            if (Vector3.Distance(player.position, transform.position) > _attackRadius + 0.1f)
            {
                EnemyState = State.chase;
            }
            else
            {
                EnemyState = State.attack;
            }
            //change to switch case for basic states than interfaces
           
            switch (EnemyState)
            {
                case State.chase:
                    Debug.Log("Chase State");
                    _animator.SetTrigger("Chase");
                    _agent.SetDestination(player.position);
                    break;
                case State.attack:
                    _animator.SetTrigger("Attack");
                    _agent.SetDestination(transform.position);
                    break;
                default:
                    Debug.LogError("Can't find enemy state " + gameObject.name);
                    break;
                
            }
            
            await UniTask.WaitForSeconds(waitTime);
        }
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        DeathFunc();
    }
}
