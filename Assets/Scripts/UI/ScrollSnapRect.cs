using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Tooltip("Set starting tab index - starting from 0")]
    public int startingTab = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    [Tooltip("Sprite for unselected tab (optional)")]
    public Sprite unselectedTabNavigationImage;
    [Tooltip("Sprite for selected tab (optional)")]
    public Sprite selectedTabNavigationImage;
    [Tooltip("Container with tab images (optional)")]
    public Transform navigationImageGroup;

    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int fastSwipeThresholdMaxLimit;

    private ScrollRect scrollRectComponent;
    private RectTransform scrollRectTransform;
    private RectTransform container;

    // number of tabs in container
    private int tabCount;
    private int currentTabIndex;

    // whether lerping is in progress and target lerp position
    private bool lerpInProgress;
    private Vector2 lerpPosition;

    // target position of every page
    private IList<Vector2> tabPositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool draggingStarted;
    private float dragTimestamp;
    private Vector2 dragStartPosition;

    // for showing small page icons
    private bool showTabNavigation;
    private int previousTabNavigationIndex;
    // container with Image components - one Image for each page
    private List<Image> tabNavigationImages;

    void Start() {
        scrollRectComponent = GetComponent<ScrollRect>();
        scrollRectTransform = GetComponent<RectTransform>();
        container = scrollRectComponent.content;
        tabCount = (int)Mathf.Ceil(container.rect.width / this.scrollRectTransform.rect.width);
        lerpInProgress = false;

        // init
        SetTabPositions();
        SetTab(startingTab);
        InitTabNavigation();
        SetNavigationTab(startingTab);
    }

    void Update() {
        // if moving to target position
        if (lerpInProgress) {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            container.anchoredPosition = Vector2.Lerp(container.anchoredPosition, lerpPosition, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(container.anchoredPosition - lerpPosition) < 0.25f) {
                // snap to target and stop lerping
                container.anchoredPosition = lerpPosition;
                lerpInProgress = false;
                // clear also any scrollrect move that may interfere with our lerping
                scrollRectComponent.velocity = Vector2.zero;
            }

            // switches selection icon exactly to correct page
            if (showTabNavigation) {
                SetNavigationTab(GetNearestTab());
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetTabPositions() {
        int width = 0;
        //int height = 0;
        int offsetX = 0;
        //int offsetY = 0;
        int containerWidth = 0;
        //int containerHeight = 0;

        // screen width in pixels of scrollrect window
        width = (int)scrollRectTransform.rect.width;
        // center position of all tabs
        offsetX = width / 2;
        // total width
        containerWidth = width * tabCount;
        //containerHeight = (int)this.container.rect.height;
        // limit fast swipe length - beyond this length it is fast swipe no more
        fastSwipeThresholdMaxLimit = width;

        // set width of container
        //Vector2 newSize = new Vector2(containerWidth, containerHeight);
        //container.sizeDelta = newSize;
        //Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        //container.anchoredPosition = newPosition;

        // delete any previous settings
        tabPositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < tabCount; i++) {
            //RectTransform child = container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            //child.anchoredPosition = childPosition;
            tabPositions.Add(-childPosition);
            //tabPositions.Add(new Vector2(-i * width, 0f));
        }
    }

    //------------------------------------------------------------------------
    private void SetTab(int newTabIndex) {
        newTabIndex = Mathf.Clamp(newTabIndex, 0, tabCount - 1);
        container.anchoredPosition = tabPositions[newTabIndex];
        currentTabIndex = newTabIndex;
    }

    //------------------------------------------------------------------------
    private void LerpToTab(int tabIndex) {
        tabIndex = Mathf.Clamp(tabIndex, 0, tabCount - 1);
        lerpPosition = tabPositions[tabIndex];
        lerpInProgress = true;
        currentTabIndex = tabIndex;
    }

    //------------------------------------------------------------------------
    private void InitTabNavigation() {
        // page selection - only if defined sprites for selection icons
        showTabNavigation = unselectedTabNavigationImage != null && selectedTabNavigationImage != null;
        if (showTabNavigation) {
            // also container with selection images must be defined and must have exatly the same amount of items as pages container
            if (navigationImageGroup == null || navigationImageGroup.childCount != tabCount) {
                Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
                showTabNavigation = false;
            }
            else {
                previousTabNavigationIndex = -1;
                tabNavigationImages = new List<Image>();

                // cache all Image components into list
                for (int i = 0; i < navigationImageGroup.childCount; i++) {
                    Image image = navigationImageGroup.GetChild(i).GetComponent<Image>();
                    if (image == null) {
                        Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
                    }
                    tabNavigationImages.Add(image);
                }
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetNavigationTab(int tabIndex) {
        // nothing to change
        if (previousTabNavigationIndex == tabIndex) {
            return;
        }

        // unselect old
        if (previousTabNavigationIndex >= 0) {
            tabNavigationImages[previousTabNavigationIndex].sprite = unselectedTabNavigationImage;
            tabNavigationImages[previousTabNavigationIndex].SetNativeSize();
        }

        // select new
        tabNavigationImages[tabIndex].sprite = selectedTabNavigationImage;
        tabNavigationImages[tabIndex].SetNativeSize();

        previousTabNavigationIndex = tabIndex;
    }

    //------------------------------------------------------------------------
    private void NextTab() {
        LerpToTab(currentTabIndex + 1);
    }

    //------------------------------------------------------------------------
    private void PreviousTab() {
        LerpToTab(currentTabIndex - 1);
    }

    //------------------------------------------------------------------------
    private int GetNearestTab() {
        // based on distance from current position, find nearest tab
        Vector2 currentPosition = container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestTab = currentTabIndex;

        for (int i = 0; i < tabPositions.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - tabPositions[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestTab = i;
            }
        }

        return nearestTab;
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData) {
        // if currently lerping, then stop it as user is draging
        lerpInProgress = false;
        // not dragging yet
        draggingStarted = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData) {
        // how much was container's content dragged
        float difference = dragStartPosition.x - container.anchoredPosition.x;

        // test for fast swipe - swipe that moves only +/-1 item
        if (Time.unscaledTime - dragTimestamp < fastSwipeThresholdTime &&
            Mathf.Abs(difference) > fastSwipeThresholdDistance &&
            Mathf.Abs(difference) < fastSwipeThresholdMaxLimit) {
            if (difference > 0) {
                NextTab();
            }
            else {
                PreviousTab();
            }
        }
        else {
            // if not fast time, look to which page we got to
            LerpToTab(GetNearestTab());
        }

        draggingStarted = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData) {
        if (!draggingStarted) {
            // dragging started
            draggingStarted = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            dragTimestamp = Time.unscaledTime;
            // save current position of cointainer
            dragStartPosition = container.anchoredPosition;
        }
        else {
            if (showTabNavigation) {
                SetNavigationTab(GetNearestTab());
            }
        }
    }
}
