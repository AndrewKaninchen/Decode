using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public MegaCorpLevelDisplay levelDisplay;
    public List<MegaCorp> corps;

    public void Display(MegaCorp corp)
    {
        levelDisplay.Display(corp);
    }
}