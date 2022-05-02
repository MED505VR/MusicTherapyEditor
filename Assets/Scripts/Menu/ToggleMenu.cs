using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
    public class ToggleMenu : MonoBehaviour
    {

        public InputActionReference toggleReference = null;

        private void Awake()
        {
            toggleReference.action.started += Toggle;
        }

        private void OnDestroy()
        {
            toggleReference.action.started -= Toggle;
        }

        private void Toggle(InputAction.CallbackContext context)
        {
            bool isActive = !gameObject.activeSelf;
            gameObject.SetActive(isActive);
        }
    }
}
