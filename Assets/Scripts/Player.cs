using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float speed;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] private Transform catPos;
    [SerializeField] private Transform groundPos;
    [SerializeField] private CinemachineFreeLook cinemachine;

    [HideInInspector] public bool canInteract = false;
    [HideInInspector] public string interactableObjectName;
    [HideInInspector] public GameObject interactableObjectNear;

    private Rigidbody rb;
    private Vector2 dir;
    private Vector3 moveDir;
    private bool isCatCarried;
    private bool isInInteractMenu;
    private float cinemachineYSpeed;
    private float cinemachineXSpeed;

    [HideInInspector] public bool isGrounded = true;

    #region MonoBehaviour
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cinemachineYSpeed = cinemachine.m_YAxis.m_MaxSpeed;
        cinemachineXSpeed = cinemachine.m_XAxis.m_MaxSpeed;

        GameEvents.current.onPickUpButtonTrigger += PickUpCat;
        GameEvents.current.onLetGoButtonTrigger += LetGoCat;
    }

    private void Start() {
        rb.velocity = Vector3.zero;
    }

    private void Update() {
        dir = InputManager.current.direction.normalized;
        Rotate();
    }

    void FixedUpdate() {
        Walk();

        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnDestroy() {
        GameEvents.current.onPickUpButtonTrigger -= PickUpCat;
        GameEvents.current.onLetGoButtonTrigger -= LetGoCat;
    }
    #endregion

    #region Methods
    private void Walk() {
        if (isInInteractMenu)
            rb.velocity = Vector3.zero;
        else
            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
    }

    public void Jump() {
        if (isGrounded && !isInInteractMenu) {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += Vector3.up * jumpVelocity;
        }
    }

    private void Rotate() {
        if (dir.magnitude >= 0.1f && !isInInteractMenu) {
            float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else {
            moveDir = Vector3.zero;
        }
    }

    public void Interact() {
        if (canInteract) {
            switch (interactableObjectName) {
                case ("Cat"):
                    if (isInInteractMenu) {
                        UiManager.current.HideCatInteractionCanvas();
                        isInInteractMenu = false;
                        ResumeCinemachine();
                    }
                    else {
                        UiManager.current.ShowCatInteractionCanvas();
                        isInInteractMenu = true;
                        StopCinemachine();
                    }

                    if (isCatCarried) UiManager.current.DisablePickUpButton();
                    else UiManager.current.EnablePickUpButton();
                    
                    break;

                case ("Quest"):
                    isInInteractMenu = true;
                    StopCinemachine();
                    break;

                case ("House"):
                    break;

                default:
                    break;
            }
        }
    }

    public void PickUpCat() {
        UiManager.current.HideCatInteractionCanvas();
        isInInteractMenu = false;
        ResumeCinemachine();

        isCatCarried = true;
        UiManager.current.CloseInteractionButton(interactableObjectNear.GetComponent<InteractionButton>().interactionButton);
        interactableObjectNear.transform.SetParent(catPos);
        interactableObjectNear.transform.DOLocalMove(Vector3.zero, 1f);
        interactableObjectNear.transform.DOLocalRotate(Vector3.zero, 1f);
    }

    public void LetGoCat() {
        UiManager.current.HideCatInteractionCanvas();
        isInInteractMenu = false;
        ResumeCinemachine();

        isCatCarried = false;
        //move to ground
        interactableObjectNear.transform.DOMove(groundPos.position, 1f);
        //setparent null
        interactableObjectNear.transform.SetParent(null);
    }

    private void StopCinemachine() {
        cinemachine.m_YAxis.m_MaxSpeed = 0f;
        cinemachine.m_XAxis.m_MaxSpeed = 0f;
    }

    private void ResumeCinemachine() {
        cinemachine.m_YAxis.m_MaxSpeed = cinemachineYSpeed;
        cinemachine.m_XAxis.m_MaxSpeed = cinemachineXSpeed;
    }
    #endregion
}
