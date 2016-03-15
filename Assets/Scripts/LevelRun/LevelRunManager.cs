public class LevelRunManager : ITickable {

    public AttackingTeam AttackingTeam { get; private set; }
    public DefendingTeam DefendingTeam { get; private set; }
    private Ticker Ticker { get; set; }

    public void InitializeRun() {
        //TODO get the appropriate data and set it
        this.AttackingTeam = new AttackingTeam();
        this.DefendingTeam = new DefendingTeam();
        this.Ticker = new Ticker(new ITickable[] { this.AttackingTeam, this.DefendingTeam });
    }

    public void Tick(float deltaTime) {
        this.Ticker.Tick(deltaTime);
    }
}