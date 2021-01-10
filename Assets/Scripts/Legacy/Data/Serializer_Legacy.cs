using UnityEngine;

using CasePlanner.Data.Notes;
using System.IO;
using System.Collections.Generic;

namespace CasePlanner.Data {
    [System.Serializable]
    public static class Serializer_Legacy {
        [System.Serializable]
        public class SerializeBoard {
            public StickyNote_Legacy.SerializeNote[] notes;
            public Yarn_Legacy.Edge[] edges;
        }

        public static void Serialize(DataManager_Legacy manager, string casePath) {
			SerializeBoard board = new SerializeBoard();

			SerializeNotes(manager.Notes, ref board);

			string output = JsonUtility.ToJson(board);
			File.WriteAllText(casePath, output);
		}

        public static void Deserialize(DataManager_Legacy manager, string casePath) {
			SerializeBoard board = (SerializeBoard) JsonUtility.FromJson(File.ReadAllText(casePath), typeof(SerializeBoard));

			foreach (StickyNote_Legacy.SerializeNote note in board.notes) {
				manager.CreateDeserializedNote(note);
			}

			manager.CreateDeserializedEdges(board.edges);
		}

        private static void SerializeNotes(List<StickyNote_Legacy> notes, ref SerializeBoard board) {
            List<StickyNote_Legacy.SerializeNote> serializedNotes = new List<StickyNote_Legacy.SerializeNote>();
            List<Yarn_Legacy.Edge> serializedEdges = new List<Yarn_Legacy.Edge>();

            foreach (StickyNote_Legacy note in notes) {
                serializedNotes.Add(note.Serialized());

                foreach (Yarn_Legacy edge in note.Edges) {
                    Yarn_Legacy.Edge e = edge.Serialized();

                    if (!serializedEdges.Contains(e)) {
                        serializedEdges.Add(e);
                    }
                }
            }

            board.notes = serializedNotes.ToArray();
            board.edges = serializedEdges.ToArray();
        }
    }
}