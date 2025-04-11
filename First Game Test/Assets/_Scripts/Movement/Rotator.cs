using UnityEngine;

namespace TopDown.Movement
{
    public class Rotator : MonoBehaviour
    {
        protected void LookAt(Transform rotatedTransform, Vector3 target)
            {
                // Cambia la llamada a AngleBetweenTwoPoints para que use la dirección (b - a)
                float lookAngle = AngleBetweenTwoPoints(rotatedTransform.position, target) - 90;
                rotatedTransform.eulerAngles = new Vector3(0, 0, lookAngle);
            }

        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
            {
                return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
            }

    }
}

