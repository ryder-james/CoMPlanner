namespace CasePlanner.Data {
    public interface ISerializable<T> {
        T Serialized();
        void Deserialize(T obj);
    }
}