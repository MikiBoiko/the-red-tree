using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Console
{
    public class ConsoleController : MonoBehaviour
    {
        public static ConsoleController Instance { get; private set; }
        public static bool IsOpen { private set; get; }

        [Header("UI")]
        [SerializeField] private GameObject _panel;
        [SerializeField] private Text _logText;
        [SerializeField] private InputField _consoleCommand;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("There can't be two ConsoleController instances. Deleting this one.");
                Destroy(gameObject);
            }
        }

        private void Start() 
        {
            Clear();
        }

        public static void OpenConsole()
        {
            GameManager.SetStateToAllPlayers("console");

            Instance._panel.SetActive(true);
            ResetInputField();
            
            IsOpen = true;
        }

        public static void CloseConsole()
        {
            GameManager.SetPreviousStateToAllPlayers();

            Instance._panel.SetActive(false);
            IsOpen = false;
        }

        public static void Print(string message)
        {
            Instance._logText.text += message;
        }

        public static void PrintLine(string message)
        {
            Instance._logText.text += $"\n{ message }";
        }

        public static void NextLine()
        {
            Instance._logText.text += "\n";
        }

        public static void Enter()
        {
            Command.RunConsoleCommand(Instance._consoleCommand.text);
            ResetInputField();
        }

        private static void ResetInputField()
        {
            InputField _currentConsoleCommand = Instance._consoleCommand;
            _currentConsoleCommand.text = "";
            _currentConsoleCommand.ActivateInputField();
            _currentConsoleCommand.Select();
        }

        public static void Clear()
        {
            Instance._logText.text = "";
        }
    }
}
