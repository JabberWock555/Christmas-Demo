using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class SantaMover : MonoBehaviour
{
    [SerializeField] private CinemachineSplineCart dollyCart;
    [SerializeField] private Animator santaAnim;
    [SerializeField] private MeshRenderer logo;
    // [SerializeField] private MeshRenderer logo_1;

    [SerializeField] private float dollyRunSpeed = 0.5f;
    [SerializeField] private float dollyWalkSpeed = 0.5f;
    [SerializeField] private float animSpeed = 0.5f;

    private Material logoMat, logoMat_1;
    GameObject target;
    bool CanMove = true, canRotate;



    private void Start()
    {
        target = new();
        santaAnim.SetFloat("Moveindex", 0);
        logoMat = logo.material;
        //logoMat_1 = logo_1.material;
        logoMat.SetVector("_DissolveOffest", new Vector4(0, .5f, 0));
        //logoMat_1.SetVector("_DissolveOffest", new Vector4(0, .5f, 0));

    }

    private void Update()
    {
        Move();

        RotateLogo();
    }

    private void Move()
    {
        if (CanMove)
        {
            float posUnits = (dollyCart.SplinePosition);

            if (posUnits < 1)
            {

                if (posUnits <= .95f)
                    posUnits += Time.deltaTime * dollyRunSpeed;
                else if (posUnits > 0.95f && posUnits <= 1)
                {
                    posUnits += Time.deltaTime * dollyWalkSpeed;
                }

                if (posUnits >= .9f)
                {
                    float animValue = santaAnim.GetFloat("Moveindex");

                    animValue += Time.deltaTime * animSpeed;

                    if (animValue < 1)
                    {
                        santaAnim.SetFloat("Moveindex", animValue);
                    }
                }

            }
            else
            {
                PlaySurprise();
                CanMove = false;
            }

            dollyCart.SplinePosition = posUnits;
        }

    }

    private void PlaySurprise()
    {
        santaAnim.Play("Surprise");
    }

    private void RotateLogo()
    {
        if (canRotate)
        {
            target.transform.position = new Vector3(Camera.main.transform.position.x, logo.transform.position.y, Camera.main.transform.position.z);
            logo.transform.LookAt(target.transform);
        }
    }

    public async void ShowLogo()
    {
        float value = 0.5f;
        canRotate = true;
        if (logo.gameObject.activeInHierarchy)
        {
            while (value > -.1f)
            {
                value -= Time.deltaTime * 0.05f;
                await Task.Yield();
                logoMat.SetVector("_DissolveOffest", new Vector4(0, value, 0));
                //logoMat_1.SetVector("_DissolveOffest", new Vector4(0, value, 0));
            }
        }
    }
}
