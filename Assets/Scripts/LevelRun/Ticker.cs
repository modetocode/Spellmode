using System;
using System.Collections.Generic;

public class Ticker {
    private IList<ITickable> tickableObjects;

    /// <summary>
    /// Returns true if the ticker has finished ticking.
    /// </summary>
    public bool TickingFinished { get; private set; }

    public Ticker(IList<ITickable> tickableObjects) {
        if (tickableObjects == null) {
            throw new ArgumentNullException("tickableObjects");
        }

        this.tickableObjects = new List<ITickable>(tickableObjects);
        this.TickingFinished = false;
    }

    public void Tick(float deltaTime) {
        this.CheckIsTickingFinished();
        for (int i = 0; i < this.tickableObjects.Count; i++) {
            ITickable tickableObject = this.tickableObjects[i];
            tickableObject.Tick(deltaTime);
        }
    }

    public void FinishTicking() {
        this.CheckIsTickingFinished();
        this.TickingFinished = true;
        for (int i = 0; i < this.tickableObjects.Count; i++) {
            this.tickableObjects[i].OnTickingFinished();
        }
    }

    public void AddTickableObject(ITickable tickableObjectToAdd) {
        this.tickableObjects.Add(tickableObjectToAdd);
    }

    public void RemoveTickableObject(ITickable tickableObjectToRemove) {
        this.tickableObjects.Remove(tickableObjectToRemove);
    }

    private void CheckIsTickingFinished() {
        if (this.TickingFinished) {
            throw new InvalidOperationException("Ticking is already finished");
        }
    }
}
