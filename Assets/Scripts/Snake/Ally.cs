#nullable enable
using UnityEngine;

namespace Snake
{
    public class Ally : MonoBehaviour
    {
        public void PickUp()
        {
            Destroy(gameObject);
        }
    }
}