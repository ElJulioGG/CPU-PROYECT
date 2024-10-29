using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonMenu : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;  // Store the button's original position
    private float soundPitch;
    private int rowIndex;
    private bool isClicked;
    [SerializeField] public int menuIndex = 0;


    public bool isDefault;
    public bool isDragging;

    private GameObject grid;
    [SerializeField] private List<AudioClip> yAxisSounds; // Assign different sounds in the Inspector
    public List<RectTransform> gridCells; // Assign this in the Inspector

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Awake()
    {
        grid = GameObject.Find("Grid");
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Populate gridCells list with the child objects of the grid
        gridCells = new List<RectTransform>();

        
        foreach (Transform child in grid.transform)
        {
            RectTransform cell = child.GetComponent<RectTransform>();
            if (cell != null)
            {
                gridCells.Add(cell);
            }
        }
        findRowIndex();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteLine"))
        {
            print("Line Colided");
            PlayNote(rowIndex);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteDeleter")&&  isDefault)
        {
            Destroy(gameObject);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.instance.lastSelectedNote == menuIndex)
        {
            GameManager.instance.lastSelectedNote = 99;
            
        }
        else
        {
            GameManager.instance.lastSelectedNote = menuIndex;
        }
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        isDragging = true;
        originalPosition = rectTransform.position;  // Save the starting position
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the image along with the drag
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        // Find the closest grid cell
        RectTransform closestCell = GetClosestCell();

        if (closestCell != null)
        {
            // Check if there's another ButtonMenu occupying this cell
            ButtonMenu otherButton = GetButtonAtCell(closestCell);

            if (otherButton != null && otherButton.rectTransform != rectTransform)
            {
               
                if (!isDefault)
                {
                    // Swap places: Move the other button to the original position of this one
                    otherButton.rectTransform.position = originalPosition;
                    rectTransform.position = closestCell.position;
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
            else
            {
                // If no other button found, snap to the closest cell
                rectTransform.position = closestCell.position;
            }
        }
        else
        {
            // If no cell found, snap back to the original position
            rectTransform.position = originalPosition;
        }

        // Adjust pitch based on X axis
        //AdjustPitchBasedOnXPosition();
        // Play sound based on Y axis
        PlaySoundBasedOnYPosition();
    }

    private RectTransform GetClosestCell()
    {
        RectTransform closestCell = null;
        float closestDistance = float.MaxValue;

        foreach (RectTransform cell in gridCells)
        {
            float distance = Vector3.Distance(rectTransform.position, cell.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCell = cell;
            }
        }

        return closestCell;
    }

    private ButtonMenu GetButtonAtCell(RectTransform cell)
    {
        foreach (ButtonMenu button in FindObjectsOfType<ButtonMenu>())
        {
            if (button.rectTransform != rectTransform && Vector3.Distance(button.rectTransform.position, cell.position) < 0.1f)
            {
                return button;
            }
        }
        return null;
    }


    private void PlaySoundBasedOnYPosition()
    {
        findRowIndex();
        PlayNote(rowIndex);
    }
    
    private void PlayNote(int index)
    {
        switch (index)
        {
            case 7:
                AudioManager.instance.PlayUI("noteC");
                break;
            case 6:
                AudioManager.instance.PlayUI("noteD");
                break;
            case 5:
                AudioManager.instance.PlayUI("noteE");
                break;
            case 4:
                AudioManager.instance.PlayUI("noteF");
                break;
            case 3:
                AudioManager.instance.PlayUI("noteG");
                break;
            case 2:
                AudioManager.instance.PlayUI("noteA");
                break;
            case 1:
                AudioManager.instance.PlayUI("noteB");
                break;
            case 0:
                //No sound
                break;
        }
    }

    private void findRowIndex()
    {
        int totalRows = 8;
        float gridHeight = grid.GetComponent<RectTransform>().rect.height;
        float cellHeight = gridHeight / totalRows;

        // Convert the button's world position to the grid's local space
        Vector3 localPositionInGrid = grid.GetComponent<RectTransform>().InverseTransformPoint(rectTransform.position);

        // Calculate the Y position within the grid's local space
        float localYPosition = localPositionInGrid.y;

        // Calculate row index, with half the grid height to start indexing from the bottom
        rowIndex = Mathf.Clamp(Mathf.FloorToInt((localYPosition + (gridHeight / 2)) / cellHeight), 0, totalRows - 1);

        //// Debugging output to help verify each step
        //Debug.Log("Button Local Y Position in Grid Space: " + localYPosition);
        //Debug.Log("Grid Height: " + gridHeight + ", Cell Height: " + cellHeight);
        //Debug.Log("Calculated Row Index: " + rowIndex);
    }





    //private void AdjustPitchBasedOnXPosition()
    //{
    //    float xPos = rectTransform.anchoredPosition.x;

    //    // Define the range for pitch scaling
    //    float minPitch = 1f;
    //    float maxPitch = 2f;

    //    // Map X position to the pitch range
    //    // Assuming the width of the grid is the maximum range for the X position
    //    float gridWidth = grid.GetComponent<RectTransform>().rect.width;

    //    // Normalize the X position based on grid width
    //    float normalizedXPos = Mathf.InverseLerp(-gridWidth / 2, gridWidth / 2, xPos);

    //    // Calculate the pitch based on the normalized X position
    //    soundPitch = Mathf.Lerp(minPitch, maxPitch, normalizedXPos);
    //}

}
