namespace CasePlanner.Data.Management {
	public interface ISerializer<T> {
		void Serialize(T serializable, string path);
		T Deserialize(string path);
	}
}