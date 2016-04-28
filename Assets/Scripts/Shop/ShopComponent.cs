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
    }

    public void Start() {
        this.ShowHeroStats();
        this.goldAmountText.text = this.PlayerModel.PlayerGameData.GoldAmount.ToString();
    }

    public void Destroy() {
        this.backToLevelSelectButton.onClick.RemoveListener(GoToLevelSelect);
    }

    private void ShowHeroStats() {
        UnitLevelData unitLevelData = this.PlayerModel.PlayerGameData.HeroUnitLevelData;
        UnitType heroType = this.PlayerModel.PlayerGameData.HeroUnitType;
        this.healthInfoText.text = UnitStatsCalculator.CalculateMaxHealthValue(heroType, unitLevelData.HealthUpgradeLevel).ToString();
        this.damageInfoText.text = UnitStatsCalculator.CalculateDamagePerHitValue(heroType, unitLevelData.DamageUpgradeLevel).ToString();
        this.ammunitionInfoText.text = UnitStatsCalculator.CalculateAmmunitionValue(heroType, unitLevelData.AmmunitionUpgradeLevel).ToString();
        string upgradeInfoPrefix = "+";
        this.healthUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelMaxHealthIncreaseValue(heroType, unitLevelData.HealthUpgradeLevel).ToString();
        this.damageUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelDamageIncreaseValue(heroType, unitLevelData.DamageUpgradeLevel).ToString();
        this.ammunitionUpgradeInfoText.text = upgradeInfoPrefix + UnitStatsCalculator.CalculateNextLevelAmmunitionIncreaseValue(heroType, unitLevelData.AmmunitionUpgradeLevel).ToString();

        //TODO handle upgrade cost and buttons
    }

    private void GoToLevelSelect() {
        SceneManager.LoadScene(Constants.Scenes.LevelSelectSceneName);
    }
}
