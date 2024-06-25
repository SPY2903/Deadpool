using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    [SerializeField] private CharacterDetail characterDetail;
    [SerializeField] private CharacterDetail targetDetail;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform defaultPos;
    [SerializeField] private Transform posRadiusCenter;
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject rayDirection;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float distanceStopAtDefault;
    private MinionStateMachine stateMachine;
    private Animator anim;
    private Transform target;
    public MinionStateMachine MstateMachine => stateMachine;
    public Animator Anim => anim;
    public CharacterDetail crtDetail => characterDetail;
    public CharacterDetail tgDetail => targetDetail;
    public GameObject HealthBar => healthBar;
    [System.NonSerialized]
    public bool findOutTarget;
    [System.NonSerialized]
    public bool atTargetPos;
    [System.NonSerialized]
    public bool atDefaultPos;
    [System.NonSerialized]
    public bool beHit;
    [System.NonSerialized]
    public int dameTake;
    [System.NonSerialized]
    public bool isDie;
    [System.NonSerialized]
    public bool isTargetDie;

    private void Awake()
    {
        stateMachine = new MinionStateMachine(this);
        anim = GetComponent<Animator>();
        characterDetail.currentHealth = characterDetail.health;
        healthBar.GetComponent<HealthBar>().Initialize(characterDetail.health);
        healthBar.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.Initialize(stateMachine.mIdleState);
        isTargetDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        CheckCollison();
        if (isDie)
        {
            Destroy(gameObject, 1f);
        }
    }
    // Check collider manually
    public void CheckCollison()
    {
        findOutTarget = false;
        target = defaultPos;
        Collider[] hitColliders = Physics.OverlapSphere(posRadiusCenter.position, detectionRadius, collisionMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") && !isTargetDie)
            {
                RaycastHit hit;
                if(Physics.Raycast(rayDirection.transform.position, hitCollider.transform.position - transform.position,out hit))
                {
                    if (hit.collider.CompareTag("Player Coll"))
                    {
                        target = hitCollider.transform;
                        findOutTarget = true;
                    }
                }
            }
        }
    }
    public void Move()
    {
        FaceToTarget();
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (findOutTarget)
        {
            nav.stoppingDistance = stoppingDistance;
            if (distanceToTarget > nav.stoppingDistance)
            {
                // Đặt đích đến nếu khoảng cách lớn hơn phạm vi dừng lại
                atTargetPos = false;
                nav.SetDestination(target.position);
            }
            else
            {
                // Dừng agent nếu đã trong phạm vi dừng lại
                atTargetPos = true;
                nav.ResetPath();
            }
        }
        else
        {
            nav.stoppingDistance = 0;
            nav.SetDestination(target.position);
            //Debug.Log(Vector3.Distance(defaultPos.position, transform.position));
            atDefaultPos = Vector3.Distance(defaultPos.position, transform.position) <= distanceStopAtDefault ? true : false;
        }
    }
    public void FaceToTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);
        }
    }
    public void TakeDame(int dame)
    {
        characterDetail.currentHealth -= dame;
        if (characterDetail.currentHealth <= 0) characterDetail.currentHealth = 0;
        healthBar.GetComponent<HealthBar>().SetHealth(characterDetail.currentHealth);
    }
    public bool CheckAtTargetPos()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        nav.stoppingDistance = stoppingDistance;
        if (distanceToTarget > nav.stoppingDistance)
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(posRadiusCenter.position, detectionRadius);
        Gizmos.color= Color.green;
    }

}
