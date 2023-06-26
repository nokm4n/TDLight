using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSetting : MonoBehaviour
{
    [SerializeField, NotNull] List<Texture> settingsTexture;
    [SerializeField, NotNull] List<GameObject> surroundingsGameObject;

    [SerializeField, NotNull] Material material;

    public void ChangeSettings(int index)
    {
        if(index >= surroundingsGameObject.Count)
            index = Random.Range(0, surroundingsGameObject.Count);


        material.SetTexture("_BaseMap", settingsTexture[index]);

        for (int i = 0; i < surroundingsGameObject.Count; i++)
        {
            if(i == index)
            {
                surroundingsGameObject[i].SetActive(true);
            }
            else
            {
                surroundingsGameObject[i].SetActive(false);
            }
        }
    }

    public Texture GetTexture()
    {
        return material.mainTexture;
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            WaveSpawner.instance.ChangeSetting(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            WaveSpawner.instance.ChangeSetting(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WaveSpawner.instance.ChangeSetting(2);
        }*/
    }
}
