using NPLTV.Dialogue;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NPLTV
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController Instance { get; private set; }

        [SerializeField] private DialogueManager _currentDialogueManager;
        private int _currentDialoguePointIndex = 0;
        private int _selectedOption = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("There can't be two DialogueManagers instances. Deleting this one.");
                Destroy(gameObject);
            }
        }

        public static void OpenDialogue(DialogueManager dialogue)
        {
            GameManager.SetStateToAllPlayers("dialogue");

            Instance._currentDialogueManager = dialogue;
            Instance.SetDialoguePoint(0);
        }

        public static void CloseDialogue()
        {
            DialogueUIManager.CloseDialogue();

            Instance._currentDialogueManager = null;

            GameManager.SetStateToAllPlayers("regular");
        }

        public void ChooseSelectedOption()
        {
            DialogueManager.DialoguePoint.DialogueOption option = _currentDialogueManager.GetDialoguePoint(_currentDialoguePointIndex).options[_selectedOption];

            if(option.nextPointIndex != null)
            {
                SetDialoguePoint((int)option.nextPointIndex);
            }
            else CloseDialogue();

            option.onChoose?.Invoke();
        }

        public void SelectUp()
        {
            if (_selectedOption <= 0)
            {
                _selectedOption = _currentDialogueManager.GetDialoguePoint(_currentDialoguePointIndex).options.Length - 1;
            }
            else _selectedOption--;

            DialogueUIManager.Instance.SelectOption(_selectedOption);
        }

        public void SelectDown()
        {
            if (_selectedOption >= _currentDialogueManager.GetDialoguePoint(_currentDialoguePointIndex).options.Length - 1)
            {
                _selectedOption = 0;
            }
            else _selectedOption++;

            DialogueUIManager.Instance.SelectOption(_selectedOption);
        }

        private void SetDialoguePoint(int index)
        {
            _currentDialoguePointIndex = index;
            _selectedOption = 0;

            DialogueUIManager.Instance.SetDialoguePoint(_currentDialogueManager.GetDialoguePoint(index));
        }
    }

    [System.Serializable]
    public class DialogueManager
    {
        [System.Serializable]
        public class DialoguePoint
        {
            public DialogueCharacter character;

            [TextArea] public string says;

            [System.Serializable]
            public class DialogueOption
            {
                public string text;
                public NullableInt nextPointIndex;
                public UnityEvent onChoose;

                public DialogueOption() => text = "...";

                public DialogueOption(string text, NullableInt nextPointIndex)
                {
                    this.text = text;
                    this.nextPointIndex = nextPointIndex;
                }
            }
            public DialogueOption[] options;

            public DialoguePoint(DialogueCharacter character, string says, List<DialogueOption> options)
            {
                this.character = character;
                this.says = says;
                this.options = options.ToArray();
            }
        }
        [SerializeField] private List<DialoguePoint> _dialoguePoints = new List<DialoguePoint>();
        /*{
            {
                new DialoguePoint(
                    _godCharacter,
                    "Hello there... I'm God! It's been a long time since we've talked, right? How are you doing? You feeling fine?",
                    new List<DialoguePoint.DialogueOption>()
                    {
                        { new DialoguePoint.DialogueOption("...", 1) },
                        { new DialoguePoint.DialogueOption("Hello God", 2) }
                    }
                )
            },
            {
                new DialoguePoint(
                    _godCharacter,
                    "So you are not much of a talker, aren't you?",
                    new List<DialoguePoint.DialogueOption>()
                    {
                        { new DialoguePoint.DialogueOption("...", null) },
                        { new DialoguePoint.DialogueOption("I am! I can talk!", 2) }
                    }
                )
            },
            { 
                new DialoguePoint(
                    _godCharacter,
                    "Sike! Live a dreadful life you stupid monkey.",
                    new List<DialoguePoint.DialogueOption>()
                    {
                        { new DialoguePoint.DialogueOption("...", null) }
                    }
                )
            }
        };*/

        public DialoguePoint GetDialoguePoint(int index)
        {
            return _dialoguePoints[index];
        }
    }
}
