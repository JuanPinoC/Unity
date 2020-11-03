using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource>();

    }

    // Physics and operations
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();


        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        if(isWalking){
           if(!m_AudioSource.isPlaying){
               m_AudioSource.Play();
            }
        } else {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = 
                            Vector3.RotateTowards(
                                transform.forward,          //from
                                m_Movement,                 //towards
                                turnSpeed * Time.deltaTime, //change angle in radians
                                0f                          //magnitude
                            );
        // Time delta: Time between frames / time since the previous frame

        m_Rotation = Quaternion.LookRotation(desiredForward);
        //Quaternions are a way of storing rotations
        //LookRotation creates a rotation given the vector

    }

    // Allows to apply root motion as desired, so movement and rotation can be applied separately
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        /*
            m_Animator.deltaPosition is the change in position due to root motion
            that would applied to this frame but we take the magnitude of it and 
            multiply it by the movement vector.
        */
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
