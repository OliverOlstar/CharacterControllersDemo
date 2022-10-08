using BerserkPixel.Prata.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata
{
    public class PrataEditorWindow : EditorWindow
    {
        private PrataGraphView graphView;
        private string _filename = "New Conversation";

        private TextField filenameTextField;
        private ObjectField loadFileField;

        [MenuItem("Prata/Graph View #p")]
        public static void Open()
        {
            GetWindow<PrataEditorWindow>("Dialog Graph");
        }

        private void CreateGUI()
        {
            GraphSaveUtilities.GenerateFolders();
            GraphSaveUtilities.CreateFirstCharacter("Player");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddMiniMap();
            AddStyles();
            AddToolbar();
            AddSecondaryToolbar();
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        private void OnDestroy()
        {
            AssetDatabase.SaveAssets();
        }

        #region Elements Addition

        private void AddToolbar()
        {
            var toolbar = new Toolbar();

            filenameTextField = PrataElementsUtilities.CreateTextField(
                "Filename:",
                "Filename:",
                evt => { _filename = evt.newValue; });
            filenameTextField.AddClasses(
                "prata-node_textfield",
                "prata-node_quote-textfield"
            );
            filenameTextField.SetValueWithoutNotify(_filename);
            filenameTextField.MarkDirtyRepaint();
            toolbar.Add(filenameTextField);

            var saveButton = PrataElementsUtilities.CreateButton("Save Data", SaveData);
            var clearButton = PrataElementsUtilities.CreateButton("Clear All", ClearData);

            toolbar.Add(saveButton);
            toolbar.Add(clearButton);

            rootVisualElement.Insert(1, toolbar);
        }

        private void AddSecondaryToolbar()
        {
            var toolbar = new Toolbar();

            loadFileField = PrataElementsUtilities.CreateObjectField<DialogContainer>("Load Graph");
            loadFileField.RegisterCallback<ChangeEvent<Object>>((evt) =>
            {
                if (evt.newValue == null)
                {
                    ResetTextfields();
                    return;
                }

                _filename = evt.newValue.name;
                filenameTextField.SetValueWithoutNotify(_filename);
                filenameTextField.MarkDirtyRepaint();
                LoadData((DialogContainer)loadFileField.value);
            });

            toolbar.Add(loadFileField);

            rootVisualElement.Insert(2, toolbar);
        }

        private void AddGraphView()
        {
            graphView = new PrataGraphView(this);
            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("PrataVariables");
        }

        private void AddMiniMap()
        {
            var miniMap = new MiniMap { anchored = true };
            miniMap.SetPosition(new Rect(10, 55, 200, 140));
            graphView.Add(miniMap);
        }

        #endregion

        private void SaveData()
        {
            if (string.IsNullOrEmpty(_filename))
            {
                EditorUtility.DisplayDialog("Invalid filename", "Please enter a valid filename", "Ok");
                return;
            }

            var saveUtility = GraphSaveUtilities.GetInstance(graphView);
            var previousGraph = (DialogContainer)loadFileField.value;
            if (previousGraph != null)
            {
                saveUtility.OverwriteGraph((DialogContainer)loadFileField.value);
            }
            else
            {
                saveUtility.SaveGraph(_filename);
            }
        }

        private void LoadData(DialogContainer dialogContainer)
        {
            if (string.IsNullOrEmpty(_filename))
            {
                EditorUtility.DisplayDialog("Invalid filename", "Please enter a valid filename", "Ok");
                return;
            }

            var loadUtility = GraphSaveUtilities.GetInstance(graphView);
            loadUtility.LoadGraph(dialogContainer);
        }

        private void ClearData()
        {
            var choice = EditorUtility.DisplayDialogComplex(
                "Are you sure?",
                "This will clear everything. There's no turning back",
                "Yes",
                "Cancel",
                "");

            if (choice == 0)
            {
                var clearUtility = GraphSaveUtilities.GetInstance(graphView);
                clearUtility.ClearAll();
                ResetTextfields();
            }
        }

        private void ResetTextfields()
        {
            loadFileField.value = null;
            _filename = "New Conversation";
            filenameTextField.SetValueWithoutNotify(_filename);
            filenameTextField.MarkDirtyRepaint();
        }
    }
}