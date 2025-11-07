using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    Vector3 startPos_cylinder;
    public Transform transform_cylinder;
    public float speed = 10.0f;
    private Animator animator;
    Rigidbody rb;
    private bool canMove = true;

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
    public GameObject Ask;
    public GameObject Take;
    public GameObject Pointer;
    public GameObject ExitMessage;
    public GameObject GotCaughtMessage;

    public GameObject Entity1Scan;
    private bool entity1visited = false;
    private bool entity2visited = false;
    private bool entity3visited = false;
    private bool entity4visited = false;
    private bool entity5visited = false;
    private bool entity6visited = false;

    private bool visitedHuman1 = false;
    private bool visitedHuman2 = false;
    private bool visitedHuman3 = false;

    private bool pointerCoroutineOn = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
     
    private void Awake()
    {
        startPos_cylinder = transform_cylinder.position;

    }

    

    // Update is called once per frame
    private void Update()
    {
        //MoveLeftRight();
        // MoveForwardBack();
        // MoveRotate();

        MoveForwardBackAndRotate();

        // MoveFB();
        // MoveLR();
        Turn();

        if (Input.GetKeyDown(KeyCode.H)){
            if (Pointer.activeSelf == false && pointerCoroutineOn == false){
                pointerCoroutineOn = true;
                StartCoroutine(pointer());
            }       
        }


        if (Input.GetKeyDown(KeyCode.Return)){
            Debug.Log(true);
            Resume();
            canMove = true;
            if (Ask.activeSelf == true){
                Ask.SetActive(false);
                Resume(); 
                if (SceneManager.GetActiveScene().name == "Stage2"){
                    SceneManager.LoadScene(13);

                }
                // to do : make it so if we are at Stage 2 then it loads the scene but if not then don't load it
                // SceneManager.LoadScene(13);               
            }

            if (Take.activeSelf == true){   
                if (SceneManager.GetActiveScene().name == "Stage3"){
                    SceneManager.LoadScene(24);
                } 

                if (SceneManager.GetActiveScene().name == "Stage4"){
                    SceneManager.LoadScene(49);
                }           
            }

            if (ExitMessage.activeSelf == true){   
                if (SceneManager.GetActiveScene().name == "Runaway1"){
                    SceneManager.LoadScene(66);
                }
                if (SceneManager.GetActiveScene().name == "Runaway2"){
                    SceneManager.LoadScene(0);
                }           
            }

            if (GotCaughtMessage.activeSelf == true){   
                if (SceneManager.GetActiveScene().name == "Runaway1"){
                    SceneManager.LoadScene(0);
                }
                if (SceneManager.GetActiveScene().name == "Runaway2"){
                    SceneManager.LoadScene(0);
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

    IEnumerator pointer(){
        Pointer.SetActive(true);
        yield return new WaitForSeconds(5);
        Pointer.SetActive(false);
        yield return new WaitForSeconds(5);
        pointerCoroutineOn = false;
    }

    

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Monster")){
    
            canMove = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            GotCaughtMessage.SetActive(true);
            Pause();
            
        }

        if (collision.CompareTag("exit")){
            if (collision.gameObject.name == "DoorExit")
            {
                // Debug.Log("true human 4");
                // print("true human 4");
                canMove = false;

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                ExitMessage.SetActive(true);
                Pause();
            }

        }

        if (collision.CompareTag("Entity"))
        {
            if (collision.gameObject.name == "Human1" && visitedHuman1 == false)
            {
                visitedHuman1 = true;
                // Debug.Log("true human 1");
                // print("true human 1");
                canMove = false;

                // Stop Human1's movement
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                Ask.SetActive(true);
                Pause();
                Invoke("Resume", 2f);
            }

            if (collision.gameObject.name == "Human2" && visitedHuman2 == false)
            {
                visitedHuman2 = true;
                // Debug.Log("true human 2");
                // print("true human 2");
                canMove = false;

                // Stop Human1's movement
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                Ask.SetActive(true);
                Pause();
                Invoke("Resume", 2f);
            }


            if (collision.gameObject.name == "Human3")
            {
                if (SceneManager.GetActiveScene().name == "Stage3"){
                    // Debug.Log("true human 4");
                    // print("true human 4");
                    canMove = false;

                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    Take.SetActive(true);
                    Pause();
                }

                if (SceneManager.GetActiveScene().name == "Stage4"){
                    visitedHuman3 = true;
                    // Debug.Log("true human 3");
                    // print("true human 3");
                    canMove = false;

                    // Stop Human1's movement
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    Ask.SetActive(true);
                    Pause();
                    Invoke("Resume", 2f);
                }
            }

            if (collision.gameObject.name == "Human4")
            {
                // Debug.Log("true human 4");
                // print("true human 4");
                canMove = false;

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                Take.SetActive(true);
                Pause();
            }

            
        }

        

    }

    private void Pause()
    {
        Time.timeScale = 0; // Stop time
        // Debug.Log("Game Paused");
    }

    private void Resume()
    {
        Time.timeScale = 1; // Resume time
        // Debug.Log("Game Resumed");
    }

    

    // Note : faire Horizontal et Vertical en meme temps pour avanver -> pour tourner utiliser z et c par exemple.
    // Donc trois fonctions : une pour avancer/reculer, une pour tourner et une pour avancer et tourner en meme temps

    void MoveFB()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Input for movement (forward/backward)
        float moveInputF = Input.GetAxis("Vertical");
        // Debug.Log(moveInputF);

        // Calculate forward movement
        // Vector3 forwardMovementF = transform_cylinder.transform.forward * moveInputF * 15.0f;
        Vector3 forwardMovementF = rb.transform.forward * moveInputF * speed;
        //Vector3 forwardMovement = rb.transform.forward * moveInput * 10.0f;
        // Debug.Log(forwardMovementF);

        // Apply movement using Rigidbody velocity
        rb.linearVelocity = new Vector3(forwardMovementF.x, rb.linearVelocity.y, forwardMovementF.z);
        // Debug.Log(rb.linearVelocity);

        // Update animator's ForwardSpeed parameter for visualization
        animator.SetFloat("ForwardSpeed", Mathf.Abs(moveInputF));
    }

    void MoveLR()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Input for move and rotating (left/right)
        float moveInput = Input.GetAxis("Horizontal");

        // Calculate side movement
        Vector3 forwardMovement = rb.transform.right * moveInput * speed;

        // transform.Rotate(Vector3.forwardMovement, 200.0f * Time.deltaTime, Space.Self);
        // transform.Translate(Vector3.forwardMovement * 10 * Time.deltaTime, Space.World);

        // Apply movement using Rigidbody velocity
        // rb.linearVelocity = new Vector3(forwardMovement.x, rb.linearVelocity.y, forwardMovement.z);
        // float rotationSpeed = moveInput * 200.0f; // Rotation speed
        // Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        // rb.MoveRotation(rb.rotation * deltaRotation);

        // Update animator's ForwardSpeed parameter for visualization
        animator.SetFloat("ForwardSpeed", Mathf.Abs(moveInput));
    }

    void Turn()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Input for rotation (left/right)
        float rotateInput = Input.GetAxis("Rotate");

        // Calculate rotation
        float rotationSpeed = rotateInput * 100.0f; // Rotation speed
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void MoveForwardBackAndRotate()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Input for movement (forward/backward)
        float moveInput = Input.GetAxis("Vertical");
        //Debug.Log(moveInput);

        // Input for rotation (left/right)
        float rotateInput = Input.GetAxis("Horizontal");

        // Apply rotation
        float rotationSpeed = rotateInput * 200.0f; // Rotation speed
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Calculate forward movement in the current forward direction of the Rigidbody
        Vector3 forwardMovement = rb.transform.forward * moveInput * speed;

        // Apply forward movement using Rigidbody velocity
        rb.linearVelocity = new Vector3(forwardMovement.x, rb.linearVelocity.y, forwardMovement.z);

        // Update animator's ForwardSpeed parameter for visualization
        if (animator != null)
        {
            animator.SetFloat("ForwardSpeed", Mathf.Abs(moveInput));

            // Reset ForwardSpeed to 0 if there is no movement input
            if (Mathf.Approximately(moveInput, 0f))
            {
                animator.SetFloat("ForwardSpeed", 0f);
            }
        }
    }

































    //////////////////////////////////////////////////
    // POUBELLE

    void MoveForwardBackAndRotate2()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Input for movement (forward/backward)
        float moveInput = Input.GetAxis("Vertical");

        // Input for rotation (left/right)
        float rotateInput = Input.GetAxis("Horizontal");

        // Calculate forward movement
        Vector3 forwardMovement = transform_cylinder.transform.forward * moveInput * 15.0f;

        // Apply movement using Rigidbody velocity
        rb.linearVelocity = new Vector3(forwardMovement.x, rb.linearVelocity.y, forwardMovement.z);

        // Calculate rotation
        float rotationSpeed = rotateInput * 150.0f;

        // Apply rotation using Rigidbody
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Update animator's ForwardSpeed parameter for movement visualization
        animator.SetFloat("ForwardSpeed", Mathf.Abs(moveInput));
    }

    void MoveForwardBack()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Get input for movement
        Vector3 vec_forward = Vector3.zero;
        vec_forward.z = Input.GetAxis("Vertical");

        // Calculate movement direction in local space
        Vector3 movement = transform_cylinder.TransformDirection(vec_forward) * 15.0f;

        // Apply force to the Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Update animator's ForwardSpeed parameter
        animator.SetFloat("ForwardSpeed", Mathf.Abs(vec_forward.z));
    }


    void MoveRotate()
    {
        Vector3 vec_rotate = Vector3.zero;
        vec_rotate.y = Input.GetAxis("Rotate");
        Vector3 v = new Vector3(0.0f, vec_rotate.y, 0.0f) * Time.deltaTime * 15.0f;
        transform_cylinder.Rotate(v, Space.Self);
        //animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity));
    }




    void MoveForwardBack2()
    {
        Vector3 vec_forward = Vector3.zero;
        vec_forward.z = Input.GetAxis("Vertical");
        Vector3 v = new Vector3(0.0f, 0.0f, vec_forward.z) * Time.deltaTime * 15.0f;
        //transform_cylinder.Translate(v, Space.Self);
        transform_cylinder.position += transform_cylinder.TransformDirection(v);
        //animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity)*1000000);
        animator.SetFloat("ForwardSpeed",Mathf.Abs(vec_forward.z));
        //Debug.Log(vec_forward.z);
    }


    void MoveLeftRight()
    {
        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Get input for rotation
        float rotateInput = Input.GetAxis("Horizontal");

        // Calculate rotation speed
        float rotationSpeed = rotateInput * 150.0f;

        // Apply rotation using Rigidbody
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Update animator's parameter (if needed)
        animator.SetFloat("ForwardSpeed", Mathf.Abs(rotateInput));
    } 

    void MoveLeftRight4()
    {
        Vector3 vec_rotate = Vector3.zero;
        vec_rotate.y = Input.GetAxis("Horizontal");
        Vector3 v = new Vector3(0.0f, vec_rotate.y, 0.0f) * Time.deltaTime * 150.0f;
        transform_cylinder.Rotate(v, Space.Self);
        //animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity));

        // Ensure you have a Rigidbody component attached
        Rigidbody rb = transform_cylinder.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the cylinder!");
            return;
        }

        // Get input for movement
        Vector3 vec_forward = Vector3.zero;
        vec_forward.y = Input.GetAxis("Horizontal");

        // Calculate movement direction in local space
        Vector3 movement = transform_cylinder.TransformDirection(vec_forward) * 15.0f;

        // Apply force to the Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Update animator's ForwardSpeed parameter
        animator.SetFloat("ForwardSpeed", Mathf.Abs(vec_forward.y));
    }  

    void MoveLeftRight2()
    {
        Vector3 vec_left = Vector3.zero;
        vec_left.x = Input.GetAxis("Horizontal");
        Vector3 v = new Vector3(vec_left.x, 0.0f, 0.0f) * Time.deltaTime * 15.0f;
        //transform_cylinder.Translate(v, Space.Self);
        transform_cylinder.position += transform_cylinder.TransformDirection(v);
        //animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity)*1000000);
        //Debug.Log(Vector3.Magnitude(rb.linearVelocity)*1000000);
        animator.SetFloat("ForwardSpeed", Mathf.Abs(vec_left.x));
        // Debug.Log(vec_left.x);
    }

    void MoveLeftRight3()
    {
        Vector3 vec_rotate = Vector3.zero;
        vec_rotate.y = Input.GetAxis("Horizontal");
        Vector3 v = new Vector3(0.0f, vec_rotate.y, 0.0f) * Time.deltaTime * 150.0f;
        transform_cylinder.Rotate(v, Space.Self);
        //animator.SetFloat("ForwardSpeed", Vector3.Magnitude(rb.linearVelocity));
    }

}
