using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Interactable
{
    public class InteractableBehaviour : MonoBehaviour
    {
        [SerializeField] private Color _unselectedColor, _selectedColor;
        [SerializeField] private SpriteRenderer _sr;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().interactables.AddInteractableBehaviour(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().interactables.RemoveInteractableBehaviour(this);
            }
        }

        public void Select()
        {
            _sr.color = _selectedColor;
            OnSelect();
        }

        public void Deselect()
        {
            _sr.color = _unselectedColor;
            OnDeselect();
        }

        public virtual void Interact()
        {
            Debug.Log($"Interacting with { gameObject.name }");
        }

        public virtual void OnSelect()
        {
            // Debug.Log($"Selecting { gameObject.name }");
        }

        public virtual void OnDeselect()
        {
            // Debug.Log($"Deselecting { gameObject.name }");
        }
    }

    [System.Serializable]
    public class Interactables
    {
        [SerializeField] private List<InteractableBehaviour> _availableInteractables;
        [SerializeField] private InteractableBehaviour _selectedInteractable;

        public Interactables()
        {
            _availableInteractables = new List<InteractableBehaviour>();
        }

        public void AddInteractableBehaviour(InteractableBehaviour interactable)
        {
            _availableInteractables.Add(interactable);
            if(_availableInteractables.Count == 1) Select(interactable);
        }

        public void RemoveInteractableBehaviour(InteractableBehaviour interactable)
        {
            if (interactable != null && interactable == _selectedInteractable)
                Deselect(interactable);

            _availableInteractables.Remove(interactable);
            if (_availableInteractables.Count != 0) Select(_availableInteractables[0]);
        }

        private void Select(InteractableBehaviour interactable)
        {
            interactable.Select();
            _selectedInteractable = interactable;
        }

        private void Deselect(InteractableBehaviour interactable)
        {
            interactable.Deselect();
            _selectedInteractable = null;
        }

        public void SelectNext()
        {
            if(_availableInteractables.Count > 1)
            {
                _selectedInteractable.Deselect();
                Select(_availableInteractables[(_availableInteractables.IndexOf(_selectedInteractable) + 1) % _availableInteractables.Count]);
            }
        }

        public void SelectBefore()
        {
            if (_availableInteractables.Count > 1)
            {
                _selectedInteractable.Deselect();

                int _selectedIndex = _availableInteractables.IndexOf(_selectedInteractable) - 1;
                if (_selectedIndex < 0) _selectedIndex = _availableInteractables.Count - 1;

                Select(_availableInteractables[_selectedIndex]);
            }
        }

        public void InteractWithSelected()
        {
            _selectedInteractable?.Interact();
        }
    }
}