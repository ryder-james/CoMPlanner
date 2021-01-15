using System.IO;
using UnityEngine;

namespace CasePlanner.Data.Management {
	public class UnitySerializer<T> : ISerializer<T> {
		public void Serialize(T serializable, string path) {
			string output = JsonUtility.ToJson(serializable);
			File.WriteAllText(path, output);
		}

		public T Deserialize(string path) {
			T result = JsonUtility.FromJson<T>(File.ReadAllText(path));

			return result;
		}
	}
}
