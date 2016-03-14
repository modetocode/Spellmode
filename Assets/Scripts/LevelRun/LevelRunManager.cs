public class LevelRunManager : ITickable {

    public AttackingTeam attackingTeam { get; private set; }
    public DefendingTeam defendingTeam { get; private set; }
    private Ticker ticker { get; set; }

    public void InitializeRun() {
        //TODO get the appropriate data and set it
        this.attackingTeam = new AttackingTeam();
        this.defendingTeam = new DefendingTeam();
        this.ticker = new Ticker(new ITickable[] { this.attackingTeam, this.defendingTeam });
    }

    public void Tick(float deltaTime) {
        this.ticker.Tick(deltaTime);
    }
}