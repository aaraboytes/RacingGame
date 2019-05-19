using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIChanger : MonoBehaviour
{
    [SerializeField]Image[] background;
    [SerializeField]Sprite[] bgImages;
    [SerializeField]float timeForBgChange;
    [SerializeField] float accChange;
    int currentSelect = 0;
    float timer = 0;
    bool changing = false;
    int imageIndex = 0;
    private void Start()
    {
        background[0].GetComponent<Image>().sprite = bgImages[imageIndex];
        imageIndex++;
        background[1].GetComponent<Image>().sprite = bgImages[imageIndex];
    }
    void Update()
    {
        if (!changing)
        {
            timer += Time.deltaTime;
            if (timer > timeForBgChange)
            {
                changing = true;
                timer = 0;
                StartCoroutine(ChangeImage());
            }
        }
    }
    IEnumerator ChangeImage()
    {
        int auxSelect = currentSelect == 0 ? 1 : 0;
        imageIndex++;
        if (imageIndex > bgImages.Length - 1)
        {
            imageIndex = 0;
        }
        background[auxSelect].GetComponent<Image>().sprite = bgImages[imageIndex];
        while(background[auxSelect].GetComponent<Image>().color.a < 1.0f)
        {
            Color auxColor = background[auxSelect].GetComponent<Image>().color;
            Color currentColor = background[currentSelect].GetComponent<Image>().color;
            auxColor.a += Time.deltaTime * accChange;
            currentColor.a -= Time.deltaTime * accChange;
            background[auxSelect].GetComponent<Image>().color = auxColor;
            background[currentSelect].GetComponent<Image>().color = currentColor;
            yield return null;
        }
        currentSelect = auxSelect;
        changing = false;
    }
}
