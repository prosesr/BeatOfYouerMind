using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
   
    public int noteType;
    private GameManager.judges judge;
    private KeyCode keyCode;

    void Start()
    {
        if (noteType == 1) keyCode = KeyCode.D;
        else if (noteType == 2) keyCode = KeyCode.F;
        else if (noteType == 3) keyCode = KeyCode.J;
        else if (noteType == 4) keyCode = KeyCode.K;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed * Time.deltaTime); //노트 판정선으로 이동
        
        //사용자가 노트 키를 누를경우
        if(Input.GetKey(keyCode))
        {
            //노트 판정시작
            GameManager.instance.processJudge(judge, noteType);
            // 노트가 판정 선에 닿기 시작하면 노트를 비활성화
            if (judge != GameManager.judges.NONE) gameObject.SetActive(false);
        }
    }


    public void Initialize()
    {
        //초기화
        judge = GameManager.judges.NONE;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        } 
        else if(other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
            //오토모드 활성화
            if(GameManager.instance.autoPerfact)
            {
                GameManager.instance.processJudge(judge, noteType);
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            GameManager.instance.processJudge(judge, noteType);
            gameObject.SetActive(false);
        }
    }

}
