using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject scanText;
    [SerializeField] private GameObject scanCompleteText;

    void Start()
    {
        scanText.SetActive(true);
        scanCompleteText.SetActive(false);
    }

    public async void OnScanComplete()
    {
        scanText.SetActive(false);
        scanCompleteText.SetActive(true);

        await Task.Delay(1500);
        scanCompleteText.SetActive(false);
    }
}
