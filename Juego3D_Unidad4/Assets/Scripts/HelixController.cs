using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();

    public float helixDistance;

    private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.position.y - (goalTransform.position.y + 0.1f);
        LoadStage(0);
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;
            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }
            
            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);

        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }

    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if (stage == null)
        {
            Debug.LogError("Stage not found");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        FindFirstObjectByType<BallControler>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float leveldistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;
        for (int i = 0; i < stage.levels.Count; i++)
        {
            float t = i / (float)(stage.levels.Count - 1);
            float yPos = Mathf.Lerp(topTransform.position.y, goalTransform.position.y, t);

            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.position = new Vector3(0, yPos, 0);
            spawnedLevels.Add(level);

            int partsToDisable= 12 -stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;

                if (!disabledParts.Contains(randomPart))
                {
                    disabledParts.Add(randomPart);
                    randomPart.SetActive(false);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform trans in level.transform) {
                trans.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;

                if (trans.gameObject.activeInHierarchy) {
                    leftParts.Add(trans.gameObject);
                }
                
            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];

                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }
}
