using CasePlanner.Data.Notes;
using CasePlanner.UI;
using Common.UI;
using Michsky.UI.ModernUIPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CasePlanner.Data {
    public class DataManager_Legacy : MonoBehaviour {
        [SerializeField]
        private GameObject pathEntryPanel = null;

        [SerializeField]
        private TMP_InputField pathField = null;

        [SerializeField]
        private ButtonManager okButton = null;

        [SerializeField]
        private List<TMP_Text> okButtonTexts = new List<TMP_Text>();

        [SerializeField]
        private StickyNoteCreator noteCreator = null;

        [SerializeField]
        private PinConnector pinConnector = null;

        public string CasePath { get; set; }

        private bool isSaving = false;

        private void Awake() {
            CasePath = Path.Combine(Application.persistentDataPath, "Cases");

            if (!Directory.Exists(CasePath)) {
                Directory.CreateDirectory(CasePath);
            }

            if (!File.Exists(Path.Combine(CasePath, "Example.case"))) {
                File.WriteAllText(
                    Path.Combine(CasePath, "Example.case"), 
                    File.ReadAllText(Path.Combine(Application.streamingAssetsPath, 
                    "Example.case")));
            }
        }

        private void Update() {
            bool saveKey, loadKey;

            saveKey = Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.K);
            loadKey = Input.GetKeyDown(KeyCode.O) | Input.GetKeyDown(KeyCode.L);

            if (Input.GetKey(KeyCode.LeftControl) && (saveKey || loadKey)) {
                isSaving = saveKey;
                okButton.buttonText = isSaving ? "Save" : "Load";
                foreach (TMP_Text text in okButtonTexts) {
                    text.text = isSaving ? "Save" : "Load";
				}
                ShowPathPanel(true);
            }

            if (pathEntryPanel.activeInHierarchy) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    SaveOrLoad();
                } else if (Input.GetKeyDown(KeyCode.Escape)) {
                    ShowPathPanel(false);
                }
            }
        }

        public List<StickyNote> GetNoteList() {
            return GameObject.FindGameObjectsWithTag("Note").Select(o => o.GetComponent<StickyNote>()).OrderBy(n => n.ID).ToList();
        }

        public void CreateDeserializedNote(StickyNote.SerializeNote serializedNote) {
            StickyNote note = noteCreator.CreateNote();
            note.Deserialize(serializedNote);
        }

        public void CreateDeserializedEdges(Yarn.Edge[] edges) {
            foreach (Yarn.Edge edge in edges) {
                if (edge is null || (edge.a == edge.b)) {
                    continue;
                }

                pinConnector.A = GetFromID(edge.a).Pin;
                pinConnector.B = GetFromID(edge.b).Pin;
                pinConnector.Connect();
            }
        }

        public void ShowPathPanel(bool show) {
            pathEntryPanel.SetActive(show);
            if (show) {
                pathField.Select();
                pathField.ActivateInputField();
            }
            Camera.main.GetComponent<PanCamera>().enabled = !show;
        }

        public void SaveOrLoad() {
            if (isSaving) {
                Serializer_Legacy.Serialize(this, pathField.text);
            } else {
                foreach (StickyNote note in GetNoteList()) {
                    Destroy(note.gameObject);
                }
                noteCreator.NoteCount = 0;
                Serializer_Legacy.Deserialize(this, pathField.text);
            }

            ShowPathPanel(false);
        }

        private StickyNote GetFromID(int id) {
            return GetNoteList().Where(n => n.ID == id).FirstOrDefault();
        }
    }
}
