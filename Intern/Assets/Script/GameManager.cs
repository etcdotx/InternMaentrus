using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Transform[] positions;
    public GameObject[] ball;
    public List<ScreenWrap> cube;
    public  int ballCount = 0;
    public int score = 0;
    public enum states {play,notplay};
    public static states state;
    public static int force = 10;
    public TextMeshProUGUI taptoplay;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI hiscoretxt;
    private int highscore = 0;
    private int lives;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        load();
        UpdateHigh();
        lives = 3;
        state = states.notplay;
        StartCoroutine(Spawn());
    }

    public static void FindSelf(GameObject obj,int code) {
        int i = 0;
        foreach (ScreenWrap screen in instance.cube) {
            if (GameObject.Equals(screen.gameObject, obj))
            {
                break;
            }
            else {
                i++;
            }
        }
        int next;
        if (code == 0)
        {
            if (i == 0)
            {
                next = 8;
            }
            else
            {
                next = i - 1;
            }
            Debug.Log(i);
            obj.transform.position = new Vector3(instance.cube[next].transform.position.x + 1.018f, obj.transform.position.y, obj.transform.position.z);
        }
        else {
            if (i == 8)
            {
                next = 0;
            }
            else
            {
                next = i + 1;
            }
            obj.transform.position = new Vector3(instance.cube[next].transform.position.x - 1.018f, obj.transform.position.y, obj.transform.position.z);
        }
    }

    public static void Down()
    {
        foreach (ScreenWrap screen in instance.cube)
        {
            screen.transform.Translate(0f, -0.2f, 0f);
        }
        instance.lives--;
        if (instance.lives == 0) {
            instance.save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static void addScore() {
        instance.score++;
        instance.updatescore();
    }

    public void updatescore() {
        scoretxt.text = score.ToString();
        if (score > highscore) {
            highscore = score;
            hiscoretxt.text = "BEST : " + highscore.ToString();
        }
    }

    public void UpdateHigh()
    {
        hiscoretxt.text = "BEST : " + highscore.ToString();
    }

    public static void changestate(){
        state = states.play;
        instance.taptoplay.gameObject.SetActive(false);
        instance.scoretxt.gameObject.SetActive(true);
    }

    IEnumerator Spawn() {
        bool running = true;
        do
        {
            if (state == states.play)
            {
                Vector3 rand = new Vector3(Random.Range(positions[0].transform.position.x, positions[1].transform.position.x), positions[0].transform.position.y, Random.Range(-0.5f, 0) + positions[0].transform.position.z);
                int a = Random.Range(0, ball.Length);
                Instantiate(ball[a], rand, Quaternion.identity);
                ballCount++;
                if (ballCount % 5 == 0 && ballCount!= 0)
                {
                    Debug.Log("A)");
                    force *= 2;
                    Debug.Log(force);
                }
               
            }
            yield return new WaitForSeconds(5f);
        } while (running);
    }


    private void load() {
        if (File.Exists(Application.persistentDataPath + "/info.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);
            scorevariable sc = (scorevariable)bf.Deserialize(file);
            highscore = sc.highscore;
            file.Close();
        }
    }

    private void save() {
        scorevariable sc = new scorevariable();
        sc.highscore = highscore;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/info.dat");
        bf.Serialize(file, sc);
    }
}

[System.Serializable]
class scorevariable {
    public int highscore;
}
