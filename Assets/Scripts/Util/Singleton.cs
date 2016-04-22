public class Singleton<T> where T : class, new() {

    public static T Instance { get { return Nested.instance; } }

    // Ensure that singleton is thread safe and lazy
    private class Nested {
        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static Nested() { }

        internal static readonly T instance = new T();
    }
}
