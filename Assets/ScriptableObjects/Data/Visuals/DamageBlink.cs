using UnityEngine;

namespace ScriptableObjects.Data.Visuals
{
    [CreateAssetMenu(menuName = "Create DamageBlink", fileName = "DamageBlink", order = 0)]
    public class DamageBlink : ScriptableObject
    {
        [Range(0.1f, 1.0f)] public float blinkTime;
        public float blinkSpeed;
        public Color color = Color.red;
    }
    
    
}