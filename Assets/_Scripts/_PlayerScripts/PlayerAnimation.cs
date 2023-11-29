using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Animation Parameters
    private const string MOVEMENT_PARAM = "Movement";

    [Header("Class Reference")]
    [SerializeField] private PlayerController player;

    [Header("Animation")]
    [SerializeField] private Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(MOVEMENT_PARAM, player.movementValue);
    }
}
