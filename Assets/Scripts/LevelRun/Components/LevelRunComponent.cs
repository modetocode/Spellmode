using System;
using UnityEngine;

class LevelRunComponent : MonoBehaviour {

    public event Action Initialized;

    [SerializeField]
    private InputComponent inputComponent;

    [SerializeField]
    private BackgroundComponent backgroundComponent;

    [SerializeField]
    private InstantiatorComponent instantiatorComponent;

    [SerializeField]
    private LevelRunCameraComponent cameraComponent;

    [SerializeField]
    private LevelRunGUIComponent levelRunGuiComponent;

    private LevelRunManager levelRunManager;
    private bool runFinished;

    public bool IsInitialized { get; private set; }
    public bool IsGamePaused { get { return this.levelRunManager.IsGamePaused; } }
    public bool IsGameFinished { get { return this.levelRunManager.IsGameFinished; } }
    private LevelRunModel LevelRunModel { get { return LevelRunModel.Instance; } }
    private PlayerModel PlayerModel { get { return PlayerModel.Instance; } }

    public void Awake() {
        if (this.inputComponent == null) {
            throw new NullReferenceException("inputComponent is null");
        }

        if (this.instantiatorComponent == null) {
            throw new NullReferenceException("instantiatorComponent is null");
        }

        if (this.cameraComponent == null) {
            throw new NullReferenceException("cameraComponent is null");
        }

        if (this.levelRunGuiComponent == null) {
            throw new NullReferenceException("levelRunGuiComponent is null");
        }
    }

    public void Start() {
        this.runFinished = false;
        this.LevelRunModel.RunFinished += FinishRun;
        this.levelRunManager = new LevelRunManager();
        this.levelRunManager.InitializeRun(this.LevelRunModel);
        this.levelRunManager.AttackingTeam.UnitAdded += OnUnitInTeamAdded;
        this.levelRunManager.DefendingTeam.UnitAdded += OnUnitInTeamAdded;
        this.levelRunManager.BulletManager.BulletAdded += OnNewBulletAdded;
        this.levelRunManager.LootItemManager.LootItemAdded += OnNewLootItemAdded;
        this.levelRunManager.GameStarted += OnGameStarted;
        this.levelRunManager.GamePaused += OnGamePaused;
        this.levelRunManager.GameResumed += OnGameResumed;
        this.instantiatorComponent.HeroUnitInstantiated += OnNewUnitInstantiated;
        this.inputComponent.JumpDownInputed += JumpDownInputedHandler;
        this.inputComponent.JumpUpInputed += JumpUpInputedHandler;
        this.inputComponent.PauseInputed += PauseInputedHandler;
        this.inputComponent.ShootInputed += ShootInputedHandler;
        this.backgroundComponent.PauseMovement();
        this.levelRunManager.SpawnStartingUnits();
        this.IsInitialized = true;
        if (this.Initialized != null) {
            this.Initialized();
        }
    }

    public void Update() {
        if (!this.levelRunManager.IsGameStarted) {
            return;
        }

        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        if (this.runFinished) {
            return;
        }

        this.levelRunManager.Tick(Time.deltaTime);
    }

    private void FinishRun(LevelRunFinishType finishType) {
        this.LevelRunModel.RunFinished -= FinishRun;
        this.UnsubscribeFromEvents();
        this.backgroundComponent.PauseMovement();
        if (finishType == LevelRunFinishType.RunCompleted) {
            int goldLootedAmount = this.LevelRunModel.LootItemManager.GetCollectedLootAmountByType(LootItemType.Gold);
            GameConstants gameConstants = GameMechanicsManager.GetGameConstanstsData();
            int levelNumber = this.LevelRunModel.LevelRunData.LevelNumber;
            int levelCompletedGoldAmount = gameConstants.GetGoldRewardForLevel(levelNumber);
            this.PlayerModel.PlayerGameData.GoldAmount += goldLootedAmount + levelCompletedGoldAmount;
            this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber = Math.Max(this.PlayerModel.PlayerGameData.HighestCompletedLevelNumber, levelNumber);
            this.LevelRunModel.LevelCompletedRewardData = new LevelCompetedRewardData(levelCompletedGoldAmount, goldLootedAmount, this.PlayerModel.PlayerGameData.GoldAmount);
        }

        this.runFinished = true;
    }

    private void OnUnitInTeamAdded(Unit newUnit) {
        this.instantiatorComponent.InstantiateUnit(newUnit);
    }

    private void OnNewBulletAdded(Bullet newBullet) {
        this.instantiatorComponent.InstantiateBullet(newBullet);
    }


    private void OnNewLootItemAdded(LootItem lootItem) {
        this.instantiatorComponent.InstantiateLootItem(lootItem);
    }

    private void OnNewUnitInstantiated(UnitComponent unitComponent) {
        if (this.levelRunManager.AttackingTeam.IsUnitInTeam(unitComponent.Unit)) {
            this.cameraComponent.TrackObject(unitComponent.gameObject);
        }
    }

    private void JumpDownInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToLowerPlatformIfPossible();
    }

    private void JumpUpInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.AttackingTeam.MoveAllAliveUnitsToUpperPlatformIfPossible();
    }

    private void PauseInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            this.levelRunManager.ResumeGame();

        }
        else {
            this.levelRunManager.PauseGame();
        }
    }

    private void ShootInputedHandler() {
        if (this.levelRunManager.IsGamePaused) {
            return;
        }

        this.levelRunManager.CombatManager.TriggerManualAttack();
    }

    private void OnGameStarted() {
        Unit heroUnit = this.LevelRunModel.HeroUnit;
        this.backgroundComponent.SetMoveSpeed(heroUnit.MovementSpeed * Constants.Scenes.LevelRun.BackgroundSpeedFactor);
        this.backgroundComponent.ResumeMovement();
    }

    private void OnGameResumed() {
        this.backgroundComponent.ResumeMovement();
    }

    private void OnGamePaused() {
        this.backgroundComponent.PauseMovement();
    }

    public void UnsubscribeFromEvents() {
        this.levelRunManager.AttackingTeam.UnitAdded -= OnUnitInTeamAdded;
        this.levelRunManager.DefendingTeam.UnitAdded -= OnUnitInTeamAdded;
        this.instantiatorComponent.HeroUnitInstantiated -= OnNewUnitInstantiated;
        this.levelRunManager.BulletManager.BulletAdded -= OnNewBulletAdded;
        this.levelRunManager.LootItemManager.LootItemAdded -= OnNewLootItemAdded;
        this.levelRunManager.GameStarted -= OnGameStarted;
        this.levelRunManager.GamePaused -= OnGamePaused;
        this.levelRunManager.GameResumed -= OnGameResumed;
        this.inputComponent.JumpDownInputed -= JumpDownInputedHandler;
        this.inputComponent.JumpUpInputed -= JumpUpInputedHandler;
        this.inputComponent.PauseInputed -= PauseInputedHandler;
        this.inputComponent.ShootInputed -= ShootInputedHandler;
    }

    public void PauseGame() {
        this.levelRunManager.PauseGame();
    }

    public void ResumeGame() {
        this.levelRunManager.ResumeGame();
    }

    public void StartRun() {
        this.levelRunManager.StartRun();
    }

    public void FinishRun() {
        this.levelRunManager.FinishRun();
    }
}