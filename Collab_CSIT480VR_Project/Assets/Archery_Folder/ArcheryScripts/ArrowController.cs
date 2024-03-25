using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float arrowMaxSpeed = 10;
    public int arrowCount;
    public TextMeshProUGUI arrowTxt;

    public void Awake()
    {
        arrowCount = 5;
        SetArrowTxt();
    }
    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        midPointVisual.SetActive(false);

        if (arrowCount > 0)
        {
            GameObject arrow = Instantiate(arrowPrefab, null);
            arrowCount--;
            SetArrowTxt();
            arrow.transform.position = arrowSpawnPoint.transform.position;
            arrow.transform.rotation = midPointVisual.transform.rotation;
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.AddForce(midPointVisual.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
        }
    }
    private void SetArrowTxt()
    {
        arrowTxt.text = "Arrows: " + arrowCount.ToString();
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("TableTennisFloor"))
        {
            transform.rotation = spawnPoint.rotation;
            transform.position = spawnPoint.position;
        }
    }

    public void ResetArrow(int count)
    {
        arrowCount = count;
    }
}
