using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Componenent that resizes the object that is attached to, based on the size of the children that are inside a grid layout group.
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GridLayoutGroup))]
public class GridResizerComponent : MonoBehaviour {

    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;

    void Awake() {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.gridLayoutGroup = this.GetComponent<GridLayoutGroup>();
    }

    public void ResizeComponent() {
        if (this.gridLayoutGroup.constraint != GridLayoutGroup.Constraint.FixedRowCount) {
            throw new System.NotSupportedException("Not supperted grid layout type");
        }

        int numberOfChildren = this.rectTransform.transform.childCount;
        float width = (this.gridLayoutGroup.cellSize.x * numberOfChildren) / this.gridLayoutGroup.constraintCount;
        this.rectTransform.sizeDelta = new Vector2(width, this.rectTransform.sizeDelta.y);
    }
}