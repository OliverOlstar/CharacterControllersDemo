using BerserkPixel.Prata;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.BerserkPixel.Prata.Samples.Scripts
{
    public class TestDialogRenderer: DialogRenderer
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Text authorText;
        [SerializeField] private Text dialogText;
        [SerializeField] private Image dialogLeftImage;
        [SerializeField] private Image dialogRightImage;
        [SerializeField] private Transform choicesContainer;
        [SerializeField] private GameObject choiceButtonPrefab;
        
        public override void Show()
        {
            container.SetActive(true);
        }
        
        public override void Render(Dialog dialog)
        {
            dialogText.text = dialog.text;
            authorText.text = dialog.character.characterName;

            if (dialog.isFacingRight)
            {
                dialogLeftImage.enabled = true;
                dialogLeftImage.sprite = dialog.GetImage();
                dialogRightImage.enabled = false;
            }
            else
            {
                dialogRightImage.enabled = true;
                dialogRightImage.sprite = dialog.GetImage();
                dialogLeftImage.enabled = false;
            }

            if (dialog.choices.Count > 1)
            {
                RemoveChoices();
                foreach (var choice in dialog.choices)
                {
                    GameObject choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
                    choiceButton.GetComponentInChildren<Text>().text = choice;
                    choiceButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        DialogManager.Instance.MakeChoice(dialog.guid, choice);
                    });
                }
                
                choicesContainer.gameObject.SetActive(true);
            }
            else
            {
                choicesContainer.gameObject.SetActive(false);
            }
        }

        public override void Hide()
        {
            RemoveChoices();
            container.SetActive(false);
        }

        private void RemoveChoices()
        {
            if (choicesContainer.childCount > 0)
            {
                foreach (Transform child in choicesContainer)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}