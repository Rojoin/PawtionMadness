using UnityEngine;

namespace Item
{
    public abstract class Pickable : MonoBehaviour
    {
        public void SetNewParent(Transform parent)
        {
            this.transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }
    }
}