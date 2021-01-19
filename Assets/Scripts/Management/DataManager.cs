using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using SimpleFileBrowser;

using JCommon.UI;
using JCommon.Data;
using JCommon.Data.Seralization;

using CasePlanner.Data.Views;
using CasePlanner.Data.Components;

namespace CasePlanner.Management {
    public class DataManager : MonoBehaviour {
        [SerializeField] private NoteManager noteManager = null;
        [SerializeField] private PinConnector pinConnector = null;
        [SerializeField] private GameObject settingsMenu = null;
        [SerializeField] private PanCamera panCam = null;

        private string activeSeriesName = null;
        private bool settingsMenuIsOpen = false;

        private readonly UnitySerializer<Series.SerializedSeries> serializer
            = new UnitySerializer<Series.SerializedSeries>();

        private void Awake() {
            noteManager.ActiveSeries = new Series();
            noteManager.ActiveSeries.AddCase(new Case());
        }

        private void Start() {
            FileBrowser.SetFilters(false,
                new FileBrowser.Filter("Case Files", ".series"));
        }

        private void Update() {
            bool saveKey, loadKey;

            saveKey = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K);
            loadKey = Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.L);

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                if (saveKey) {
                    StartCoroutine(nameof(ShowSaveDialogAsync));
                } else if (loadKey) {
                    StartCoroutine(nameof(ShowLoadDialogAsync));
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                ToggleSettings();
			}
        }

        public void Quit() {
            Application.Quit();
		}

        private void ToggleSettings() {
            settingsMenuIsOpen = !settingsMenuIsOpen;

            settingsMenu.SetActive(settingsMenuIsOpen);
            panCam.CamEnabled = !settingsMenuIsOpen;

		}

        private IEnumerator ShowSaveDialogAsync() {
            string prevPath = PlayerPrefs.GetString("PrevPath", null);
            panCam.CamEnabled = false;
            yield return FileBrowser.WaitForSaveDialog(
                FileBrowser.PickMode.Files, false, prevPath, activeSeriesName);
            panCam.CamEnabled = true;

            if (FileBrowser.Success) {
                string path = FileBrowser.Result[0];
                int indexOfLastSeparator = path.LastIndexOf(Path.DirectorySeparatorChar) + 1;
                PlayerPrefs.SetString("PrevPath", path.Substring(0, indexOfLastSeparator));
                activeSeriesName = path.Substring(indexOfLastSeparator);
                serializer.Serialize(noteManager.ActiveSeries.Serialized(), path);
            }
        }

        private IEnumerator ShowLoadDialogAsync() {
            string prevPath = PlayerPrefs.GetString("PrevPath", null);
            panCam.CamEnabled = false;
            yield return FileBrowser.WaitForLoadDialog(
                FileBrowser.PickMode.Files, false, prevPath, activeSeriesName);
            panCam.CamEnabled = true;

            if (FileBrowser.Success) {
                noteManager.ClearNotes();

                string path = FileBrowser.Result[0];
                int indexOfLastSeparator = path.LastIndexOf(Path.DirectorySeparatorChar) + 1;
                PlayerPrefs.SetString("PrevPath", path.Substring(0, indexOfLastSeparator));
                activeSeriesName = path.Substring(indexOfLastSeparator);
                noteManager.ActiveSeries.Deserialize(serializer.Deserialize(path));

                noteManager.PopulateNotes();
                ConnectPins();
            }
        }

        private void ConnectPins() {
            List<Edge> edges = new List<Edge>();
            foreach (NoteView nv in noteManager.Notes) {
                foreach (int bSide in nv.Note.Connections) {
                    if (nv.Note.ID == bSide) {
                        continue;
                    }

                    Edge e = new Edge {
                        a = nv.Note.ID,
                        b = bSide
                    };

                    if (!edges.Contains(e)) {
                        edges.Add(e);
                    }
                }
            }

            foreach (Edge edge in edges) {
                pinConnector.A = noteManager.GetNoteByID(edge.a).Pin;
                pinConnector.B = noteManager.GetNoteByID(edge.b).Pin;
                pinConnector.Connect();
            }
        }
    }
}