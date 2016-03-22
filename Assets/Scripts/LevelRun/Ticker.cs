using System;
using System.Collections.Generic;

public class Ticker {
    private IList<ITickable> tickableObjects;

    /// <summary>
    /// Returns true if the ticker is ticking, or false if the ticker is paused
    /// </summary>
    public bool IsTicking { get; private set; }

    public Ticker(IList<ITickable> tickableObjects) {
        if (tickableObjects == null) {
            throw new ArgumentNullException("tickableObjects");
        }

        this.tickableObjects = new List<ITickable>(tickableObjects);
        this.IsTicking = true;
    }

    public void Tick(float deltaTime) {
        for (int i = 0; i < tickableObjects.Count; i++) {
            ITickable tickableObject = tickableObjects[i];
            if (this.IsTicking) {
                tickableObject.Tick(deltaTime);
            }
            else {
                tickableObject.OnTickingPaused(deltaTime);
            }
        }
    }

    public void PauseTicking() {
        this.IsTicking = false;
    }

    public void ResumeTicking() {
        this.IsTicking = true;
    }

    public void AddTickableObject(ITickable tickableObjectToAdd) {
        this.tickableObjects.Add(tickableObjectToAdd);
    }

    public void RemoveTickableObject(ITickable tickableObjectToRemove) {
        this.tickableObjects.Remove(tickableObjectToRemove);
    }
}
