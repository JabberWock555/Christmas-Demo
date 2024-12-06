using UnityEngine;
using DG.Tweening;

public class TreeScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void TreeScaling()
    {
        transform.DOScale(3, 1); // (Sacle, time)
    }
}
