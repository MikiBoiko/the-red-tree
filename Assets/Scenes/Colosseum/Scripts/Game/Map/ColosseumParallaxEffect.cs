using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Colosseum.Game.Map
{
    public class ColosseumParallaxEffect : MonoBehaviour
    {
        [SerializeField] private List<Transform> _layers = new List<Transform>();
        [Range(1f, 10f)]
        [SerializeField] private float _factor = 2;
        [Range(0f, 1f)] [SerializeField] private float _lerpTime;

        private void Awake()
        {
            UpdateLayers();
        }

        [ContextMenu("Update layers")]
        private void UpdateLayers()
        {
            _layers = GetChildTransforms();
        }

        private List<Transform> GetChildTransforms()
        {
            List<Transform> result = new List<Transform>();

            foreach(Transform tgo in transform)
            {
                result.Add(tgo);
            }

            return result;
        }

        // TODO : reduce calls with IENumeratoirs
        private void FixedUpdate()
        {
            UpdateLayerPositions();
        }

        // TODO : IMPROVE PLS
        private void UpdateLayerPositions()
        {
            for (int i = 1; i <= _layers.Count; i++)
            {
                Transform layer = _layers[i - 1];

                layer.localPosition = new Vector2(
                    Mathf.Lerp(
                        layer.localPosition.x, 
                        CameraController.Position.x / (_factor * i), _lerpTime),
                    layer.localPosition.y
                ); 
            }
        }
    }
}