using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager current;

    [SerializeField] private Player player;

    public Vector2 direction;
    /*public ReputationBar reputationBar;

    private int nbQuests = 5;
    private int catScore = 75;
    private int bonusScore = 25;

    public Animator anim;*/

    private void Awake() {
        current = this;
    }

    private void Start() {
        direction = Vector2.zero;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) direction.y = 1;
        if (Input.GetKey(KeyCode.S)) direction.y = -1;
        if (Input.GetKey(KeyCode.Q)) direction.x = -1;
        if (Input.GetKey(KeyCode.D)) direction.x = 1;

        if (Input.GetKeyUp(KeyCode.Z)) direction.y = 0;
        if (Input.GetKeyUp(KeyCode.S)) direction.y = 0;
        if (Input.GetKeyUp(KeyCode.Q)) direction.x = 0;
        if (Input.GetKeyUp(KeyCode.D)) direction.x = 0;

        if (Input.GetKeyDown(KeyCode.Space)) player.Jump();

        if (Input.GetKeyDown(KeyCode.E)) player.Interact();
        if (Input.GetKeyDown(KeyCode.A)) UiManager.current.OpenOngoingQuestList();

        /*if (Input.GetKeyDown(KeyCode.M)) reputationBar.SetMaxReputation(nbQuests * (catScore + bonusScore));
        if (Input.GetKeyDown(KeyCode.RightArrow)) reputationBar.AddReputation(catScore + bonusScore); //chat rapporté avec bonus
        if (Input.GetKeyDown(KeyCode.LeftArrow)) reputationBar.AddReputation(catScore); //chat rapporté sans bonus

        if (Input.GetKey(KeyCode.UpArrow)) anim.SetBool("isRunning", true);
        if (Input.GetKeyUp(KeyCode.UpArrow)) anim.SetBool("isRunning", false);

        if (Input.GetKey(KeyCode.DownArrow)) anim.SetBool("isCarryRunning", true);
        if (Input.GetKeyUp(KeyCode.DownArrow)) anim.SetBool("isCarryRunning", false);

        if (Input.GetKeyUp(KeyCode.M)) anim.SetTrigger("pickUpTrigger");
        if (Input.GetKeyUp(KeyCode.P)) anim.SetTrigger("letGoTrigger");
        if (Input.GetKeyUp(KeyCode.L)) anim.SetTrigger("callTrigger");
        if (Input.GetKeyUp(KeyCode.K)) anim.SetTrigger("caressTrigger");
        if (Input.GetKeyUp(KeyCode.O)) anim.SetTrigger("playTrigger");

        if (Input.GetKey(KeyCode.Z)) anim.SetBool("isRunning", true);
        if (Input.GetKey(KeyCode.S)) anim.SetBool("isRunning", true);
        if (Input.GetKey(KeyCode.Q)) anim.SetBool("isRunning", true);
        if (Input.GetKey(KeyCode.D)) anim.SetBool("isRunning", true);

        if (Input.GetKeyUp(KeyCode.Z)) anim.SetBool("isRunning", false);
        if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("isRunning", false);
        if (Input.GetKeyUp(KeyCode.Q)) anim.SetBool("isRunning", false);
        if (Input.GetKeyUp(KeyCode.D)) anim.SetBool("isRunning", false);

        if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("jumpTrigger");*/
    }

}
