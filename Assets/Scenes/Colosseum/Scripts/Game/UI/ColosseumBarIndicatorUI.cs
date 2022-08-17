using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Game.UI
{
    public class ColosseumBarIndicatorUI : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)]
        private float _percentage, _shadowSpeed;

        [SerializeField]
        private Gradient _color;

        [SerializeField]
        private Image _barImage;
        [SerializeField]
        private Transform _barTransform, _shadowTransform;

        private void FixedUpdate()
        {
            
        }

        private void UpdateIndicator()
        {
            _shadowTransform.localScale = new Vector2(
                Mathf.Lerp(_shadowTransform.localScale.x, _percentage, _shadowSpeed),
                1
            );
        }

        public void SetPlayerColor(Color color)
        {

        }

        public void SetPercentage(float percentage)
        {
            _barTransform.localScale = new Vector2(
                percentage,
                1
            );
            _percentage = percentage;
            _barImage.color = _color.Evaluate(percentage);
        }

        private void OnDrawGizmos()
        {
            UpdateIndicator();   
            SetPercentage(_percentage);
        }
    }
}
