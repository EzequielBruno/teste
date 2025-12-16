using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace MiningPPE
{
    /// <summary>
    /// Gerencia o checklist de EPI e envia eventos quando todos os itens são equipados ou removidos.
    /// </summary>
    public class PPETrainingManager : MonoBehaviour
    {
        [Header("Referências")]
        [Tooltip("Lista de itens de EPI na cena. Se vazio, será preenchido automaticamente.")]
        [SerializeField] private List<PPEItem> ppeItems = new();

        [Tooltip("Texto de checklist (TextMeshProUGUI).")]
        [SerializeField] private TextMeshProUGUI checklistText;

        [Tooltip("Texto de instrução/feedback (TextMeshProUGUI).")]
        [SerializeField] private TextMeshProUGUI feedbackText;

        [Header("Eventos")]
        public UnityEvent onAllEquipped;
        public UnityEvent onMissingEquipment;

        public bool AllItemsEquipped => ppeItems.Count > 0 && ppeItems.All(i => i.IsEquipped);

        private void Start()
        {
            if (ppeItems.Count == 0)
            {
                ppeItems = FindObjectsOfType<PPEItem>().ToList();
            }

            foreach (var item in ppeItems)
            {
                item.onEquipped.AddListener(OnItemEquipped);
                item.onUnequipped.AddListener(OnItemUnequipped);
            }

            UpdateUI();
        }

        private void OnDestroy()
        {
            foreach (var item in ppeItems)
            {
                item.onEquipped.RemoveListener(OnItemEquipped);
                item.onUnequipped.RemoveListener(OnItemUnequipped);
            }
        }

        private void OnItemEquipped(PPEItem item)
        {
            UpdateUI();
            if (AllItemsEquipped)
            {
                feedbackText?.SetText("Todos os EPI equipados. Você pode entrar na área de trabalho.");
                onAllEquipped?.Invoke();
            }
        }

        private void OnItemUnequipped(PPEItem item)
        {
            UpdateUI();
            if (!AllItemsEquipped)
            {
                feedbackText?.SetText("Complete o checklist de EPI antes de avançar.");
                onMissingEquipment?.Invoke();
            }
        }

        private void UpdateUI()
        {
            if (checklistText == null)
            {
                return;
            }

            var lines = ppeItems.Select(i => $"- {(i.IsEquipped ? "[OK]" : "[ ]")} {i.DisplayName}");
            checklistText.SetText(string.Join('\n', lines));
        }
    }
}
