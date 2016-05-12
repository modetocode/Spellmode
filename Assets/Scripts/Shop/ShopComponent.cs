using System;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField]
    private YesNoPopupComponent yesNoPopupComponent;

    private int healthUpgradeCost;
    private int damageUpgradeCost;
    private int ammunitionUpgradeCost;
    private int availableGoldAmount;
    private UnitLevelData currentUnitLevelData;
    private UnitType currentUnitType;
    private PlayerGameData playerGameData;

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

        if (this.yesNoPopupComponent == null) {
            throw new NullReferenceException("yesNoPopupComponent is null");
        }

        this.backToLevelSelectButton.onClick.AddListener(GoToLevelSelect);
        this.healthUpgradeButton.onClick.AddListener(ShowHealthUpgradeYesNoPopup);
        this.damageUpgradeButton.onClick.AddListener(ShowDamagePerHitUpgradeYesNoPopup);
        this.ammunitionUpgradeButton.onClick.AddListener(ShowAmmunitionUpgradeYesNoPopup);
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
        this.playerGameData = this.PlayerModel.PlayerGameData;
        PlayerHeroData currentHeroData = playerGameData.GetHeroData(UnitType.HeroUnit);
        this.currentUnitLevelData = currentHeroData.HeroLevelData;
        this.currentUnitType = currentHeroData.HeroType;
        this.availableGoldAmount = playerGameData.GoldAmount;
        this.goldAmountText.text = this.availableGoldAmount.ToString();
        this.UpdateHeroUpgradePrices();
        this.ShowHeroStats();
    }

    private void UpdateHeroUpgradePrices() {
        GameConstants gameConstants = GameMechanicsManager.GetGameConstantsData();
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

    private void ShowHealthUpgradeYesNoPopup() {
        string newHealthValue = UnitStatsCalculator.CalculateMaxHealthValue(this.currentUnitType, this.currentUnitLevelData.HealthUpgradeLevel + 1).ToString();
        string message = string.Format(Constants.Strings.ShopHealthUpgradeYesNoMesssageTemplate, newHealthValue);
        UnityAction onYesClickedAction = BuyHealthUpgrade;
        this.yesNoPopupComponent.Show(message, onYesClickedAction);
    }

    private void ShowDamagePerHitUpgradeYesNoPopup() {
        string newDamageValue = UnitStatsCalculator.CalculateDamagePerHitValue(this.currentUnitType, this.currentUnitLevelData.DamageUpgradeLevel + 1).ToString();
        string message = string.Format(Constants.Strings.ShopDamageUpgradeYesNoMessageTemplate, newDamageValue);
        UnityAction onYesClickedAction = BuyDamageUpgrade;
        this.yesNoPopupComponent.Show(message, onYesClickedAction);
    }

    private void ShowAmmunitionUpgradeYesNoPopup() {
        string newAmmunitionValue = UnitStatsCalculator.CalculateAmmunitionValue(this.currentUnitType, this.currentUnitLevelData.AmmunitionUpgradeLevel + 1).ToString();
        string message = string.Format(Constants.Strings.ShopAmmunitionUpgradeYesNoMessageTemplate, newAmmunitionValue);
        UnityAction onYesClickedAction = BuyAmmunitionUpgrade;
        this.yesNoPopupComponent.Show(message, onYesClickedAction);
    }

    private void BuyHealthUpgrade() {
        if (this.availableGoldAmount < this.healthUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.healthUpgradeCost);
        UnitLevelData newLevelData = new UnitLevelData(
            healthUpgradeLevel: this.currentUnitLevelData.HealthUpgradeLevel + 1,
            damageUpgradeLevel: this.currentUnitLevelData.DamageUpgradeLevel,
            ammunitionUpgradeLevel: this.currentUnitLevelData.AmmunitionUpgradeLevel);
        this.UpdateCurrentUnitData(newLevelData);
        this.UpdateAndDisplayShopData();
    }

    private void BuyDamageUpgrade() {
        if (this.availableGoldAmount < this.damageUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.damageUpgradeCost);
        UnitLevelData newLevelData = new UnitLevelData(
            healthUpgradeLevel: this.currentUnitLevelData.HealthUpgradeLevel,
            damageUpgradeLevel: this.currentUnitLevelData.DamageUpgradeLevel + 1,
            ammunitionUpgradeLevel: this.currentUnitLevelData.AmmunitionUpgradeLevel);
        this.UpdateCurrentUnitData(newLevelData);
        this.UpdateAndDisplayShopData();
    }

    private void BuyAmmunitionUpgrade() {
        if (this.availableGoldAmount < this.ammunitionUpgradeCost) {
            throw new InvalidOperationException("No gold to buy the update");
        }

        this.ReducePlayersGold(this.ammunitionUpgradeCost);
        UnitLevelData newLevelData = new UnitLevelData(
            healthUpgradeLevel: this.currentUnitLevelData.HealthUpgradeLevel,
            damageUpgradeLevel: this.currentUnitLevelData.DamageUpgradeLevel,
            ammunitionUpgradeLevel: this.currentUnitLevelData.AmmunitionUpgradeLevel + 1);
        this.UpdateCurrentUnitData(newLevelData);
        this.UpdateAndDisplayShopData();
    }

    private void ReducePlayersGold(int amount) {
        this.PlayerModel.PlayerGameData.GoldAmount -= amount;
    }

    private void GoToLevelSelect() {
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }

    private void UpdateCurrentUnitData(UnitLevelData newLevelData) {
        PlayerHeroData updatedUnitData = new PlayerHeroData(this.currentUnitType, newLevelData);
        this.playerGameData.UpdateHeroData(updatedUnitData);
    }
}
