using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonVR4 : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            StartCoroutine(PlaySoundAndLoadScene());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    IEnumerator PlaySoundAndLoadScene()
    {
        // Start the fade out effect
        yield return StartCoroutine(FadeOut());

        sound.Play(); // Play the sound after the fade out effect

        // Load the scene after fading out
        SceneManager.LoadScene(4);
    }

    IEnumerator FadeOut()
    {
        float duration = 1.0f; // Duration of the fade effect
        Image fadePanel = FindObjectOfType<Image>(); // Find any UI Image in the scene
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true); // Show the fade panel
            float elapsedTime = 0.0f;
            Color originalColor = fadePanel.color;
            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);
                fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f); // Ensure the panel is fully faded out
        }
    }

    public void GoToScene(int sceneIndex)
    {
        if (!isPressed)
        {
            isPressed = true;
            StartCoroutine(PlaySoundAndLoadScene());
        }
    }
}
