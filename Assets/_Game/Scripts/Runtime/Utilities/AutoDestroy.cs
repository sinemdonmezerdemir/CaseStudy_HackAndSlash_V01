using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// particle effect vslerin otomatik destroy edilmesi için kullanılabilir
    /// </summary>
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;

        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}