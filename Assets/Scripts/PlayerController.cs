using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private PlayerController Controller;
    public Camera cam;
    public NavMeshAgent agent;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<PlayerController>();
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //move the player
                agent.SetDestination(hit.point);
                animator.SetFloat("ForwardSpeed", agent.velocity.magnitude);
                // Debug.Log("l'objet avance");

            }

        }
        
        //Vector2 input = Context.
        /*
        Vector3 viewDir = agent.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3 inputDir = transform.orientation.forward * verticalInput + transform.orientation.right * horizontalInput;

        
        float Horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 Movement = cam.transform.right * Horizontal + cam.transform.forward * Vertical;
        Movement.y = 0f;



        Controller.Move(Movement);
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * cam.GetComponent<CameraController>().sensivity * Time.deltaTime);


            Quaternion CamRotation = cam.transform.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }*/

        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude/agent.speed);
        print(agent.velocity.magnitude);

    }

}


   