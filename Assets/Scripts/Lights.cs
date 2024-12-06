using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<GameObject> lights;
  

    public void HitLights()
    {
        StartCoroutine(SwitchOnLights());
        Debug.Log("On");

    }

    IEnumerator SwitchOnLights()
    {
        foreach (GameObject light in lights) 
        {
            light.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
    }
}
