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
    public GameObject[] meters;

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

    int meterIndex(Key key) {
        int index = 0;
        switch (key)
        {
            case Key.C:
                index = 11;
                break;
            case Key.Cs:
                index = 10;
                break;
            case Key.D:
                index = 9;
                break;
            case Key.Ds:
                index = 8;
                break;
            case Key.E:
                index = 7;
                break;
            case Key.F:
                index = 6;
                break;
            case Key.Fs:
                index = 5;
                break;
            case Key.G:
                index = 4;
                break;
            case Key.Gs:
                index = 3;
                break;
            case Key.A:
                index = 2;
                break;
            case Key.As:
                index = 1;
                break;
            case Key.B:
                index = 0;
                break;
        }
        return index;
    }

    public void OnChangeY(Key key, float offset)
    {
        var i = meterIndex(key);
        var x = meters[i].transform.localScale.x;
        var y = 1 + (offset * 2);
        var z = meters[i].transform.localScale.z;
        meters[i].transform.localScale = new Vector3(x, y, z);
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
