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
        [SerializeField] private GameObject blackOverlay;

        private void Update()
        {
            if (MenuPausa.GameIsPaused) return;
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

                // Mostrar pantalla negra
                if (blackOverlay != null)
                    blackOverlay.SetActive(true);
            }
            else
            {
                CurrentMode = PlayerMode.Normal;

                // Ocultar pantalla negra
                if (blackOverlay != null)
                    blackOverlay.SetActive(false);
            }

            mainCamera.cullingMask = visibleLayers;
        }


        public bool IsImmortal => CurrentMode == PlayerMode.Immortal;
    }
}
