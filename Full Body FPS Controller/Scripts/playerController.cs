using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class playerController : MonoBehaviour
{
    // Player Movement Variables
    [Header("Player Movement Variables")]
    CharacterController characterController;
    Animator animator;
    Vector3 rootMotion;
    Vector2 input;
    int isRunningParameters = Animator.StringToHash("isRunning");

    //Audio System
    [Header("Audio System")]
    public AudioSource audioSource;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void onAnimationMovement()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        characterController.Move(rootMotion);
        rootMotion = Vector3.zero;
    }

    void HandleMovement()
    {
        // Character Movement
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        //running state
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool(isRunningParameters, true);
        }
        else
        {
            animator.SetBool(isRunningParameters, false);
        }


    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                //AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(characterController.center), FootstepAudioVolume);
                audioSource.PlayOneShot(FootstepAudioClips[index], FootstepAudioVolume);
            }
        }
    }

}
