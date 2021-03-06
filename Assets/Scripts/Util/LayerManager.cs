﻿using UnityEngine;

public static class LayerManager {

    public static int GetLayerMask(Constants.Layers.LayerType layerType) {
        if (!Constants.Layers.LayerTypeToNameMap.ContainsKey(layerType)) {
            throw new System.InvalidOperationException("The layer type to name map does not contain entry for that type");
        }

        int layerIndex = LayerMask.NameToLayer(Constants.Layers.LayerTypeToNameMap[layerType]);
        return 1 << layerIndex;
    }

    public static Constants.Layers.LayerType GetLayerType(GameObject gameObject) {
        string searchedLayer = LayerMask.LayerToName(gameObject.layer);
        foreach (var keyValue in Constants.Layers.LayerTypeToNameMap) {
            if (keyValue.Value.Equals(searchedLayer)) {
                return keyValue.Key;
            }
        }

        throw new System.InvalidOperationException("Unknown layer");
    }
}
