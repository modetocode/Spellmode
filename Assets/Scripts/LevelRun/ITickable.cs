public interface ITickable {

    /// <summary>
    /// Executed regularly when the ticking is not paused.
    /// </summary>
    /// <param name="deltaTime"></param>
    void Tick(float deltaTime);

    /// <summary>
    /// Executed regularly when the ticking is paused
    /// </summary>
    /// <param name="deltaTime"></param>
    void OnTickingPaused(float deltaTime);
}