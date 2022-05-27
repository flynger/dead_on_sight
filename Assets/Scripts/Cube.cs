using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace StarterAssets {
    public class Cube : Entity
    {
        public bool isTrap;
        public TextMeshProUGUI endText;
        public Renderer endZone;
        public GameObject blackOutSquare;
        public Button nextLvlBtn;
        public Button restartBtn;
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            if (!isTrap) {
                endZone.material.color = Color.red;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("E");
            if (collision.gameObject.tag == "player") {
                Debug.Log("I'm working");
                if (isTrap) {
                    hitPoints -= 2;
                }
                else {
                    endText.gameObject.SetActive(true);
                    nextLvlBtn.gameObject.SetActive(true);
                    restartBtn.gameObject.SetActive(true);
                    StartCoroutine(FadeBlackOutSquare());
                    endZone.material.color = Color.green;
                }
            }
        }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextLevel() {
            player.transform.position = new Vector3(9, 12, -67);
        }

        public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1) {
            Color objectColor = blackOutSquare.GetComponent<Image>().color;
            float fadeAmount;

            if (fadeToBlack)
            {
                while (blackOutSquare.GetComponent<Image>().color.a < 1)
                {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                    yield return null;
                }
            }
            else {
                while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                    yield return null;
                }
            }
        }
    }
}

