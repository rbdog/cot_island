using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest001 : MonoBehaviour, PianoObserver
{
    AudioSource src;
    public Piano piano;
    public GameObject[] blocks;

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

        StartCoroutine(ShowBlocks());
    }

    // クエスト失敗
    void OnMiss()
    {
        Debug.Log("クエストに失敗しました");
        // 履歴を全て削除
        this.historyKeys.RemoveAll((e) => true);
    }

    // ブロック出現
    IEnumerator ShowBlocks()
    {
        for (float i = 0; i < 1; i += 0.1f)
        {
            for (int bi = 0; bi < blocks.Length; bi += 1)
            {
                var x = blocks[bi].transform.localScale.x;
                var y = i;
                var z = blocks[bi].transform.localScale.z;
                blocks[bi].transform.localScale = new Vector3(x, y, z);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
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
