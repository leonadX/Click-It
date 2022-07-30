using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyDisplay : MonoBehaviour
{
    public List<Enemy> enemy = new List<Enemy>();

    [SerializeField] TextMeshProUGUI enemyNameText;

    [SerializeField] TextMeshProUGUI highScore;

    [SerializeField] TextMeshProUGUI weaponName;

    public Image enemyImage;

    public Slider health;


    private int enemyNumber;
    public static int highscore;
    public static int score;
    private float enemyHealth;

    void Start()
    {
        enemyNumber = 0;
        enemyNameText.text = enemy[enemyNumber].enemyName;
        weaponName.text = enemy[enemyNumber].weaponName;

        enemyImage.sprite = enemy[enemyNumber].enemyImage;


        health.maxValue = enemy[enemyNumber].health;
        health.value = enemy[enemyNumber].health;

        enemyHealth = enemy[enemyNumber].health;
        highScore.text = "Score: " + score.ToString();
        highscore = (highscore < score) ? score : highscore;
    }

    public void KillEnemy(int _damage)
    {
        enemyHealth -= _damage;
        health.value = enemyHealth;

        if (health.value == 0)
        {
            score += 1;
            highscore = (highscore < score) ? score : highscore;
            highScore.text = "Score: " + score.ToString();

            if (highscore%6 == 0)
            {
                if (SceneManager.GetActiveScene().buildIndex < 3)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else
                {
                    SceneManager.LoadScene(0);
                    score = 0;
                }
                return;
            }

            if (highscore == 10)
            {
                Social.ReportProgress(GPGSIds.achievement_killed_10_enemies, 100.0f, null);
            }

            ChangeEnemy();
            return;
        }



    }

    void ChangeEnemy()
    {
        if (enemyNumber == enemy.Count - 1) enemyNumber = 0;
        else enemyNumber++;

        enemyNameText.text = enemy[enemyNumber].enemyName;
        weaponName.text = enemy[enemyNumber].weaponName;

        enemyImage.sprite = enemy[enemyNumber].enemyImage;

        enemyHealth = enemy[enemyNumber].health;
        health.maxValue = enemyHealth;
        health.value = enemyHealth;

        Debug.Log("Enemy Changed");
    }
}
