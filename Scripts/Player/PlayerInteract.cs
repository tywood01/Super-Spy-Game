using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<PlayerLook>().playerCamera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // Create a ray at the center of the camera, shooting outwards
        Ray lookRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(lookRay.origin, lookRay.direction * interactDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(lookRay, out hitInfo, interactDistance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.WasPressedThisFrame())
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
