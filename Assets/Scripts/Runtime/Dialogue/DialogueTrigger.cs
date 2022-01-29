using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parasync.Runtime.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Ink JSON")]
        //[SerializeField] public TextAsset inkJSON;
        [SerializeField] public TextAsset inkJSON;
        private bool playerInRange;

        private void Awake()
        {
            playerInRange = false;
        }

        private void Update()
        {
            if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                if (Input.GetKeyDown("e"))
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
            }    
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                playerInRange = true;
            }
        }
    
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                playerInRange = false;
            }
        }
    }
}
