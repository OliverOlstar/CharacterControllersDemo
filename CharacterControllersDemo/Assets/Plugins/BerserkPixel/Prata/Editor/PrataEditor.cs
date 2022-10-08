using BerserkPixel.Prata.Utilities;
using UnityEditor;
using UnityEngine;

namespace BerserkPixel.Prata
{
    public class PrataEditor
    {
        [MenuItem("Prata/Setup Scene")]
        public static void Setup()
        {
            var prevDialogManager = Object.FindObjectOfType<DialogManager>();
            if (prevDialogManager == null)
            {
                GameObject g = new GameObject("Dialog Manager");
                g.AddComponent<DialogManager>();
            }
            
            GraphSaveUtilities.GenerateFolders();
            GraphSaveUtilities.CreateFirstCharacter("Player");
        }

        [MenuItem("Prata/Create/New Character")]
        public static void CreateCharacter()
        {
            var character = ScriptableObject.CreateInstance<Character>();

            GraphSaveUtilities.GenerateFolders();

            AssetDatabase.CreateAsset(character, $"{PrataConstants.FOLDER_CHARACTERS_COMPLETE}/New Character.asset");
            EditorUtility.SetDirty(character);
            AssetDatabase.SaveAssets();
            
            Selection.activeObject = character;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("Prata/Create/New Interaction")]
        public static void CreateInteraction()
        {
            var interaction = ScriptableObject.CreateInstance<Interaction>();

            GraphSaveUtilities.GenerateFolders();

            AssetDatabase.CreateAsset(interaction,
                $"{PrataConstants.FOLDER_INTERACTIONS_COMPLETE}/New Interaction.asset");
            EditorUtility.SetDirty(interaction);
            AssetDatabase.SaveAssets();

            Selection.activeObject = interaction;
            SceneView.FrameLastActiveSceneView();
        }

        #region Window Menus

        [MenuItem("Window/Prata/Leave a Review")]
        public static void OpenReviewsPage()
        {
            Application.OpenURL(BerserkURL.URL_REVIEWS);
        }

        [MenuItem("Window/Prata/Learning Resources/Online Manual")]
        public static void ShowOnlineManual()
        {
            Application.OpenURL(BerserkURL.URL_PRATA_DOCS);
        }

        [MenuItem("Window/Prata/Learning Resources/Youtube")]
        public static void ShowYoutube()
        {
            Application.OpenURL(BerserkURL.URL_PRATA_YOUTUBE);
        }

        [MenuItem("Window/Prata/Explore/Berserk Pixel\'s Assets")]
        public static void OpenBerserkStorePage()
        {
            Application.OpenURL(BerserkURL.URL_STORE_PAGE);
        }

        [MenuItem("Window/Prata/Contact/Support")]
        public static void ShowSupportEmailEditor()
        {
            BerserkURL.OpenEmailEditor(
                BerserkURL.URL_SUPPORT_EMAIL,
                "[Prata] SHORT_QUESTION_HERE",
                "YOUR_QUESTION_IN_DETAIL");
        }

        [MenuItem("Window/Prata/Contact/Business")]
        public static void ShowBusinessEmailEditor()
        {
            BerserkURL.OpenEmailEditor(
                BerserkURL.URL_BUSINESS_EMAIL,
                "[Prata] SHORT_QUESTION_HERE",
                "YOUR_QUESTION_IN_DETAIL");
        }

        #endregion
    }
}