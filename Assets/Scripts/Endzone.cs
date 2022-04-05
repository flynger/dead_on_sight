using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StarterAssets
{
    public class Endzone : MonoBehaviour
    {
        public TextMeshProUGUI endText;
        public Renderer endZone;
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            endZone = GetComponent<Renderer>();
            endZone.material.color = Color.yellow;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision coll)
        {
            Collider other = coll.collider;
            Debug.Log("s");
            if (other.gameObject == player)
            {
                endText.gameObject.SetActive(true);
                endZone.material.color = Color.green;
                StartCoroutine(other.gameObject.GetComponent<Terminal>().RestartGame(15f));
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            endText.gameObject.SetActive(false);
            endZone.material.color = Color.yellow;
            player.transform.position = new Vector3(-7.4f, 0, -96);
        }
    }

}