using System;
using UnityEngine;

namespace BerserkPixel.Prata
{
    public class DialogManager : MonoBehaviour
    {
        private static DialogManager _instance;
        public static DialogManager Instance => _instance;

        [SerializeField] private DialogRenderer dialogRenderer;

        /// <summary>
        /// Subscribe to this actions to listen and act according to this different events
        ///
        /// For example on another script you can do:
        ///
        /// private void Start()
        /// {
        ///     DialogManager.Instance.OnDialogStart += HandleDialogStart;
        ///     DialogManager.Instance.OnDialogEnds += HandleDialogEnd;
        ///     DialogManager.Instance.OnDialogCancelled += HandleDialogEnd;
        /// }
        ///
        /// private void OnDisable()
        /// {
        ///     DialogManager.Instance.OnDialogStart -= HandleDialogStart;
        ///     DialogManager.Instance.OnDialogEnds -= HandleDialogEnd;
        ///     DialogManager.Instance.OnDialogCancelled -= HandleDialogEnd;
        /// }
        ///
        /// private void HandleDialogStart()
        /// {
        ///     // Not allow player to move 
        /// }
        ///
        /// private void HandleDialogEnd()
        /// {
        ///     // Enable player movement
        /// }
        ///  
        /// </summary>
        public Action OnDialogStart = delegate { };

        public Action OnDialogEnds = delegate { };
        public Action OnDialogCancelled = delegate { };

        private bool isInConversation;
        private Interaction lastInteraction;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void BeginConversation(Interaction interaction)
        {
            if (dialogRenderer != null)
                dialogRenderer.Show();

            if (!isInConversation)
            {
                isInConversation = true;
                OnDialogStart?.Invoke();
            }

            lastInteraction = interaction;

            ShowDialog();
        }

        public void HideDialog()
        {
            if (dialogRenderer != null)
                dialogRenderer.Hide();

            if (lastInteraction != null)
            {
                if (lastInteraction.HasAnyDialogLeft())
                {
                    OnDialogCancelled?.Invoke();
                }
                else
                {
                    OnDialogEnds?.Invoke();
                }

                lastInteraction = null;
            }

            isInConversation = false;
        }

        private void ShowDialog()
        {
            Dialog dialog = lastInteraction.GetCurrentDialog();

            if (dialog == null)
            {
                HideDialog();
                return;
            }

            if (dialogRenderer != null)
                dialogRenderer.Render(dialog);
        }

        public void MakeChoice(string dialogGuid, string choice)
        {
            if (lastInteraction == null) return;

            Dialog dialog = lastInteraction.GetCurrentDialogFromChoice(dialogGuid, choice);

            if (dialog == null)
            {
                HideDialog();
                return;
            }

            if (dialogRenderer != null)
                dialogRenderer.Render(dialog);
        }
    }
}