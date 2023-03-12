using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour
{
    [System.Serializable]
    public struct BGMType
    {
        public string name;
        public AudioClip audio;
    }
    public BGMType bgm;
    private AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM = GetComponent<AudioSource>();
        BGM.clip = bgm.audio;
        BGM.loop = true;
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
