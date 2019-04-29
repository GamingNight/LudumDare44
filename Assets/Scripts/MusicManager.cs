using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	private static MusicManager instance;

	public GameObject coolMusic;
	public GameObject hardMusic;
	public GameObject switchMusic;
	private AudioSource audioSourceCool;
	private AudioSource audioSourceHard;
	private AudioSource audioSourceSwitch;
	private float killTime;
	private bool coolMode = true;
	static private int hardTimer = 10; // 10sec

    public static MusicManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        audioSourceCool = coolMusic.GetComponent<AudioSource>();
        audioSourceHard = hardMusic.GetComponent<AudioSource>();
        audioSourceSwitch = switchMusic.GetComponent<AudioSource>();
        coolMode = true;
        audioSourceCool.Play();
        killTime = 0;
    }

	public void NPCiskilled()
	{
		killTime = GameManager.Instance().getPlayTime();
	}

    void Update()
    {
    	bool newCoolMode = false;
        if ((killTime == 0) || (GameManager.Instance().getPlayTime() - killTime > hardTimer)) {
            killTime = 0;
            newCoolMode = true;
        }

    	if (newCoolMode == coolMode)
    		return;

    	audioSourceSwitch.Play();
    	coolMode = newCoolMode;
        if (coolMode)
        {
            audioSourceCool.Play();
            audioSourceHard.Pause();
        }
		else
        {
            audioSourceHard.Play();
            audioSourceCool.Pause();
        }
    }
}
