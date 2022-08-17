using System.Collections.Generic;
using UnityEngine;

namespace NPLTV
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField] protected List<PlayerManager> players = new List<PlayerManager>();
        public static List<PlayerManager> Players { get => Instance.players; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("There can't be two GameManager instances. Deleting this one.");
                Destroy(gameObject);
            }
        }

        public static void SetStateToAllPlayers(string stateName)
        {
            foreach (PlayerManager player in Players)
            {
                player.StateInput.SetState(stateName);
            }
        }

        public static void SetPreviousStateToAllPlayers()
        {
            foreach (PlayerManager player in Players)
            {
                player.StateInput.SetPreviousState();
            }
        }

        public static void AddPlayer(PlayerManager player)
        {
            Instance.players.Add(player);

            player.transform.SetParent(Instance.transform);
        }

        public static void RemovePlayer(PlayerManager player)
        {
            Instance.players.Remove(player);
        }
    }
}
