using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private Camera cam;

    private RaycastHit2D hitInfo;

    private int rayDistance = 15;
    private Vector3 MousePosition;

    private bool isContact = false;

    private DialogueManager theDM;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        CheckObject();
    }

    private void CheckObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = cam.ScreenToWorldPoint(MousePosition);
            if (hitInfo = Physics2D.Raycast(MousePosition, transform.forward, rayDistance))
            {
                Contact();
            }
            else
            {
                NotContact();
            }
        }
    }

    private void Contact()
    {
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            if(!isContact)
            {
                isContact = true;
                theDM.ShowDialogue(hitInfo.transform.GetComponent<InteractionEvent>().GetDialogue());
            }
            
        }
        else
        {
            NotContact();
        }
    }

    private void NotContact()
    {
        if(isContact)
        {
            isContact = false;
            theDM.HideDialogue();
        }
        
    }
}
