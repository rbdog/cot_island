using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest001 : MonoBehaviour, PianoObserver
{
    AudioSource src;
    public Piano piano;

    List<Piano.Key> historyKeys = new List<Piano.Key>();
    List<Piano.Key> answers = new List<Piano.Key>() {
        Piano.Key.B,
        Piano.Key.A,
        Piano.Key.F,
        Piano.Key.D,
        Piano.Key.E,
        Piano.Key.G,
        Piano.Key.C,
    };

    public void OnStrikeKey(Piano.Key key)
    {
        // 履歴に追加
        historyKeys.Add(key);

        var solve = historyKeys.SequenceEqual(answers);
        if (solve)
        {
            // 履歴が答えに一致したら
            this.OnSolve();
            return;
        }
        
        for (int i = 0; i < historyKeys.Count; i++)
        {
            var historyKey = historyKeys[i]; 
            var answer = answers[i];
            if(historyKey != answer)
            {
                this.OnMiss();
                return;
            }
        }
    }

    // クエストクリア
    void OnSolve()
    {
        // クリア音を鳴らす
        this.src = gameObject.AddComponent<AudioSource>();
        var c = Resources.Load<AudioClip>("sound_effect/solve");
        this.src.clip = c;
        src.Play();
    }

    // クエスト失敗
    void OnMiss()
    {
        Debug.Log("クエストに失敗しました");
        // 履歴を全て削除
        this.historyKeys.RemoveAll((e) => true);
    }

    // Start is called before the first frame update
    void Start()
    {
        piano.observer = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
