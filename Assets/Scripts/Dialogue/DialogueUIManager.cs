using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Dialogue
{
    public class DialogueUIManager : MonoBehaviour
    {
        public static DialogueUIManager Instance { get; private set; }

        [Header("Dialogue GameObjects")]
        [SerializeField] private GameObject _talkingDialogue;

        [Header("Character info")]
        [SerializeField] private Text _characterNameText;
        [SerializeField] private Image _characterImage;

        [Header("Character says and player options to say")]
        [SerializeField] private Text _saysText;
        [SerializeField] private Text[] _optionTexts = new Text[3];

        private int _selectedOption;

        [Header("Option settings")]
        [SerializeField] private Color normalColor, selectedColor;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            {
                Debug.LogError("There can't be two player instances. Deleting this one.");
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _optionTexts[0].color = selectedColor;
        }

        public void SetDialoguePoint(DialogueManager.DialoguePoint dialoguePoint)
        {
            if (!_talkingDialogue.activeSelf) _talkingDialogue.SetActive(true);

            _characterNameText.text = dialoguePoint.character.characterName;
            _characterImage.sprite = dialoguePoint.character.icon;

            _saysText.text = dialoguePoint.says;

            SelectOption(0);

            for (int i = 0; i < _optionTexts.Length; i++)
            {
                if(i < dialoguePoint.options.Length)
                {
                    if (!_optionTexts[i].gameObject.activeSelf)
                        _optionTexts[i].gameObject.SetActive(true);

                    _optionTexts[i].text = dialoguePoint.options[i].text;
                }
                else if (_optionTexts[i].gameObject.activeSelf)
                    _optionTexts[i].gameObject.SetActive(false);
            }
        }

        public static void CloseDialogue() => Instance._talkingDialogue.SetActive(false);

        public void SelectOption(int index)
        {
            if(_selectedOption != index)
            {
                _optionTexts[_selectedOption].color = normalColor;
                _selectedOption = index;
                _optionTexts[_selectedOption].color = selectedColor;
            }
        }
    }
}