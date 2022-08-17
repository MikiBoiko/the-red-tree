using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Colosseum
{
    public class ColosseumLobbyUIManager : MonoBehaviour
    {
        private static ColosseumLobbyUIManager _instance;

        [SerializeField]
        private Transform _lobbyPlayersList;

        private void Awake()
        {
            if(_instance == null && _instance != this)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("Can't instantiate two ColosseumLobbyUIManagers at the same time.");
                Destroy(gameObject);
            }
        }

        public static void JoinPlayer(ColosseumLobbyPlayer player)
        {
            // Add to list
            player.transform.SetParent(_instance._lobbyPlayersList);
        }
    }
}
