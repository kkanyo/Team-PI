using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour {
	public float animTime = 2f;

	private Image fadeImage;

	private float start = 1f;
	private float end = 0f;
	private float time = 0f;

	private bool isPlaying = false;
	public GameObject FadeIn;

	void Start()
	{
		if (isPlaying == false)
			StartCoroutine("PlayFadeIn");
	}
	
	void Awake()
	{
		fadeImage = GetComponent<Image>();
	}

	IEnumerator PlayFadeIn()
	{
		isPlaying = true;

		Color color = fadeImage.color;
		time = 0f;
		color.a = Mathf.Lerp(start, end, time);

		while (color.a > 0f) {
			time += Time.deltaTime / animTime;

			color.a = Mathf.Lerp(start, end, time);
			fadeImage.color = color;

			yield return null;
		}

		isPlaying = false;
		FadeIn.SetActive(false);
	}

}
