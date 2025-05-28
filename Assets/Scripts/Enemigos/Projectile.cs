using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using DesignPatterns.ISP;  // para IDamageable

namespace DesignPatterns.ISP
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int m_DamageValue = 5;
        [SerializeField] private float m_Lifetime = 3f;

        private IObjectPool<Projectile> m_ObjectPool;
        private Rigidbody m_Rigidbody;
        private float m_MuzzleVelocity;

        /// <summary>
        /// Setter de la pool que gestiona este proyectil.
        /// </summary>
        public IObjectPool<Projectile> ObjectPool
        {
            set => m_ObjectPool = value;
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Ahora usamos Trigger para que también funcione con CharacterController.
        /// Asegúrate de marcar el Collider de este prefab como "Is Trigger".
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            // Intentamos infligir daño a cualquier IDamageable
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_DamageValue);
            }

            DeactivateProjectile();
        }

        /// <summary>
        /// Resetea física y devuelve el proyectil a la pool.
        /// </summary>
        private void DeactivateProjectile()
        {
            m_Rigidbody.linearVelocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_ObjectPool.Release(this);
        }

        /// <summary>
        /// Inicializa la pool y la velocidad de salida.
        /// </summary>
        public void Initialize(IObjectPool<Projectile> pool, float velocity)
        {
            ObjectPool = pool;
            m_MuzzleVelocity = velocity;
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Lanza el proyectil desde una posición y rotación dadas.
        /// </summary>
        public void Launch(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
            m_Rigidbody.AddForce(transform.forward * m_MuzzleVelocity, ForceMode.Acceleration);
            StartCoroutine(LifetimeCoroutine());
        }

        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(m_Lifetime);
            DeactivateProjectile();
        }
    }
}
