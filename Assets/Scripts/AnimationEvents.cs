using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private Player player;

    public void EndAnimation() {
        Debug.Log("end animation");
        player.isInAnimation = false;
        Debug.Log("isInAnimation = " + player.isInAnimation);
    }
}
