using UnityEngine;
using UnityEngine.InputSystem;


namespace TopDown.Movement
{
    public enum PlayerMode
    {
        Normal,
        Immortal
    }

    public class PlayerModeManager : MonoBehaviour
    {
        public PlayerMode CurrentMode { get; private set; } = PlayerMode.Normal;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask visibleLayers;

        private void Update()
        {
            if (Keyboard.current.leftCtrlKey.wasPressedThisFrame || Keyboard.current.rightCtrlKey.wasPressedThisFrame)
            {
                ToggleMode();
            }
        }

        private void ToggleMode()
        {
            if (CurrentMode == PlayerMode.Normal)
            {
                CurrentMode = PlayerMode.Immortal;
                // Oculta visión
                mainCamera.cullingMask = visibleLayers; //0
            }
            else
            {
                CurrentMode = PlayerMode.Normal;
                // Restaura visión
                mainCamera.cullingMask = visibleLayers;
            }
        }

        public bool IsImmortal => CurrentMode == PlayerMode.Immortal;
    }
}
