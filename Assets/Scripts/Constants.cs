using System.Collections.Generic;

public static class Constants {
    public static class Input {
        public const string JumpSpeedInputName = "Jump";
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

    public static class Animation {
        public static class MainCharacter {
            public const string MoveSpeedParameterName = "MoveSpeed";
        }
    }
}

