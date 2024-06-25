using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] private CharacterDetail characterDetail;
    [SerializeField] private CharacterDetail targetDetail;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance;
    private BossStateMachine bossStateMachine;
    private Animator anim;
    public CharacterDetail crtDetail => characterDetail;
    public CharacterDetail tgDetail => targetDetail;
    public BossStateMachine BStateMachine => bossStateMachine;
    public NavMeshAgent navMesh => nav;
    public Transform Target => target;
    public float StopDistance => stoppingDistance;
    public Animator Anim => anim;
    [System.NonSerialized]
    public bool isAtTarget;
    [System.NonSerialized]
    public int currentAttack;
    [System.NonSerialized]
    public bool isJumpAttack;
    [System.NonSerialized]
    public bool beHit;
    [System.NonSerialized]
    public int dameTake;
    [System.NonSerialized]
    public bool isDie;

    private void Awake()
    {
        bossStateMachine = new BossStateMachine(this);
        anim = GetComponent<Animator>();
        characterDetail.currentHealth = characterDetail.health;
        healthBar.GetComponent<HealthBar>().Initialize(characterDetail.health);
        currentAttack = 0;
        nav.speed = characterDetail.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossStateMachine.Initilialize(bossStateMachine.bIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        bossStateMachine.Update();
        if (isDie)
        {
            Destroy(gameObject, 1f);
            healthBar.SetActive(false);
        }
    }
    public void Move(Transform tg)
    {
        FaceToTarget();
        float distanceToTarget = Vector3.Distance(transform.position, tg.position);
        nav.stoppingDistance = stoppingDistance;
        if (distanceToTarget > nav.stoppingDistance)
        {
            // Đặt đích đến nếu khoảng cách lớn hơn phạm vi dừng lại
            isAtTarget = false;
            nav.SetDestination(target.position);
        }
        else
        {
            // Dừng agent nếu đã trong phạm vi dừng lại
            isAtTarget = true;
            nav.ResetPath();
        }
    }
    public void Jump(Transform tg)
    {
        FaceToTarget();
        float distanceToTarget = Vector3.Distance(transform.position, tg.position);
        nav.stoppingDistance = stoppingDistance;
        if (distanceToTarget > nav.stoppingDistance && isJumpAttack)
        {
            // Đặt đích đến nếu khoảng cách lớn hơn phạm vi dừng lại
            isAtTarget = false;
            nav.SetDestination(target.position);
        }
        else
        {
            // Dừng agent nếu đã trong phạm vi dừng lại
            isAtTarget = true;
            nav.ResetPath();
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

            // Quay đối tượng một cách mượt mà
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);
        }
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
    public void TakeDame(int dame)
    {
        characterDetail.currentHealth -= dame;
        if (characterDetail.currentHealth <= 0) characterDetail.currentHealth = 0;
        healthBar.GetComponent<HealthBar>().SetHealth(characterDetail.currentHealth);
    }
    public void JumpingMove()
    {
        isJumpAttack = true;
    }
    public void FallingMove()
    {
        isJumpAttack = false;
    }
}
