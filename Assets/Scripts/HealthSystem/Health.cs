using UnityEngine;
using UnityEngine.Events;
using DesignPatterns.ISP;  // para IDamageable

namespace DesignPatterns.LSP
{
    /// <summary>
    /// Basic behavior for tracking the health of an object.
    /// </summary>
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] float m_MaxHealth = 100f;
        [SerializeField] float m_CurrentHealth;
        [SerializeField] bool m_ResetOnStart;

        [Tooltip("Notifies listeners that this object has died")]
        public UnityEvent Died;

        [Tooltip("Notifies listeners of updated health percentage")]
        public UnityEvent<float> HealthChanged;

        protected bool m_IsInvulnerable;
        protected bool m_IsDead;

        // Propiedades públicas
        public float MaxHealth { get => m_MaxHealth; set => m_MaxHealth = value; }
        public float CurrentHealth => m_CurrentHealth;
        public bool IsInvulnerable { get => m_IsInvulnerable; set => m_IsInvulnerable = value; }

        private void Awake()
        {
            if (m_ResetOnStart)
                m_CurrentHealth = MaxHealth;
        }

        private void Start()
        {
            // Inicializa la UI
            HealthChanged.Invoke(CurrentHealth / MaxHealth);
        }

        /// <summary>
        /// Aplica daño; este método satisface IDamageable.TakeDamage(...)
        /// </summary>
        public virtual void TakeDamage(float amount)
        {
            if (m_IsDead || m_IsInvulnerable)
                return;

            m_CurrentHealth -= amount;
            if (m_CurrentHealth <= 0f)
            {
                m_CurrentHealth = 0f;
                Die();
            }

            HealthChanged.Invoke(CurrentHealth / MaxHealth);
        }

        /// <summary>
        /// Cura hasta el máximo y notifica cambios.
        /// </summary>
        public void Heal(float amount)
        {
            if (m_IsDead)
                return;

            m_CurrentHealth += amount;
            if (m_CurrentHealth > MaxHealth)
                m_CurrentHealth = MaxHealth;

            HealthChanged.Invoke(CurrentHealth / MaxHealth);
        }

        /// <summary>
        /// Se llama internamente cuando la vida llega a cero.
        /// </summary>
        protected virtual void Die()
        {
            if (m_IsDead)
                return;

            m_IsDead = true;
            Died.Invoke();
            gameObject.SetActive(false);
        }
    }
}
