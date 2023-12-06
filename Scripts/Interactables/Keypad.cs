using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField] private GameObject door;
    private bool doorOpen;
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = door.GetComponent<Animator>();
        doorOpen = false;
    }

    protected override void Interact()
    {
        doorOpen = !doorOpen;
        doorAnimator.SetBool("IsOpen", doorOpen);
    }
}
