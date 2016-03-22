using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    public static class Input {
        public const string JumpSpeedInputName = "Jump";
        public const string JumpUpInputName = "JumpUp";
        public const string JumpDownInputName = "JumpDown";
        public const string PauseInputName = "Pause";
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

    public static class Units {
        public const float DistanceToTriggerUnitSpawnInMetters = 100f;
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
    }
}

