using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Component that splits the content inside a container in virtual scrollable tabs.
/// The number of tabs is dependant of the size of the container and display size.
/// </summary>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollableTabsAddon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Tooltip("Set starting tab index - starting from 0")]
    public int startingTab = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 50;
    [Tooltip("How fast will tab lerp to target position")]
    public float decelerationRate = 10f;
    [Tooltip("Sprite for unselected tab (optional)")]
    public Sprite unselectedTabNavigationSprite;
    [Tooltip("Sprite for selected tab (optional)")]
    public Sprite selectedTabNavigationSprite;
    [Tooltip("Container for tab images (optional)")]
    public Transform navigationImageGroup;
    [Tooltip("Image prefab for tab navigation (optional)")]
    public Image tabNavigationImagePrefab;
    [Tooltip("Auto initialize this component on start")]
    public bool autoInitializeOnStart = true;

    private ScrollRect scrollRectComponent;
    private RectTransform container;
    private int tabCount;
    private float tabWidth;
    private IList<Vector2> tabOffsets;
    private int currentTabIndex;
    private bool lerpInProgress;
    private Vector2 lerpPosition;
    private bool draggingStarted;
    private float dragStartTime;
    private Vector2 dragStartPosition;
    private bool showTabNavigation;
    private int previousTabNavigationIndex;
    private List<Image> tabNavigationImages;
    private int fastSwipeThresholdMaxLimit;
    private bool isInitialized = false;

    public void Start() {
        if (this.autoInitializeOnStart) {
            this.Initialize();
        }
    }

    public void Initialize() {
        if (this.isInitialized) {
            throw new InvalidOperationException("Component already initialized");
        }

        this.scrollRectComponent = GetComponent<ScrollRect>();
        this.tabWidth = GetComponent<RectTransform>().rect.width;
        this.container = scrollRectComponent.content;
        this.container.pivot = new Vector2(0f, this.container.pivot.y);
        this.container.localPosition = new Vector2(0f, this.container.localPosition.y);
        this.container.anchorMin = new Vector2(0f, this.container.anchorMin.y);
        this.container.anchorMax = new Vector2(0f, this.container.anchorMax.y);
        this.tabCount = (int)Mathf.Ceil(container.rect.width / this.tabWidth);
        this.fastSwipeThresholdMaxLimit = (int)this.tabWidth;
        this.lerpInProgress = false;

        this.SetTabOffsetPositions();
        this.SetTab(startingTab);
        this.InitTabNavigation();
        this.SetNavigationTab(startingTab);
        this.isInitialized = true;
    }

    public void Update() {
        if (!this.isInitialized) {
            return;
        }

        if (!this.lerpInProgress) {
            return;
        }

        float decelarationValue = Mathf.Min(this.decelerationRate * Time.deltaTime, 1f);
        this.container.anchoredPosition = Vector2.Lerp(this.container.anchoredPosition, this.lerpPosition, decelarationValue);
        if (Vector2.SqrMagnitude(this.container.anchoredPosition - this.lerpPosition) < 0.25f) {
            container.anchoredPosition = lerpPosition;
            lerpInProgress = false;
            scrollRectComponent.velocity = Vector2.zero;
        }

        if (this.showTabNavigation) {
            this.SetNavigationTab(GetNearestTab());
        }
    }

    private void SetTabOffsetPositions() {
        this.tabOffsets = new List<Vector2>();
        for (int i = 0; i < this.tabCount; i++) {
            float tabOffsetPositionXFromPivot = -i * tabWidth;
            this.tabOffsets.Add(new Vector2(tabOffsetPositionXFromPivot, 0f));
        }
    }

    public void SetTab(int newTabIndex) {
        newTabIndex = Mathf.Clamp(newTabIndex, 0, this.tabCount - 1);
        this.container.anchoredPosition = tabOffsets[newTabIndex];
        this.currentTabIndex = newTabIndex;
        if (this.showTabNavigation) {
            this.SetNavigationTab(GetNearestTab());
        }
    }

    private void LerpToTab(int tabIndex) {
        tabIndex = Mathf.Clamp(tabIndex, 0, this.tabCount - 1);
        this.lerpPosition = tabOffsets[tabIndex];
        this.lerpInProgress = true;
        this.currentTabIndex = tabIndex;
    }

    private void InitTabNavigation() {
        this.showTabNavigation = this.unselectedTabNavigationSprite != null && this.selectedTabNavigationSprite != null && this.navigationImageGroup != null && this.tabNavigationImagePrefab;
        if (!this.showTabNavigation) {
            return;
        }

        this.previousTabNavigationIndex = -1;
        this.tabNavigationImages = new List<Image>();
        for (int i = 0; i < this.tabCount; i++) {
            Image navigationImage = Instantiate(this.tabNavigationImagePrefab);
            navigationImage.sprite = this.unselectedTabNavigationSprite;
            navigationImage.gameObject.transform.SetParent(this.navigationImageGroup);
            navigationImage.gameObject.transform.localScale = Vector3.one;
            this.tabNavigationImages.Add(navigationImage);
        }
    }

    private void SetNavigationTab(int tabIndex) {
        if (this.previousTabNavigationIndex == tabIndex) {
            return;
        }

        if (this.previousTabNavigationIndex >= 0) {
            tabNavigationImages[this.previousTabNavigationIndex].sprite = this.unselectedTabNavigationSprite;
        }

        this.tabNavigationImages[tabIndex].sprite = this.selectedTabNavigationSprite;
        this.previousTabNavigationIndex = tabIndex;
    }

    private void LerpToNextTab() {
        LerpToTab(this.currentTabIndex + 1);
    }

    private void LerpToPreviousTab() {
        LerpToTab(this.currentTabIndex - 1);
    }

    private int GetNearestTab() {
        Vector2 currentPosition = this.container.anchoredPosition;
        float distance = float.MaxValue;
        int nearestTab = this.currentTabIndex;
        for (int i = 0; i < this.tabOffsets.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - this.tabOffsets[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestTab = i;
            }
        }

        return nearestTab;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!this.isInitialized) {
            return;
        }

        this.lerpInProgress = false;
        this.draggingStarted = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!this.isInitialized) {
            return;
        }

        this.draggingStarted = false;
        float dragDistance = this.dragStartPosition.x - container.anchoredPosition.x;
        bool isFastAndShortSwipe = Time.unscaledTime - this.dragStartTime < this.fastSwipeThresholdTime && Mathf.Abs(dragDistance) > this.fastSwipeThresholdDistance && Mathf.Abs(dragDistance) < this.fastSwipeThresholdMaxLimit;
        if (isFastAndShortSwipe) {
            if (dragDistance > 0) {
                this.LerpToNextTab();
            }
            else {
                this.LerpToPreviousTab();
            }

            return;
        }

        this.LerpToTab(GetNearestTab());
    }

    public void OnDrag(PointerEventData eventData) {
        if (!this.isInitialized) {
            return;
        }

        if (!this.draggingStarted) {
            this.draggingStarted = true;
            this.dragStartTime = Time.unscaledTime;
            this.dragStartPosition = container.anchoredPosition;
            return;
        }

        if (this.showTabNavigation) {
            this.SetNavigationTab(this.GetNearestTab());
        }
    }
}