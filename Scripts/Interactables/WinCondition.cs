using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCondition : Interactable
{
    public GameObject player;
    private PlayerUI playerUI;
    public GameObject winObject;

    private void Awake()
    {
        playerUI = player.GetComponent<PlayerUI>();
    }

    protected override void Interact()
    {
        winObject.SetActive(false);
        playerUI.Win();
    }
}
