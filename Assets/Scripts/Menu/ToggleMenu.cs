using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
    public class ToggleMenu : MonoBehaviour
    {
        [SerializeField] private InputActionReference toggleReference;

        private void Awake()
        {
            toggleReference.action.started += _ => Toggle();
        }

        private void Start()
        {
            Toggle();
        }

        private void Toggle()
        {
            var isActive = !gameObject.activeSelf;
            gameObject.SetActive(isActive);
        }
    }
}