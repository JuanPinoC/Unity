using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player){
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == player){
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if(m_IsPlayerInRange){
            Vector3 direction = player.position - transform.position + Vector3.up; 
            // player position minus point of view position plus 1 unit up

            Ray ray = new Ray (transform.position, direction);
            //A Ray is a line starting from a point (PointOfView) toward a direction

            RaycastHit raycastHit; //out parameter

            if(Physics.Raycast(ray, out raycastHit)){
                if(raycastHit.collider.transform == player){
                    gameEnding.CaughtPlayer ();
                }
            }

        }
    }


}
