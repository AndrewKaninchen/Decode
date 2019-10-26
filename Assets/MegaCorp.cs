using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

public class MegaCorp : MonoBehaviour, IPointerClickHandler
{
    public List<SceneField> levels;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Displaying {this}");
        FindObjectOfType<LevelSelector>().Display(this);
    }
}