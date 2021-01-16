namespace JCommon.Data.Seralization {
    public interface ISerializable<T> {
        T Serialized();
        void Deserialize(T obj);
    }
}