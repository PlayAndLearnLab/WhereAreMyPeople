using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    //public UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("UI")]
    public GameObject eye_green;
    public GameObject nose_green;
    public GameObject hair_green;
    public GameObject eye_orange;
    public GameObject nose_orange;
    public GameObject hair_orange;

    public GameObject Entity1Check;
    public GameObject Entity2Check;
    public GameObject Entity3Check;
    public GameObject Entity4Check;
    public GameObject Entity5Check;
    public GameObject Entity6Check;
    private bool canMove = true;
    private bool entity1visited = false;
    private bool entity2visited = false;
    private bool entity3visited = false;
    private bool entity4visited = false;
    private bool entity5visited = false;
    private bool entity6visited = false;
    

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        if (canMove){
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

            MyInput();
            SpeedControl();

            // handle drag
            if (grounded)
                rb.linearDamping = groundDrag;
                //Debug.Log("grounded");
            else
                rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.Return)){
            Resume();
            canMove = true;
            if (Entity1Check.activeSelf == true){
                Entity1Check.SetActive(false);
                if (entity4visited == false){
                    eye_orange.SetActive(true);
                    eye_green.SetActive(false);
                } else {
                    eye_orange.SetActive(false);
                    eye_green.SetActive(true);
                }
                
            }
            if (Entity2Check.activeSelf == true){
                Entity2Check.SetActive(false);
                if (entity5visited == false){
                    nose_orange.SetActive(true);
                    nose_green.SetActive(false);
                } else {
                    nose_orange.SetActive(false);
                    nose_green.SetActive(true);
                }
            }
            if (Entity3Check.activeSelf == true){
                Entity3Check.SetActive(false);
                if (entity6visited == false){
                    hair_orange.SetActive(true);
                    hair_green.SetActive(false);
                } else {
                    hair_orange.SetActive(false);
                    hair_green.SetActive(true);
                }
            }
            if (Entity4Check.activeSelf == true){
                Entity4Check.SetActive(false);
                if (entity1visited == false){
                    eye_orange.SetActive(true);
                    eye_green.SetActive(false);
                } else {
                    eye_orange.SetActive(false);
                    eye_green.SetActive(true);
                }
            }
            if (Entity5Check.activeSelf == true){
                Entity5Check.SetActive(false);
                if (entity2visited == false){
                    nose_orange.SetActive(true);
                    nose_green.SetActive(false);
                } else {
                    nose_orange.SetActive(false);
                    nose_green.SetActive(true);
                }
            }
            if (Entity6Check.activeSelf == true){
                Entity6Check.SetActive(false);
                if (entity3visited == false){
                    hair_orange.SetActive(true);
                    hair_green.SetActive(false);
                } else {
                    hair_orange.SetActive(false);
                    hair_green.SetActive(true);
                }
            }

            if ((entity1visited == true) && (entity2visited == true) && (entity3visited == true) && (entity4visited == true) && (entity5visited == true) && (entity6visited == true)) {
                SceneManager.LoadScene(4);
            }

            if (Input.GetKeyDown(KeyCode.G)){
                SceneManager.LoadScene(4);
            }

            
            
        }
    }

    private void FixedUpdate()
    {
        if (canMove){
            MovePlayer();
        
        
            //transform.position.y = 0;
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

            MyInput();
            SpeedControl();

            // handle drag
            if (grounded)
                rb.linearDamping = groundDrag;
                //Debug.Log("grounded");
            else
                rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.Return)){
            Resume();
            canMove = true;
            if (Entity1Check.activeSelf == true){
                Entity1Check.SetActive(false);
            }
            if (Entity2Check.activeSelf == true){
                Entity2Check.SetActive(false);
            }
            
        }

        if ((entity1visited == true) && (entity2visited == true) && (entity3visited == true) && (entity4visited == true) && (entity5visited == true) && (entity6visited == true)) {
            SceneManager.LoadScene(4);
        }

        if (Input.GetKeyDown(KeyCode.G)){
            SceneManager.LoadScene(4);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //print(grounded);

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            //animator.SetFloat("ForwardSpeed", agent.velocity.magnitude);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            //animator.SetFloat("ForwardSpeed", agent.velocity.magnitude);
        
        animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity));
        //Debug.Log(agent.velocity.magnitude/agent.speed);
        //Debug.Log(Vector3.Magnitude(rb.linearVelocity));
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Entity"))
        {
            if ((collision.gameObject.name == "Entity1") && (this.entity1visited == false)){
                // Debug.Log(true);
                // print("true 1");
                this.entity1visited = true;
                //Destroy(collision.GameObject);
                canMove = false;
                
                Entity1Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.name == "Entity2") && (this.entity2visited == false)){
                // Debug.Log(true);
                // print("true 2");
                this.entity2visited = true;
                //Destroy(collision.GameObject);
                canMove = false;
                
                Entity2Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.name == "Entity3") && (this.entity3visited == false)){
                
                this.entity3visited = true;
                canMove = false;               
                Entity3Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.name == "Entity4") && (this.entity4visited == false)){
                
                this.entity4visited = true;
                canMove = false;               
                Entity4Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.name == "Entity5") && (this.entity5visited == false)){
                
                this.entity5visited = true;
                canMove = false;               
                Entity5Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.name == "Entity6") && (this.entity6visited == false)){
                
                this.entity6visited = true;
                canMove = false;               
                Entity6Check.SetActive(true);
                Pause();
                Destroy(collision.gameObject);
            }
            
        }

    }

    private void Pause()
    {
        Time.timeScale = 0; // Stop time
        Debug.Log("Game Paused");
    }

    private void Resume()
    {
        Time.timeScale = 1; // Resume time
        Debug.Log("Game Resumed");
    }
}