using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DesignPatterns.LSP;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Health m_Health;
    [SerializeField] private Slider m_HealthSlider;

    [Header("Suavizado y color")]
    [SerializeField] private float m_LerpDuration = 0.5f;
    [SerializeField] private Gradient m_Gradient; // define: 0 = rojo, 0.5 = amarillo, 1 = verde

    private Image m_FillImage;
    private Coroutine m_LerpCoroutine;

    private void Awake()
    {
        if (m_HealthSlider == null)
            m_HealthSlider = GetComponent<Slider>();
        m_FillImage = m_HealthSlider.fillRect.GetComponent<Image>();
    }

    private void OnEnable()
    {
        m_Health.HealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        m_Health.HealthChanged.RemoveListener(OnHealthChanged);
    }

    private void Start()
    {
        // Inicializa barra y color al valor actual
        float init = m_Health.CurrentHealth / m_Health.MaxHealth;
        m_HealthSlider.value = init;
        UpdateColor(init);
    }

    private void OnHealthChanged(float target)
    {
        if (m_LerpCoroutine != null)
            StopCoroutine(m_LerpCoroutine);
        m_LerpCoroutine = StartCoroutine(LerpTo(target));
    }

    private IEnumerator LerpTo(float target)
    {
        float start = m_HealthSlider.value;
        float elapsed = 0f;

        while (elapsed < m_LerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / m_LerpDuration;
            float v = Mathf.Lerp(start, target, t);
            m_HealthSlider.value = v;
            UpdateColor(v);
            yield return null;
        }

        m_HealthSlider.value = target;
        UpdateColor(target);
    }

    private void UpdateColor(float value)
    {
        if (m_Gradient != null && m_FillImage != null)
            m_FillImage.color = m_Gradient.Evaluate(value);
    }
}
