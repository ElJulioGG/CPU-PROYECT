using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateNote : MonoBehaviour, IPointerDownHandler
{
    public GameObject note; // Assign your UI prefab in the Inspector
    public RectTransform canvasRectTransform; // Assign your Canvas RectTransform in the Inspector

    public void OnPointerDown(PointerEventData eventData)
    {
        createNote();
    }

    private void createNote()
    {
        // Get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // Convert screen point to world point within the canvas
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, mousePos, Camera.main, out Vector3 worldPoint);

        // Instantiate the note as a child of the canvas in world space
        GameObject instantiatedNote = Instantiate(note, canvasRectTransform);

        // Set the position of the instantiated note at the world point
        instantiatedNote.transform.position = worldPoint;

        // Ensure the note's RectTransform pivot is centered for better alignment
        RectTransform noteRectTransform = instantiatedNote.GetComponent<RectTransform>();
        noteRectTransform.pivot = new Vector2(0.5f, 0.5f);
    }
}
