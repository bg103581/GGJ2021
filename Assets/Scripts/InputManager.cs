using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager current;

    [SerializeField] private Player player;

    public Vector2 direction;

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
        if (Input.GetKeyDown(KeyCode.A)) UiManager.current.OpenOngoingQuest();
    }

}
