using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Code Reference
 * Author: SpeedTutor (Youtube)
 * URL: https://www.youtube.com/watch?v=bR0clpZvjXo
 * Title: A Beginner Guide to TEXTMESHPRO (Unity UI Tutorial)
 */
namespace BeanstalkBlitz
{
    public class TextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextbox;
        int score;

        void Start()
        {
            score = 0;
            scoreTextbox.text = "Stomp on bugs (Red Boxes) before they eat through the beanstalk stem!\n" +
                                "Reach the treasure at the top (Gold Orb) to win the game!";
        }

        public void UpdateScore(int addScore)
        {
            score += addScore;
            scoreTextbox.text = $"Bugs stomped: {score}";
        }

        public void WinGame()
        {
            scoreTextbox.text = "YOU WIN!\n" +
                                "You reached the treasure in the clouds";
        }
    }
}