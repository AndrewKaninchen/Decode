using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MegaCorpLevelDisplay : MonoBehaviour
{
    public GameObject buttonTemplate;

    private void Clear()
    {
        for (var i = 1; i < transform.GetChild(1).childCount; i++)
        {
            Destroy(transform.GetChild(1).GetChild(i).gameObject);
        }
    }

    public void Deactivate()
    {
        Clear();
        gameObject.SetActive(false);
    }

    public void Display(MegaCorp corp)
    {
        gameObject.SetActive(true);
        foreach (var scene in corp.levels)
        {
            var button = Instantiate(buttonTemplate, transform.GetChild(1)).GetComponent<Button>();
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(scene.SceneName);
            });
        }
    }
    
}
