using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace TopDown.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    

    public class PlayerMovement : Mover
    {
        [SerializeField] private AudioClip[] footstepClips;
        [SerializeField, Range(0f, 1f)] private float footstepVolume = 0.1f;
        [SerializeField] private float stepInterval = 0.4f;

        private AudioSource audioSource;
        private bool isWalking = false;
        private Coroutine footstepRoutine;
        private PlayerModeManager modeManager;

        private void Start()
        {
            modeManager = GetComponent<PlayerModeManager>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnMove(InputValue value)
        {
            if (MenuPausa.GameIsPaused) return;
            Vector3 playerInput = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0);
            currentInput = playerInput;

            if (playerInput.magnitude > 0 && !isWalking)
            {
                isWalking = true;
                footstepRoutine = StartCoroutine(PlayFootsteps());
            }
            else if (playerInput.magnitude == 0 && isWalking)
            {
                isWalking = false;
                if (footstepRoutine != null)
                    StopCoroutine(footstepRoutine);
            }
        }

        private IEnumerator PlayFootsteps()
        {
            while (true)
            {
                if (footstepClips.Length > 0)
                {
                    int index = Random.Range(0, footstepClips.Length);
                    audioSource.PlayOneShot(footstepClips[index], footstepVolume);
                }
                yield return new WaitForSeconds(stepInterval);
            }
        }
    }
}
