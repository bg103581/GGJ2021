using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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

    [HideInInspector] public bool objectIsInteractable = false;
    [HideInInspector] public bool isCatCarried = false;
    [HideInInspector] public GameObject interactableObjectNear;
    [HideInInspector] public Quest quest = null;

    private GameObject carriedCat = null;
    private Rigidbody rb;
    private Vector2 dir;
    private Vector3 moveDir;

    private bool isInCatInteractMenu = false;
    private bool isInQuestInteractMenu = false;

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
        GameEvents.current.onCaressButtonTrigger += Caress;
        GameEvents.current.onPlayButtonTrigger += Play;
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
        GameEvents.current.onCaressButtonTrigger -= Caress;
        GameEvents.current.onPlayButtonTrigger -= Play;
    }
    #endregion

    #region Methods
    private void Walk() {
        if (isInCatInteractMenu || isInQuestInteractMenu)
            rb.velocity = Vector3.zero;
        else
            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
    }

    public void Jump() {
        if (isGrounded && !isInCatInteractMenu) {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += Vector3.up * jumpVelocity;
        }
    }

    private void Rotate() {
        if (dir.magnitude >= 0.1f && !(isInCatInteractMenu || isInQuestInteractMenu)) {
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
        if (!isCatCarried) {

            if (objectIsInteractable && interactableObjectNear != null) {

                switch (interactableObjectNear.tag) {

                    case ("Cat"):

                        if (!isInCatInteractMenu) {
                            ShowCatInteraction(true);
                        } else {
                            ShowCatInteraction(false);
                        }

                        break;

                    case ("Quest"):
                        quest = interactableObjectNear.GetComponent<Quest>();

                        if (!isInQuestInteractMenu) {
                            ShowQuestInteraction(true, quest);
                        } else {
                            ShowQuestInteraction(false);
                        }

                        break;

                    default:
                        break;
                }
            } else {
                objectIsInteractable = false;
            }
        } else {
            if (!isInCatInteractMenu) {
                ShowCatInteraction(true);
            } else {
                ShowCatInteraction(false);
            }
        }
    }

    public void PickUpCat() {
        UiManager.current.ShowCatInteractionCanvas(false);
        isInCatInteractMenu = false;
        ResumeCinemachine();

        carriedCat = interactableObjectNear;
        isCatCarried = true;
        Image interactionButton = carriedCat.GetComponent<InteractionButton>().interactionButton;
        UiManager.current.ShowInteractionButton(interactionButton, false);

        carriedCat.transform.SetParent(catPos);
        carriedCat.transform.DOLocalMove(Vector3.zero, 1f);
        carriedCat.transform.DOLocalRotate(Vector3.zero, 1f);

        UiManager.current.EnablePickUpButton(false);
    }

    public void LetGoCat() {
        UiManager.current.ShowCatInteractionCanvas(false);
        isInCatInteractMenu = false;
        ResumeCinemachine();

        isCatCarried = false;
        //move to ground
        carriedCat.transform.DOMove(groundPos.position, 1f);
        //setparent null
        carriedCat.transform.SetParent(null);

        if (interactableObjectNear.CompareTag("House")) {
            Cat cat = carriedCat.GetComponent<Cat>();
            QuestManager.instance.FinishCurrentQuest(interactableObjectNear, cat);
        }

        carriedCat = null;

        UiManager.current.EnablePickUpButton(true);

    }

    public void Caress() {

    }

    public void Play() {

    }

    public void ShowQuestInteraction(bool show, Quest quest = null) {
        UiManager.current.ShowQuestCanvas(show, quest);
        isInQuestInteractMenu = show;

        if (show)
            StopCinemachine();
        else
            ResumeCinemachine();
    }
    public void ShowCatInteraction(bool show) {
        UiManager.current.ShowCatInteractionCanvas(show);
        isInCatInteractMenu = show;

        if (show)
            StopCinemachine();
        else
            ResumeCinemachine();
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
