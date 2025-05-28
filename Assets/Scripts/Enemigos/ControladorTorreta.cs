using UnityEngine;
using DesignPatterns.ISP; // Para TurretGun

public class ControladorTorreta : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TurretGun turreta;            // ← Ya estaba
    [SerializeField] private Transform parteGiratoria;     // ← Ya estaba
    [SerializeField] private Transform origenRayo;         // ← NUEVO: arrastra aquí tu PuntoDisparo  

    [Header("Detección")]
    [SerializeField] private string etiquetaJugador = "Player";
    [SerializeField] private float radioDeteccion = 20f;

    [Header("Rotación")]
    [SerializeField] private float velocidadRotacion = 90f;
    [SerializeField] private float umbralAngulo = 5f;

    [Header("Visión y disparo")]
    [SerializeField] private float alcanceMaximo = 20f;
    [SerializeField] private LayerMask capaIgnorar;        // ← NUEVO (opcional): p.ej. Turret layer

    private Transform objetivo;

    private void Update()
    {
        // 1) Detectar jugador en esfera
        objetivo = null;
        Collider[] hits = Physics.OverlapSphere(transform.position, radioDeteccion);
        foreach (var h in hits)
            if (h.CompareTag(etiquetaJugador))
            {
                objetivo = h.transform;
                break;
            }
        if (objetivo == null) return;

        // 2) Rotar hacia jugador (igual que antes)
        Vector3 dirPlano = (objetivo.position - parteGiratoria.position).normalized;
        Quaternion deseada = Quaternion.LookRotation(new Vector3(dirPlano.x, 0, dirPlano.z));
        parteGiratoria.rotation = Quaternion.RotateTowards(
            parteGiratoria.rotation,
            deseada,
            velocidadRotacion * Time.deltaTime
        );
        if (Quaternion.Angle(parteGiratoria.rotation, deseada) > umbralAngulo) return;

        // 3) Comprobar distancia
        float dist = Vector3.Distance(origenRayo.position, objetivo.position);
        if (dist > alcanceMaximo) return;

        // 4) Raycast desde el cañón
        Vector3 dirDisparo = origenRayo.forward;
        Debug.DrawRay(origenRayo.position, dirDisparo * alcanceMaximo, Color.red, 0.1f);

        // Excluimos la capa de la propia torreta (~capaIgnorar)
        if (Physics.Raycast(
                origenRayo.position,
                dirDisparo,
                out RaycastHit hitInfo,
                alcanceMaximo,
                ~capaIgnorar))
        {
            // Comparamos tag en el root del objeto impactado (por si choca con hijos)
            Transform hitRoot = hitInfo.collider.transform.root;
            if (hitRoot.CompareTag(etiquetaJugador))
            {
                turreta.ForzarDisparo();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
