using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for controlling the interaction in the shop scene.
/// </summary>
public class ShopComponent : MonoBehaviour {

    [SerializeField]
    private Text healthInfoText;
    [SerializeField]
    private Text healthUpgradeInfoText;
    [SerializeField]
    private Button healthUpgradeButton;
    [SerializeField]
    private Text healthUpgradeCostText;
    [SerializeField]
    private Text damageInfoText;
    [SerializeField]
    private Text damageUpgradeInfoText;
    [SerializeField]
    private Button damageUpgradeButton;
    [SerializeField]
    private Text damageUpgradeCostText;
    [SerializeField]
    private Text ammunitionInfoText;
    [SerializeField]
    private Text ammunitionUpgradeInfoText;
    [SerializeField]
    private Button ammunitionUpgradeButton;
    [SerializeField]
    private Text ammunitionUpgradeCostText;
    [SerializeField]
    private Text goldAmountText;
    [SerializeField]
    private Button backToLevelSelectButton;

    private int healthUpgradeCost;
    private int damageUpgradeCost;
    private int ammunitionUpgradeCost;
    private int availableGoldAmount;
    private UnitLevelData currentUnitLevelData;
    private UnitType currentUnitType;

    private PlayerModel PlayerModel { get { return PlayerModel.Instance; } }

    public void Awake() {
        if (this.healthInfoText == null) {
            throw new NullReferenceException("healthInfoText is null");
        }

        if (this.healthUpgradeInfoText == null) {
            throw new NullReferenceException("healthUpgradeInfoText is null");
        }

        if (this.healthUpgradeButton == null) {
            throw new NullReferenceException("healthUpgradeButton is null");
        }

        if (this.healthUpgradeCostText == null) {
            throw new NullReferenceException("healthUpgradeCostText is null");
        }

        if (this.damageInfoText == null) {
            throw new NullReferenceException("damageInfoText is null");
        }

        if (this.damageUpgradeInfoText == null) {
            throw new NullReferenceException("damageUpgradeInfoText is null");
        }

        if (this.damageUpgradeButton == null) {
            throw new NullReferenceException("damageUpgradeButton is null");
        }

        if (this.damageUpgradeCostText == null) {
            throw new NullReferenceException("damageUpgradeCostText is null");
        }

        if (this.ammunitionInfoText == null) {
            throw new NullReferenceException("ammunitionInfoText is null");
        }

        if (this.ammunitionUpgradeInfoText == null) {
            throw new NullReferenceException("ammunitionUpgradeInfoText is null");
        }

        if (this.ammunitionUpgradeButton == null) {
            throw new NullReferenceException("ammunitionUpgradeButton is null");
        }

        if (this.ammunitionUpgradeCostText == null) {
            throw new NullReferenceException("ammunitionUpgradeCostText is null");
        }

        if (this.goldAmountText == null) {
            throw new NullReferenceException("goldAmountText is null");
        }

        if (this.backToLevelSelectButton == null) {
            throw new NullReferenceException("backToLevelSelectButton is null");
        }

        this.backToLevelSelectButton.onClick.AddListener(GoToLevelSelect);
        this.healthUpgradeButton.onClick.AddListener(BuyHealthUpgrade);
        this.damageUpgradeButton.onClick.AddListener(BuyDamageUpgrade);
        this.ammunitionUpgradeButton.onClick.AddListener(BuyAmmunitionUpgrade);
    }

    public void Start() {

        this.UpdateAndDisplayShopData();
    }

    public void Destroy() {
        this.backToLevelSelectButton.onClick.RemoveListener(GoToLevelSelect);
        this.healthUpgradeButton.onClick.RemoveListener(BuyHealthUpgrade);
        this.damageUpgradeButton.onClick.RemoveListener(BuyDamageUpgrade);
        this.ammunitionUpgradeButton.onClick.RemoveListener(BuyAmmunitionUpgrade);
    }

    private void UpdateAndDisplayShopData() {
        PlayerGameData playerGameData = this.PlayerModel.PlayerGameData;
        this.currentUnitLevelData = playerGameData.HeroUnitLevelData;
        this.currentUnitType = playerGameData.HeroUnitType;
        this.availableGoldAmount = playerGameData.GoldAmount;
        this.goldAmountText.text = this.availableGoldAmount.ToString();
        this.UpdateHeroUpgradePrices();
        this.ShowHeroStats();
    }

    private void UpdateHeroUpgradePrices() {
        GameConstants gameConstants = GameMechanicsManager.GetGameConstanstsData();
        this.healthUpgradeCost = gameConstants.GetGoldCostForHeroStat(this.currentUnitLevelData.HealthUpgradeLevel);
        this.damageUpgradeCost = gameConstants.GetGoldCostForHeroStat(this.currentUnitLevelData.DamageUpgradeLevel);
        this.ammunitionUpgradeCost = gameConstants.GetGoldCostForHeroStat(this.currentUnitLevelData.AmmunitionUpgradeLevel);
    }

    private void ShowHeroStats() {
        this.healthInfoText.text = UnitStatsCalculator.CalculateMaxHealthValue(this.currentUnitType, this.currentUnitLevelData.HealthUpgradeLevel).ToString();
        this.damageInfoText.text = UnitStatsCalculator.CalculateDamagePerHitValue(this.currentUnitType, this.currentUnitLevelData.DamageUpgradeLevel).ToString();
        this.ammunitionInfoText.text = UnitStatsCalculator.CalculateAmmunitionValue(this.currentUnitType, this.currentUnitLevelData.AmmunitionUpgradeLevel).ToString();
        string upgradeInfoPrefix = "+";
        this.healthUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelMaxHealthIncreaseValue(this.currentUnitType, this.currentUnitLevelData.HealthUpgradeLevel).ToString();
        this.damageUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelDamageIncreaseValue(this.currentUnitType, this.currentUnitLevelData.DamageUpgradeLevel).ToString();
        this.ammunitionUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelAmmunitionIncreaseValue(this.currentUnitType, this.currentUnitLevelData.AmmunitionUpgradeLevel).ToString();
        this.healthUpgradeCostText.text = this.healthUpgradeCost.ToString();
        this.damageUpgradeCostText.text = this.damageUpgradeCost.ToString();
        this.ammunitionUpgradeCostText.text = this.ammunitionUpgradeCost.ToString();
        this.healthUpgradeButton.interactable = this.healthUpgradeCost <= this.availableGoldAmount;
        this.damageUpgradeButton.interactable = this.damageUpgradeCost <= this.availableGoldAmount;
        this.ammunitionUpgradeButton.interactable = this.ammunitionUpgradeCost <= this.availableGoldAmount;
    }

    private void BuyHealthUpgrade() {
        if (this.availableGoldAmount < this.healthUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.healthUpgradeCost);
        this.currentUnitLevelData.HealthUpgradeLevel++;
        this.UpdateAndDisplayShopData();
    }

    private void BuyDamageUpgrade() {
        if (this.availableGoldAmount < this.damageUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.damageUpgradeCost);
        this.currentUnitLevelData.DamageUpgradeLevel++;
        this.UpdateAndDisplayShopData();
    }

    private void BuyAmmunitionUpgrade() {
        if (this.availableGoldAmount < this.ammunitionUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.ammunitionUpgradeCost);
        this.currentUnitLevelData.AmmunitionUpgradeLevel++;
        this.UpdateAndDisplayShopData();
    }

    private void ReducePlayersGold(int amount) {
        this.PlayerModel.PlayerGameData.GoldAmount -= amount;
    }

    private void GoToLevelSelect() {
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }
}
