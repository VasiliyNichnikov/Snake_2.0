#nullable enable
using UnityEngine;

namespace Enemies
{
    public class SkinsStorage : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] _skins = null!;
        
        public void SetRandomSkin()
        {
            HideAllSkins();
            
            var skinId = Random.Range(0, _skins.Length - 1);
            var skin = _skins[skinId];
            skin.SetActive(true);
        }
        
        private void HideAllSkins()
        {
            foreach (var skin in _skins)
            {
                skin.SetActive(false);
            }
        }
    }
}