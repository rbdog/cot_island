using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PianoObserver
{
    void OnStrikeKey(Piano.Key key);
}

public class Piano : MonoBehaviour, PianoKeyObserver
{
    ///  鍵盤
    public enum Key
    {
        C,
        Cs,
        D,
        Ds,
        E,
        F,
        Fs,
        G,
        Gs,
        A,
        As,
        B
    }
    /// 鍵盤
    public PianoKey[] keys;

    AudioSource src;
    AudioClip soundC;
    AudioClip soundD;
    AudioClip soundE;
    AudioClip soundF;
    AudioClip soundG;
    AudioClip soundA;
    AudioClip soundB;

    public PianoObserver observer;

    // 鍵盤が押された時
    public void OnStrikeKey(Piano.Key key)
    {
        this.src.Stop();
        switch (key)
        {
            case Key.C:
                this.src.clip = soundC;
                break;
            case Key.D:
                this.src.clip = soundD;
                break;
            case Key.E:
                this.src.clip = soundE;
                break;
            case Key.F:
                this.src.clip = soundF;
                break;
            case Key.G:
                this.src.clip = soundG;
                break;
            case Key.A:
                this.src.clip = soundA;
                break;
            case Key.B:
                this.src.clip = soundB;
                break;
        }
        this.src.Play();

        this.observer?.OnStrikeKey(key);
    }

    void Start()
    {
        // AudioSource
        this.src = gameObject.AddComponent<AudioSource>();
        // 打鍵音をロード
        soundC = Resources.Load<AudioClip>("sound_effect/C");
        soundD = Resources.Load<AudioClip>("sound_effect/D");
        soundE = Resources.Load<AudioClip>("sound_effect/E");
        soundF = Resources.Load<AudioClip>("sound_effect/F");
        soundG = Resources.Load<AudioClip>("sound_effect/G");
        soundA = Resources.Load<AudioClip>("sound_effect/A");
        soundB = Resources.Load<AudioClip>("sound_effect/B");
        // 鍵盤にオブサーバーとして自分を登録
        if (keys.Length > 0)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].observer = this;
            }
        }
        else
        {
            Debug.Log("ピアノの鍵盤が見つかりません");
        }
    }

    void Update()
    {

    }
}
