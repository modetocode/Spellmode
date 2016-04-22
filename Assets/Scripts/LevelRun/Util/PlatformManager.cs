using System;
using UnityEngine;
/// <summary>
/// Responsible for handling logic about the platforms
/// </summary>
public static class PlatformManager {
    public class PlatformResult {
        public bool Successful { get; private set; }
        public Constants.Platforms.PlatformType PlatformType { get; private set; }

        public PlatformResult(bool successful) {
            this.Successful = successful;
        }

        public PlatformResult(bool successful, Constants.Platforms.PlatformType platformType) : this(successful) {
            this.PlatformType = platformType;
        }
    }

    public static float GetYCoordinateForPlatform(Constants.Platforms.PlatformType platformType) {
        if (!Constants.Platforms.PlatformTypeToCoordinateMap.ContainsKey(platformType)) {
            throw new System.InvalidOperationException("The platform type to coordinate map does not contain entry for that type");
        }

        return Constants.Platforms.PlatformTypeToCoordinateMap[platformType];
    }

    /// <summary>
    /// Checks if given position is positioned on some platform. Returns boolean result of the check and if the position is on a platform then returns also that platform type.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static PlatformResult IsPositionOnPlatform(Vector3 position) {
        foreach (var platformToCoordinate in Constants.Platforms.PlatformTypeToCoordinateMap) {
            float platformPosition = platformToCoordinate.Value;
            if (IsPositionOnPlatform(position.y, platformPosition)) {
                return new PlatformResult(successful: true, platformType: platformToCoordinate.Key);
            }
        }

        return new PlatformResult(successful: false);
    }

    private static bool IsPositionOnPlatform(float positionToCheck, float platformYPosition) {
        return Mathf.Abs(positionToCheck - platformYPosition) < Constants.Math.FloatPositiveEpsilon;
    }

    public static PlatformResult HasUpperPlatformThan(Constants.Platforms.PlatformType platformType) {
        switch (platformType) {
            case Constants.Platforms.PlatformType.Top:
                return new PlatformResult(successful: false);
            case Constants.Platforms.PlatformType.Bottom:
                return new PlatformResult(successful: true, platformType: Constants.Platforms.PlatformType.Top);
            default:
                throw new InvalidOperationException("Platform type not supported");
        }
    }

    public static PlatformResult HasLowerPlatformThan(Constants.Platforms.PlatformType platformType) {
        switch (platformType) {
            case Constants.Platforms.PlatformType.Bottom:
                return new PlatformResult(successful: false);
            case Constants.Platforms.PlatformType.Top:
                return new PlatformResult(successful: true, platformType: Constants.Platforms.PlatformType.Bottom);
            default:
                throw new InvalidOperationException("Platform type not supported");
        }
    }
}
