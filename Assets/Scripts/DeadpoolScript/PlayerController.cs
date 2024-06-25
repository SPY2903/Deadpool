using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterDetail characterDetail;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private DeadpoolSound deadpoolSound;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollTime;
    //[SerializeField] private GameObject checkGround;
    [SerializeField] private GameObject checkHit;
    private CloseToGround closeToGround;
    private float turnSmoothVelocity;
    private StateMachine playerStateMachine;
    private CharacterController controller;
    private Animator anim;
    private Vector3 playerVelocity;
    private Vector3 moveDir;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private float currentSpeed;
    private Camera mainCamera;
    private CheckHit checkH;
    public bool isCloseToGround;
    // Check collider manually
    //[SerializeField] private float detectionRadius = 0.5f;
    //[SerializeField] private LayerMask collisionMask;
    public StateMachine PlayerStateMachine => playerStateMachine;
    public Animator PlayerAnimator => anim;
    public CharacterDetail CrtDetail => characterDetail;
    public Vector3 PlayerVelocity => playerVelocity;
    public Vector3 MoveDir => moveDir;
    public bool GroundPlayer => groundedPlayer;
    //public CloseToGround checkCloseToGround => closeToGround;
    public CheckHit CheckHit => checkH;
    public DeadpoolSound dpS => deadpoolSound;

    public void SetPlayerVelocity(float f)
    {
        playerVelocity.y = f;
    }
    public void SetCurrentSpeed(float speed)
    {
        currentSpeed = speed;
    }
    private void Awake()
    {
        playerStateMachine = new StateMachine(this);
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        currentSpeed = characterDetail.speed;
        mainCamera = Camera.main;
        characterDetail.currentHealth = characterDetail.health; 
        healthBar.GetComponent<HealthBar>().Initialize(characterDetail.health);
        //closeToGround = checkGround.GetComponent<CloseToGround>();
        checkH = checkHit.GetComponent<CheckHit>();
        if(playerData.currentDamePoint != 1) characterDetail.dame = 20 + (playerData.currentDamePoint-1) * 20;
        if(playerData.currentHealthPoint != 1) characterDetail.health = 100 + (playerData.currentHealthPoint-1) * 20;
        if(playerData.currentRecoveryPoint != 1) characterDetail.healthRecover = 5 + (playerData.currentRecoveryPoint-1) * 5;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerStateMachine.Initialize(playerStateMachine.idleState);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 300f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isCloseToGround = true;
            }
            else
            {
                isCloseToGround = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        playerStateMachine.Update();
    }

    private void LateUpdate()
    {
        //Move();
        
    }

    public void Move()
    {

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * Time.deltaTime * currentSpeed);
        }
        Gravity();
    }
    public void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime * characterDetail.fallSpeed;
        controller.Move(playerVelocity * Time.deltaTime * characterDetail.jumpSpeed);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }
    public void Jump()
    {
        // Changes the height position of the player..
        playerVelocity.y += Mathf.Sqrt(characterDetail.jumpHeight * -3.0f * gravityValue);
    }

    public void Roll()
    {
        StartCoroutine(IRoll());
    }

    IEnumerator IRoll()
    {
        float startTime = Time.time;
        while(Time.time < startTime + rollTime)
        {
            controller.Move(moveDir * rollSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void TakeDame(int dame)
    {
        characterDetail.currentHealth -= dame;
        if (characterDetail.currentHealth <= 0) characterDetail.currentHealth = 0;
        healthBar.GetComponent<HealthBar>().SetHealth(characterDetail.currentHealth);
    }
    public void HealthRecover()
    {
        characterDetail.currentHealth += characterDetail.healthRecover;
        if (characterDetail.currentHealth >= characterDetail.health) characterDetail.currentHealth = characterDetail.health;
        healthBar.GetComponent<HealthBar>().SetHealth(characterDetail.currentHealth);

    }
    public void UpgradeHealth()
    {
        healthBar.GetComponent<HealthBar>().UpgradeHealth(characterDetail.health,characterDetail.currentHealth);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(hit.gameObject.name);
    }
    // Check collider manually
    //public void CheckCollison()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, collisionMask);

    //    foreach (var hitCollider in hitColliders)
    //    {
    //        Debug.Log("Character collided with: " + hitCollider.gameObject.name);

    //        // Logic xử lý va chạm
    //        if (hitCollider.CompareTag("Wall"))
    //        {
    //            Debug.Log("Character hit a wall!");
    //        }
    //    }
    //}
    private void OnDrawGizmosSelected()
    {
        // Check collider manually
        //Gizmos.DrawWireSphere(transform.position,detectionRadius);
        //Vector3 pos = transform.position;
        //pos.y -= 90;
        //Gizmos.DrawLine(transform.position,pos);
        //Gizmos.color = Color.red;
    }


}
