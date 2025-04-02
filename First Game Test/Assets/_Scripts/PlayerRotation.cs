using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotator
    {
        [Header("Torso y Piernas")]
        [SerializeField] private Transform torso;
        [SerializeField] private Transform legs;

        [Header("Mover Reference")]
        [SerializeField] private Mover playerMover;
        private void OnLook(InputValue value)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            LookAt(torso, mousePosition);
        }

        private void Update()
        {  
            Vector3 legsLookPoint = transform.position + new Vector3(playerMover.CurrentInput.x, playerMover.CurrentInput.y);
            LookAt(legs, Vector3.zero);
        }

    }
}

