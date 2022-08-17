using UnityEngine;

namespace NPLTV.Damage
{
    public interface IKnockable
    {
        void Knock(Vector2 direction, float force, float time);
    }
}