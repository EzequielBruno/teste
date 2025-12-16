using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace MiningPPE
{
    /// <summary>
    /// Representa um item de EPI que pode ser pego e equipado em um socket do XR Interaction Toolkit.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(XRGrabInteractable))]
    public class PPEItem : MonoBehaviour
    {
        [Header("Metadados")]
        [Tooltip("Nome exibido no checklist.")]
        [SerializeField] private string displayName = "Capacete";

        [Header("Estado")]
        [SerializeField] private bool isEquipped;

        [Header("Eventos")]
        public UnityEvent<PPEItem> onEquipped;
        public UnityEvent<PPEItem> onUnequipped;

        private XRGrabInteractable _grab;

        public string DisplayName => displayName;
        public bool IsEquipped => isEquipped;

        private void Awake()
        {
            _grab = GetComponent<XRGrabInteractable>();
            _grab.selectEntered.AddListener(OnSelected);
            _grab.selectExited.AddListener(OnDeselected);
        }

        private void OnDestroy()
        {
            _grab.selectEntered.RemoveListener(OnSelected);
            _grab.selectExited.RemoveListener(OnDeselected);
        }

        private void OnSelected(SelectEnterEventArgs args)
        {
            // Se o item foi encaixado em um socket válido, considere como equipado.
            if (args.interactorObject is XRSocketInteractor)
            {
                SetEquipped(true);
            }
        }

        private void OnDeselected(SelectExitEventArgs args)
        {
            // Se saiu de um socket, considere como removido.
            if (args.interactorObject is XRSocketInteractor)
            {
                SetEquipped(false);
            }
        }

        public void ForceEquipState(bool equipped)
        {
            SetEquipped(equipped);
        }

        private void SetEquipped(bool equipped)
        {
            if (isEquipped == equipped)
            {
                return;
            }

            isEquipped = equipped;
            if (isEquipped)
            {
                onEquipped?.Invoke(this);
            }
            else
            {
                onUnequipped?.Invoke(this);
            }
        }
    }
}
