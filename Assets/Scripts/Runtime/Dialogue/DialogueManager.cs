using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;
using Ink.Runtime;

namespace Parasync.Runtime.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        [SerializeField] private float typingSpeed = 1.51f;
        [SerializeField] public GameObject dialoguePanel;
        [SerializeField] public TextMeshProUGUI dialogueText;
        [SerializeField] private Animator portraitAnimator;
        [Header("Choices UI")]
        [SerializeField] public GameObject[] choices;

        public AudioClip[] voices;
        [SerializeField] public AudioSource audioSource;
        public TextMeshProUGUI[] choicesText;

        public Story currentStory;
        public bool dialogueIsPlaying { get; private set; }

        private bool canContinueToNextLine = false;

        private static DialogueManager instance;

        private const string PORTRAIT_TAG = "portrait";

        public static DialogueVariables variables;

        private Coroutine displayLineCoroutine;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            }
            instance = this;
        }

        public static DialogueManager GetInstance()
        {
            return instance;
        }

        private void Start()
        {
            variables = new DialogueVariables();
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);

            // get all of the choices text 
            choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;
            foreach (GameObject choice in choices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }
        }

        private void Update()
        {
            // return right away if dialogue isn't playing
            if (!dialogueIsPlaying)
            {
                return;
            }

            // handle continuing to the next line in the dialogue when submit is pressed
            if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && Input.GetKeyDown("space"))
            {
                ContinueStory();
            }
        }

        public void EnterDialogueMode(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);
            variables.LoadVariablesToStory(ref currentStory);

            // reset portrait, layout, and speaker
            portraitAnimator.Play("default");

            ContinueStory();
        }

        private IEnumerator ExitDialogueMode()
        {
            yield return new WaitForSeconds(0.2f);

            variables.SaveVariablesFromStory(currentStory);
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }

        private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                if (displayLineCoroutine != null)
                {
                    StopCoroutine(displayLineCoroutine);
                }
                // set text for the current dialogue line
                displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

                // handle tags
                HandleTags(currentStory.currentTags);
            }
            else
            {
                StartCoroutine(ExitDialogueMode());
            }
        }

        private void HandleTags(List<string> currentTags)
        {
            // Loop through each tag and handle it accordingly
            foreach (string tag in currentTags)
            {
                //parse the tag
                string[] splitTag = tag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError("Tag could not be appropriately parsed: " + tag);
                }
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                //handle the tag
                switch (tagKey)
                {
                    case PORTRAIT_TAG:
                        portraitAnimator.Play(tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled:" + tag);
                        break;
                }
            }
        }

        private void DisplayChoices()
        {
            List<Choice> currentChoices = currentStory.currentChoices;

            // defensive check to make sure our UI can support the number of choices coming in
            if (currentChoices.Count > choices.Length)
            {
                Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                    + currentChoices.Count);
            }

            int index = 0;
            // enable and initialize the choices up to the amount of choices for this line of dialogue
            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }
            // go through the remaining choices the UI supports and make sure they're hidden
            for (int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }

            StartCoroutine(SelectFirstChoice());
        }

        private IEnumerator SelectFirstChoice()
        {
            // Event System requires we clear it first, then wait
            // for at least one frame before we set the current selected object.
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }

        public void MakeChoice(int choiceIndex)
        {
            if (canContinueToNextLine)
            {
                currentStory.ChooseChoiceIndex(choiceIndex);
            }
        }
   
        private void HideChoices()
        {
            foreach (GameObject choiceButton in choices)
            {
                choiceButton.SetActive(false);
            }
        }

        private IEnumerator DisplayLine(string line)
        {
            // empty the dialogue text
            dialogueText.text = "";
            HideChoices();

            canContinueToNextLine = false;
       
            //type each character 1 by 1
            foreach (char letter in line.ToCharArray())
            {
                yield return new WaitForSeconds(typingSpeed);
                if (!canContinueToNextLine && Input.GetKeyDown("space"))
                {
                    dialogueText.text = line;
                    break;
                }
                dialogueText.text += letter;
                FindObjectOfType<AudioManager>().Play("DialogueSound");
            }

            // display choices, if any, for this dialogue line
            DisplayChoices();

            canContinueToNextLine = true;

        }

    }
}
