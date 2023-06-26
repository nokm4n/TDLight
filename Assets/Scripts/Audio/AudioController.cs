using UnityEngine.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour
{

	[SerializeField, NotNull] private AudioSource win;
	[SerializeField, NotNull] private AudioSource hit;
	[SerializeField, NotNull] private AudioSource laser;

	public static AudioController instance;

	//public AudioMixerGroup mixerGroup;


	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	/*public void PlayWin()
    {
		if (win != null)
			win.Play();
    }


	public void PlayHit()
    {
		if(hit != null)
			hit.Play();
	}*/

	public void PlaySound(string song)
    {
		switch (song)
        {
			case "win":
				if (win != null)
					win.Play();
				break;
			case "hit":
				if (hit != null)
					hit.Play();
				break;
			case "laser":
				if (laser != null)
					laser.Play();
				Debug.Log("laser");
				break;
		}
    }

	public void StopSound(string song)
	{
		switch (song)
		{
			case "win":
				if (win != null)
					win.Pause();
				break;
			case "hit":
				if (hit != null)
					hit.Pause();
				break;
			case "laser":
				if (laser != null)
					laser.Pause();
				Debug.Log("laser");
				break;
		}
	}
}
