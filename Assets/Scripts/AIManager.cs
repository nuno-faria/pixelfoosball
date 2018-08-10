using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIManager : MonoBehaviour {

    private static List<Rigidbody2D> players;
    private static Rigidbody2D ball;
    private static float speed;
    private static float thinkTime = 0.075f;
    private static float nextProcessTime;
    private static int nConsecutiveProcesses = 2; //ai does n calculations at a time
    private static int currentNProcesses;

    void Awake() {
        players = GameObject.FindGameObjectsWithTag("p2").ToList().Select(x => x.GetComponent<Rigidbody2D>()).ToList();
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody2D>();
        speed = 5.0f;
        nextProcessTime = 0;
        currentNProcesses = 0;
    }

    void Start() {
        switch (GameManager.gm.difficulty) {
            case 0:
                thinkTime = 0.125f;
                speed = 4.5f;
                break;
            case 1:
                thinkTime = 0.08f;
                speed = 5f;
                break;
            case 2:
                thinkTime = 0.02f;
                speed = 5f;
                break;
        }
    }

    public static void processAI() {
        if (nextProcessTime <= 0 && !(ball.velocity == new Vector2(0, 0))) {
            Rigidbody2D player = getClosest();

            if (ball.position.y > player.position.y - 0.05f)
                applySpeed(speed);
            else if (ball.position.y < player.position.y + 0.05f)
                applySpeed(-speed);
            else applySpeed(0);

            currentNProcesses++;
            if (currentNProcesses == nConsecutiveProcesses) {
                nextProcessTime = thinkTime;
                currentNProcesses = 0;
            }
        }
        else {
            applySpeed(0);
            nextProcessTime -= Time.deltaTime;
        }
    }

    public static void applySpeed(float speed) {
        Vector2 v = new Vector2(0, speed);
        players.ForEach(x => x.velocity = v);
    }

    public static Rigidbody2D getClosest() {
        return players.Aggregate((p1, p2) => Vector2.Distance(p1.position, ball.position) <
                                              Vector2.Distance(p2.position, ball.position)
                                             ? p1 : p2);
    }
}
