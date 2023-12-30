using UnityEngine;

namespace Utilities
{
    public class DoNotDestroyThat : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}