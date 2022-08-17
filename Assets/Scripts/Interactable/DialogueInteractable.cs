using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Interactable
{
    public class DialogueInteractable : InteractableBehaviour
    {
        [SerializeField] protected DialogueManager _dialogue;

        public override void Interact()
        {
            DialogueController.OpenDialogue(_dialogue);
        }
    }
}
