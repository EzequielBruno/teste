using UnityEngine;
using UnityEngine.Events;

namespace MiningPPE
{
    /// <summary>
    /// Controla acesso à área de trabalho. Bloqueia passagem quando o checklist de EPI não está completo.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SafetyZone : MonoBehaviour
    {
        [Tooltip("Referência ao PPETrainingManager que controla o checklist.")]
        [SerializeField] private PPETrainingManager manager;

        [Tooltip("GameObject opcional para visual de barreira (ex.: campo de força).")]
        [SerializeField] private GameObject barrierVisual;

        [Header("Eventos")]
        public UnityEvent onBlocked;
        public UnityEvent onGranted;

        private void Awake()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void Update()
        {
            if (manager == null)
            {
                return;
            }

            if (barrierVisual != null)
            {
                barrierVisual.SetActive(!manager.AllItemsEquipped);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (manager == null)
            {
                return;
            }

            if (manager.AllItemsEquipped)
            {
                onGranted?.Invoke();
            }
            else
            {
                onBlocked?.Invoke();
            }
        }
    }
}
