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
            // Piernas se mueven en dirección del teclado.
            // Vector3 legsLookPoint = transform.position + new Vector3(playerMover.CurrentInput.x, playerMover.CurrentInput.y);
            
            // Piernas se mueven en la dirección a la que apunta el mouse.
            Vector3 mousePositionTest = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            LookAt(legs, mousePositionTest);
        }

    }
}

