using UnityEngine;
using UnityEngine.InputSystem;

namespace NPLTV.Colosseum
{
    public class ColosseumLobbyPlayer : PlayerInput
    {
        private void Start()
        {
            ColosseumLobbyManager.JoinPlayer(this);

            actions.FindActionMap("Menu").Enable();
            SwitchCurrentActionMap("Menu");

            currentActionMap["Ready"].performed += ctx =>
            {
                Debug.Log("Getting ready");
            };
        }
    }
}