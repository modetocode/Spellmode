using System;
using System.Collections.Generic;

/// <summary>
/// Represents a simple object pool that have fetch/release/clear functionality.
/// </summary>
/// <typeparam name="ObjectType"></typeparam>
public class SimplePool<ObjectType> where ObjectType : class {

    private Func<ObjectType> factoryFunction;
    private Stack<ObjectType> objectPoolStack;

    public SimplePool(Func<ObjectType> factoryFunction, int initialCapacity) {
        if (factoryFunction == null) {
            throw new ArgumentNullException("factoryFunction");
        }

        if (initialCapacity < 0) {
            throw new ArgumentOutOfRangeException("initialCapacity", "Cannot be less than zero.");
        }

        this.factoryFunction = factoryFunction;
        this.objectPoolStack = new Stack<ObjectType>();
        for (int i = 0; i < initialCapacity; i++) {
            ObjectType newObject = factoryFunction();
            this.objectPoolStack.Push(newObject);
        }
    }

    public ObjectType Fetch() {
        ObjectType itemFromPool = null;
        if (this.objectPoolStack.Count == 0) {
            itemFromPool = factoryFunction();
        }
        else {
            itemFromPool = this.objectPoolStack.Pop();
        }

        return itemFromPool;
    }

    public void Release(ObjectType objectToRelease) {
        if (objectToRelease == null) {
            throw new ArgumentNullException("objectToRelease");
        }

        this.objectPoolStack.Push(objectToRelease);
    }

    public void Clear() {
        this.objectPoolStack.Clear();
    }
}

