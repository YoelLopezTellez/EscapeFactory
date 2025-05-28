using UnityEngine;

public class RecogerTarjeta : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("Clip que se reproduce al recoger la tarjeta")]
    [SerializeField] private AudioClip recogerClip;
    [Tooltip("Volumen al reproducir el clip de recogida")]
    [Range(0f, 1f)]
    [SerializeField] private float recogerVolume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ▶️ Reproduce el sonido de recogida
            if (recogerClip != null)
                AudioSource.PlayClipAtPoint(recogerClip, transform.position, recogerVolume);

            // 1) Sumar tarjeta recogida
            var gestorTarjetas = FindObjectOfType<GestorTarjetas>();
            if (gestorTarjetas != null)
                gestorTarjetas.SumarTarjeta();

            // 2) Añadir puntos por cada tarjeta
            var gp = FindObjectOfType<GestorPuntuacion>();
            if (gp != null)
                gp.AñadirPuntosPorTarjeta();

            // 3) Destruir la tarjeta
            Destroy(gameObject);
        }
    }
}
