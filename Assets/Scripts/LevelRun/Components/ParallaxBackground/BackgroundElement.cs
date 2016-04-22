using System;
using UnityEngine;

/// <summary>
/// An entity that represents one part of the scrolling background.
/// </summary>
[Serializable]
public class BackgroundElement {
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private float speedReductionPercentage;

    [SerializeField]
    private bool isMovingForward;


    public GameObject GameObject {
        get { return this.gameObject; }
    }

    public float SpeedReductionPercentage {
        get { return this.speedReductionPercentage; }
    }

    public bool IsMovingForward {
        get { return this.isMovingForward; }
    }
}

