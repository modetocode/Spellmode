using System;
using System.Collections.Generic;

public class Ticker {
    private IList<ITickable> tickableObjects;

    public Ticker(IList<ITickable> tickableObjects) {
        if (tickableObjects == null) {
            throw new ArgumentNullException("tickableObjects");
        }

        this.tickableObjects = tickableObjects;
    }

    public void Tick(float deltaTime) {
        for (int i = 0; i < tickableObjects.Count; i++) {
            ITickable tickableObject = tickableObjects[i];
            tickableObject.Tick(deltaTime);
        }
    }

    public void AddTickableObject(ITickable tickableObjectToAdd) {
        this.tickableObjects.Add(tickableObjectToAdd);
    }

    public void RemoveTickableObject(ITickable tickableObjectToRemove) {
        this.tickableObjects.Remove(tickableObjectToRemove);
    }
}
