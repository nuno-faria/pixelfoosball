using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIManager : MonoBehaviour {

    private static List<Rigidbody2D> players;
    private static Rigidbody2D ball;
    private static float speed;

    void Awake() {
        players = GameObject.FindGameObjectsWithTag("p2").ToList().Select(x => x.GetComponent<Rigidbody2D>()).ToList();
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody2D>();
        speed = 5.0f;
    }

    public static void processAI() {
        if (!(ball.velocity == new Vector2(0, 0))) {
            Rigidbody2D player = getClosest();

            //ball 'stuck'
            if (ball.velocity.y == 0) {
                if (ball.transform.position.y > player.position.y)
                    applySpeed(-speed);
                else
                    applySpeed(speed);

            }

            if (ball.position.y > player.position.y - 0.1f)
                applySpeed(speed);
            else if (ball.position.y < player.position.y + 0.1f)
                applySpeed(-speed);
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
