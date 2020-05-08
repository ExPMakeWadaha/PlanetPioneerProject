using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip buttonSound;
    public AudioClip eventSound;

    AudioSource myAudio; //static으로 공유할 것이기 때문에 AudioSource.playoneshot로 접근할 수 없어 선언

    public static SoundManager instance; //사운드매니저 정적 객체

	// Use this for initialization
	void Awake () {
        if (SoundManager.instance == null) //하나만 생성해서 공유
            SoundManager.instance = this;
	}

    void Start()
    {
      myAudio = GetComponent<AudioSource>();

    }
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaySound(string name)
    {
        if(name == "buttonSound")
            myAudio.PlayOneShot(buttonSound);

        if(name == "eventSound")
            myAudio.PlayOneShot(eventSound);
    }
/* 참고사항
    스크립트 작성 시 삽입 코드: instance.PlaySound("eventSound");
*/
}
