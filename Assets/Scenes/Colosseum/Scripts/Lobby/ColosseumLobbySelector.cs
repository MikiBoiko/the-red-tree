using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumLobbySelector<T> : MonoBehaviour where T : ColosseumLobbyIcon
    {
        [SerializeField] private GridLayoutGroup _layout;
        [SerializeField] private Sprite _randomSelectionIconSprite;
        public ColosseumDataSelection<T> Data;
        [SerializeField] private Transform[] _playerIndicators;

        public void InitializeData(GameObject selectionPrefab)
        {
            _playerIndicators = new Transform[Data.dataSheet.Length + 1];
            // Instantiate random selection
            _playerIndicators[0] = SetUpSelection(Instantiate(selectionPrefab, _layout.transform), 0);
            
            // For each element in data selection...
            for (int i = 1; i <= Data.dataSheet.Length; i++)
            {
                _playerIndicators[i] = SetUpSelection(Instantiate(selectionPrefab, _layout.transform), i); ;
            }
        }

        private Transform SetUpSelection(GameObject instantiatedSelection, int index)
        {
            Transform instantiatedTransform = instantiatedSelection.transform;
            // if its random
            if(index == 0)
            {
                instantiatedTransform.Find("Grid Image").GetComponent<Image>().sprite = _randomSelectionIconSprite;
            }
            else
            {
                instantiatedTransform.Find("Grid Image").GetComponent<Image>().sprite = Data.dataSheet[index - 1].icon;
            }

            return instantiatedTransform.Find("Player Indicators").transform;
        }

        public int SelectFrom(int from, ColosseumLobbyDirection direction, Transform indicator)
        {
            int result = from;
            switch (direction)
            {
                case ColosseumLobbyDirection.Right:
                    result++;
                    break;
                case ColosseumLobbyDirection.Left:
                    result--;
                    break;
                case ColosseumLobbyDirection.Up:
                    result += _layout.constraintCount;
                    break;
                case ColosseumLobbyDirection.Down:
                    result -= _layout.constraintCount;
                    break;
                default:
                    break;
            }
            if(result < 0) result += _playerIndicators.Length;
            else result %= _playerIndicators.Length;
            Select(result, indicator);
            return result;
        }

        public void Select(int to, Transform indicator)
        {
            indicator.gameObject.SetActive(true);
            indicator.SetParent(_playerIndicators[to]);
        }

        public T GetValue(int index) {
            return Data.dataSheet[index];
        }

        public T GetRandomValue() {
            return Data.GetRandom();
        }
    }
}
