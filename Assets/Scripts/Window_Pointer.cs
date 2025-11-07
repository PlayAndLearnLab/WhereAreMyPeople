using UnityEngine;
//using CodeMonkey.Utils;

public class Window_Pointer : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    //private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake() {
        //targetPosition = new Vector3(target.transform.position);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update() {
        Vector3 toPosition = target.transform.position;
        // Debug.Log("toPosition ok");
        Vector3 fromPosition = player.transform.position;
        // Debug.Log("fromPosition ok");
        fromPosition.y = 0f;
        toPosition.y = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        //Debug.Log(dir);
        //Vector3 zero1 = new Vector3(1,0,0);
        //Vector3 zero2 = player.transform.eulerAngles;
        Vector3 zero = (player.transform.forward).normalized;
        zero.y = 0;
        // Debug.Log(zero);
        
        //zero.y = 0f;
        float angle = GetAngle(zero,dir);
        // Debug.Log(angle);
        //if ((zero.x < dir.x && zero.z>dir.z) || (zero.x>dir.x && zero.z<dir.z)){
        //    angle = -angle;
        //}
        
        //float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0,0,angle+90);

        LayerMask defaultLayer = LayerMask.GetMask("Default");
        LayerMask entityLayer = LayerMask.GetMask("Entity");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(player.transform.position, target.transform.position, out hit, Mathf.Infinity, defaultLayer))

        { 
            // Debug.DrawRay(player.transform.position, target.transform.position * hit.distance, Color.yellow); 
            //Debug.Log("Did Hit"); 
        }
        if (Physics.Raycast(player.transform.position, target.transform.position, out hit, Mathf.Infinity, entityLayer))
        { 
            // Debug.DrawRay(player.transform.position, target.transform.position * hit.distance, Color.red); 
            //Debug.Log("Did Hit"); 
        }

        

    }

    private static float GetAngle(Vector3 v1, Vector3 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.z - v1.z * v2.x);
        return Vector3.Angle(v1, v2) * sign;
    }
}
