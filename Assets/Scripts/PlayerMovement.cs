using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float MoveForce;
    [SerializeField] Camera cam;

    [SerializeField] Animator PlayerAnim;
    void Update()
    {
        MoveCharacter();   
    }

    public void MoveCharacter()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.forward * MoveForce * Time.deltaTime);

            Vector3 inputPosition = Input.mousePosition;
            Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
            Vector2 offset = new Vector2(inputPosition.x - screenPoint.x, inputPosition.y - screenPoint.y);
            float TargetAngle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, TargetAngle, 0);
            PlayerAnim.SetBool("isRunning", true);
        }
        else
        {
            PlayerAnim.SetBool("isRunning", false);
        }
    }



   
}
