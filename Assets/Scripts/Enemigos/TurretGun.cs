using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using DesignPatterns.Utilities;

namespace DesignPatterns.ISP
{
    public class TurretGun : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [Tooltip("Prefab to shoot")]
        [SerializeField] private Projectile m_ProjectilePrefab;
        [Tooltip("Projectile force")]
        [SerializeField] private float m_MuzzleVelocity = 700f;
        [Tooltip("End point of gun where shots appear")]
        [SerializeField] private Transform m_MuzzlePosition;

        [Header("Firing Rate")]
        [Tooltip("Time between shots / smaller = higher rate of fire")]
        [SerializeField] private float m_CooldownWindow = 0.1f;

        [Header("Object Pool Settings")]
        [Tooltip("Throw errors if we try to release an item that is already in the pool")]
        [SerializeField] private bool m_CollectionCheck = true;
        [Tooltip("Default pool size")]
        [SerializeField] private int m_DefaultCapacity = 20;
        [Tooltip("Pool can expand to this limit")]
        [SerializeField] private int m_MaxSize = 100;

        [Header("Audio")]
        [Tooltip("AudioSource for playing shot sounds")]
        [SerializeField] private AudioSource audioSource;
        [Tooltip("AudioClip to play when shooting")]
        [SerializeField] private AudioClip shootClip;

        [SerializeField] private UnityEvent m_GunFired;

        private IObjectPool<Projectile> objectPool;
        private float nextTimeToShoot;

        private void Awake()
        {
            // Initialize AudioSource if not assigned
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();

            // Set up object pool for projectiles
            objectPool = new ObjectPool<Projectile>(
                CreateProjectile,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                m_CollectionCheck,
                m_DefaultCapacity,
                m_MaxSize
            );
        }

        private Projectile CreateProjectile()
        {
            var instance = Instantiate(m_ProjectilePrefab);
            instance.Initialize(objectPool, m_MuzzleVelocity);
            return instance;
        }

        private void OnGetFromPool(Projectile p)    => p.gameObject.SetActive(true);
        private void OnReleaseToPool(Projectile p)  => p.gameObject.SetActive(false);
        private void OnDestroyPooledObject(Projectile p) => Destroy(p.gameObject);

        /// <summary>
        /// Call externally (e.g., from turret controller) to force a shot.
        /// </summary>
        public void ForzarDisparo()
        {
            if (Time.time < nextTimeToShoot)
                return;

            // Play the shooting sound
            if (audioSource != null && shootClip != null)
            {
                audioSource.PlayOneShot(shootClip);
            }

            // Get a projectile from pool, launch it, and set next shot time
            var projectile = objectPool.Get();
            projectile.Launch(m_MuzzlePosition.position, m_MuzzlePosition.rotation);
            nextTimeToShoot = Time.time + m_CooldownWindow;
            m_GunFired.Invoke();
        }
    }
}