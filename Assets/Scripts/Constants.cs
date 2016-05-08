using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    public static class Input {
        public const string JumpUpInputName = "JumpUp";
        public const string JumpDownInputName = "JumpDown";
        public const string PauseInputName = "Pause";
        public const string ShootInputName = "Fire1";
        public const float MinimalScreenPercentageConsideredToBeSwipe = 0.15f;
    }

    public static class Layers {
        public enum LayerType {
            Ground,
            MainCharacter
        }

        public const string GroundLayerName = "Ground";
        public const string MainCharacterLayerName = "MainCharacter";

        public static IDictionary<LayerType, string> LayerTypeToNameMap = new Dictionary<LayerType, string> {
            {LayerType.Ground, GroundLayerName },
            {LayerType.MainCharacter, MainCharacterLayerName },
        };
    }

    public static class Animations {
        public static class MainCharacter {
            public const string MoveSpeedParameterName = "MoveSpeed";
            public const string FireTriggerParameterName = "Fire";
            public const string DeathTriggerParameterName = "Death";
            public const string HitTriggerParameterName = "Hit";
        }
    }

    public static class Platforms {
        public enum PlatformType {
            Bottom,
            Top
        }

        public static IDictionary<PlatformType, float> PlatformTypeToCoordinateMap = new Dictionary<PlatformType, float> {
            {PlatformType.Bottom, 0f},
            {PlatformType.Top, 3.5f}
        };
    }

    public static class Camera {
        public static class LevelRun {
            /// <summary>
            /// The initial focus object position in the camera where (0,0) is bottom left and (1,1) is top right points of the screen
            /// </summary>
            public static Vector2 InitialFocusObjectPosition = new Vector2(0.1f, 0.18f);

            /// <summary>
            /// The worldspace coordinate Z of the camera representing the depth of the camera.
            /// </summary>
            public static float CameraDepth = -10f;
        }
    }

    public static class Math {
        public const float FloatPositiveEpsilon = 0.00001f;
    }

    public static class Scenes {
        public const string LevelRunSceneName = "LevelRun";
        public const string LevelSelectSceneName = "LevelSelect";
        public const string ShopSceneName = "Shop";
        public const string LoginSceneName = "Login";

        public static class LevelRun {
            public const float BackgroundSpeedFactor = 0.1f;
            public const float DistanceToTriggerObjectSpawnInMetters = 25f;
            public const string LevelProgressStringTemplate = "{0:0.} / {1:0.}";
        }

        public static class LevelSelect {
            public const int NumberOdDisplayedLevelsPerTab = 8;
        }
    }

    public static class GameData {
        public const string GameDataFileName = "gameData.gd";
    }

    public static class GameMechanics {
        public const string ResourcesPath = "Assets/Resources/";
        public const string GameMechanicsRelativePath = "GameMechanics/";
        public const string AssetsExtension = ".asset";
        public static IDictionary<UnitType, string> UnitTypeToProgressionAssetNameMap = new Dictionary<UnitType, string> {
            {UnitType.HeroUnit, "HeroUnitProgressionData"},
            {UnitType.DefendingArcherUnit, "DefendingArcherUnitProgressionData"},
            {UnitType.DefendingMeleeUnit, "DefendingMeleeUnitProgressionData"},
        };

        public const string GameMechanicsMenuName = "Game Mechanics";
        public const string UnitsMenuName = "Units";
        public const string HeroUnitMenuName = "Hero Unit";
        public const string DefendingArcherUnitMenuName = "Defending Archer Unit";
        public const string DefendingMeleeUnitMenuName = "Defending Melee Unit";
        public const string LootTableMenuName = "Loot Table";
        public const string LootTableAssetName = "LootTable";
        public const string GameConstantsMenuName = "Game Constants";
        public const string GameConstantsAssetName = "GameConstants";
    }

    public static class Strings {
        public const string WelcomeInfoTutorialMessageTemplate = "Welcome {0}. Open the first level of the game.";
        public const string HealthInfoTutorialMessage = "This is your health. If your health drops below 0 you will fail the level and must start again.";
        public const string AmmunitionInfoTutorialMessage = "Your current number of arrows is displayed here. If you run out of arrows you cannot shoot.";
        public const string ProgressInfoTutorialMessage = "This is your progress in this level. The bar is filled automatically as you walk. When the bar is fully filled you will pass the level.";
        public const string TotalLevelRunGoldAmountInfoTutorialMessage = "The gold collected during this level is shown here. You can buy powerful upgrades in the shop with it.";
        public const string MobileCommandsTutorialMessage = "Tap to shoot. Swipe up/down to move between platforms.";
        public const string WindowsCommandsTutorialMessage = "Press space to shoot. Press w/s buttons to move up/down between platforms.";
        public const string TotalGoldTutorialMessage = "This is your total earned gold.";
        public const string ShopIntroTutorialMessage = "You have enough gold to improve your hero. Visit the shop.";
    }
}

