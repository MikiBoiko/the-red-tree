using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumLobbySelector<T> : MonoBehaviour where T : ColosseumLobbyIcon
    {
        [SerializeField] private GridLayoutGroup _layout;
        [SerializeField] private Sprite _randomSelectionIconSprite;
        [SerializeField] ColosseumDataSelection<T> _dataSelection;
        [SerializeField] private Transform[] _playerIndicators;

        public void InitializeData(GameObject selectionPrefab)
        {
            _playerIndicators = new Transform[_dataSelection.dataSheet.Length + 1];
            // Instantiate random selection
            _playerIndicators[0] = SetUpSelection(Instantiate(selectionPrefab, _layout.transform), 0);
            
            // For each element in data selection...
            for (int i = 1; i <= _dataSelection.dataSheet.Length; i++)
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
                instantiatedTransform.Find("Grid Image").GetComponent<Image>().sprite = _dataSelection.dataSheet[index - 1].icon;
            }

            return instantiatedTransform.Find("Player Indicators").transform;
        }

        public int SelectFrom(int from, Vector2 direction)
        {
            int result = from;
            if(direction.x > 0.5f)
            {
                result++;
            }
            else if (direction.x < -0.5f)
            {
                result--;
            }
            if(direction.y > 0.5f)
            {
                result += _layout.constraintCount;
            }
            else if (direction.y < -0.5f)
            {
                result -= _layout.constraintCount;
            }

            return _dataSelection.ContainsAt(result - 1) || result == 0 ? result :  from;
        }

        public void Select(Transform indicator)
        {

        }
    }
}
