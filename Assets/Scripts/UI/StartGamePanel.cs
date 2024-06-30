#nullable enable
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGamePanel : MonoBehaviour
    {
        [SerializeField] private Text _cooldown = null!;
        [SerializeField] private GameObject _button = null!;
        
        private IEnumerator? _animator;

        private Action _onClicked = null!;
        
        public void Init(Action onClicked)
        {
            _onClicked = onClicked;
            _cooldown.gameObject.SetActive(false);
            _button.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Called from unity
        /// </summary>
        public void OnYesButtonClicked()
        {
            if (_animator != null)
            {
                return;
            }

            _animator = Cooldown();
            StartCoroutine(_animator);
        }

        private IEnumerator Cooldown()
        {
            _cooldown.gameObject.SetActive(true);
            _button.gameObject.SetActive(false);
            
            for (var i = 3; i > 0; i--)
            {
                _cooldown.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            _animator = null;
            _onClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}