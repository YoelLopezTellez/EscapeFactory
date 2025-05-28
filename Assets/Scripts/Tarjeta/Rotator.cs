using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Tooltip("Velocidad de rotación en grados por segundo")]
    [SerializeField] private float rotationSpeed = 60f;

    private void Update()
    {
        // Gira alrededor del eje Y del mundo → movimiento horizontal
        transform.Rotate(
            Vector3.up * rotationSpeed * Time.deltaTime, 
            Space.World
        );
    }
}
