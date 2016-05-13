using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a container that can store multiple object pools.
/// </summary>
public class ObjectPoolContainer {
    private IDictionary<string, SimplePool<GameObject>> objectPools;

    public ObjectPoolContainer() {
        this.objectPools = new Dictionary<string, SimplePool<GameObject>>();
    }

    public bool CreatePool(string poolName, Func<GameObject> factoryFunction, int initialCapacity) {
        if (factoryFunction == null) {
            throw new ArgumentNullException("factoryFunction");
        }

        if (initialCapacity < 0) {
            throw new ArgumentOutOfRangeException("initialCapacity", "Cannot be less than zero.");
        }

        if (this.objectPools.ContainsKey(poolName)) {
            return false;
        }

        SimplePool<GameObject> newPool = new SimplePool<GameObject>(factoryFunction, initialCapacity);
        this.objectPools.Add(poolName, newPool);
        return true;
    }

    public GameObject FetchObject(string poolName) {
        GameObject fetchedGameObject = this.objectPools[poolName].Fetch();
        fetchedGameObject.SetActive(true);
        return fetchedGameObject;
    }

    public void ReleaseObject(GameObject gameObject, string poolName) {
        if (gameObject == null) {
            throw new ArgumentNullException("gameObject");
        }

        if (!this.objectPools.ContainsKey(poolName)) {
            throw new InvalidOperationException("There is no pool for the given pool name");
        }

        this.objectPools[poolName].Release(gameObject);
        gameObject.SetActive(false);
    }

    public void ClearPools() {
        foreach (var pool in objectPools.Values) {
            pool.Clear();
        }
    }

    public bool ContainsPool(string poolName) {
        return this.objectPools.ContainsKey(poolName);
    }
}