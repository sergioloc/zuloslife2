using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    
    [Tooltip("Button to go to the previous page (optional)")]
    public GameObject prevButton;
    [Tooltip("Button to go to the next page (optional)")]
    public GameObject nextButton;
    [Tooltip("Sprite for unselected page (optional)")]
    public Sprite unselectedPage;
    [Tooltip("Sprite for selected page (optional)")]
    public Sprite selectedPage;
    [Tooltip("Container with page images (optional)")]
    public Transform pageSelectionIcons;

    private ScrollRect scrollRectComponent;
    private RectTransform scrollRectRect;
    private RectTransform container;
    
    // number of pages in container
    private int pageCount;
    private int currentPage;

    // whether lerping is in progress and target lerp position
    private bool lerp;
    private Vector2 lerpTo;


    // in draggging, when dragging started and where it started
    private bool dragging;
    private float timeStamp;
    private Vector2 startPosition;

    private int previousPage;
    private List<Image> pageSelectionImages;
    private List<Vector2> pagePositions = new List<Vector2>();
    public SpriteRenderer background, skyline; 
    public Color32[] backgroundColor;
    public Color32[] skylineColor;


    //------------------------------------------------------------------------
    void Start() {
        scrollRectComponent = GetComponent<ScrollRect>();
        scrollRectRect = GetComponent<RectTransform>();
        container = scrollRectComponent.content;
        pageCount = container.childCount;
        lerp = false;
        background.color = backgroundColor[0];
        skyline.color = skylineColor[0];

        SetPagePositions();
        SetPage(startingPage);
        InitPageSelection();
        SelectPage(startingPage);

        if (nextButton)
            nextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

        if (prevButton)
            prevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });

        prevButton.SetActive(false);
	}

    //------------------------------------------------------------------------
    void Update() {
        if (lerp) {
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            container.anchoredPosition = Vector2.Lerp(container.anchoredPosition, lerpTo, decelerate); 
            
            if (Vector2.SqrMagnitude(container.anchoredPosition - lerpTo) < 0.25f) {

                container.anchoredPosition = lerpTo;
                lerp = false;
                scrollRectComponent.velocity = Vector2.zero;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            NextScreen();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            PreviousScreen();
        }
    }

    //------------------------------------------------------------------------
    private void SetPagePositions() {
        int width = 0;
        int offsetX = 0;
        int containerWidth = 0;

        width = (int)scrollRectRect.rect.width; // ancho del scrollrect
        offsetX = width / 2; // posicion central de las paginas
        containerWidth = width * pageCount; // ancho total

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, 0);
        container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, 0);
        container.anchoredPosition = newPosition;

        pagePositions.Clear();

        for (int i = 0; i < pageCount; i++) {
            RectTransform child = container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            child.anchoredPosition = childPosition;
            pagePositions.Add(-childPosition);
        }
    }

    //------------------------------------------------------------------------
    private void SetPage(int position) {
        position = Mathf.Clamp(position, 0, pageCount - 1);
        container.anchoredPosition = pagePositions[position];
        currentPage = position;
    }

    //------------------------------------------------------------------------
    private void GoToPage(int position) {
        position = Mathf.Clamp(position, 0, pageCount - 1);
        lerpTo = pagePositions[position];
        lerp = true;
        currentPage = position;
        SelectPage(position);
        background.color = backgroundColor[position];
        skyline.color = skylineColor[position];

        if (position == 0)
            prevButton.SetActive(false);
        else if (position == pagePositions.Count-1)
            nextButton.SetActive(false);

    }

    private void NextScreen() {
        nextButton.GetComponent<Animator>().SetTrigger("Pressed");
        GoToPage(currentPage + 1);
        prevButton.SetActive(true);
    }

    private void PreviousScreen() {
        prevButton.GetComponent<Animator>().SetTrigger("Pressed");
        GoToPage(currentPage - 1);
        nextButton.SetActive(true);
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData) {
        // if currently lerping, then stop it as user is draging
        lerp = false;
        // not dragging yet
        dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData) {
        // how much was container's content dragged
        float difference = startPosition.x - container.anchoredPosition.x; 
        if (difference > 0) {
                NextScreen();
            } else {
                PreviousScreen();
            }
        dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData) {
        if (!dragging) {
            // dragging started
            dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            timeStamp = Time.unscaledTime;
            // save current position of cointainer
            startPosition = container.anchoredPosition;
        } else {
            /*
            if (_showPageSelection) {
                SetPageSelection(GetNearestPage());
            }
            */
        }
    }


    
    private void InitPageSelection() {
        previousPage--;
        pageSelectionImages = new List<Image>();

        for (int i = 0; i < pageSelectionIcons.childCount; i++) {
            Image image = pageSelectionIcons.GetChild(i).GetComponent<Image>();
            pageSelectionImages.Add(image);
        }
    }

    private void SelectPage(int position) {
        if (previousPage != position) {

            if (previousPage >= 0) {
                pageSelectionImages[previousPage].sprite = unselectedPage;
            }

            pageSelectionImages[position].sprite = selectedPage;
            previousPage = position;
        }
    }
}
