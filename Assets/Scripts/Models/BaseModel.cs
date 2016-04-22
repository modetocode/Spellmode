public abstract class BaseModel<T> : Singleton<T> where T : Singleton<T>, new() {
    public abstract void Clear();
}
