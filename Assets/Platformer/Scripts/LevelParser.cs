using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    [Header("Block Prefabs")]

    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;
    public GameObject goalPrefab;

    public Transform environmentRoot; // parent - clean, no need for thousand of blocks

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if (letter == 'x')
                {

                    Vector3 newPos = new Vector3(column, row, 0f);
                    GameObject newObj = Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                }

                if (letter == 's')
                {
                    Vector3 newStonePos = new Vector3(column, row, 0f);
                    GameObject newStone = Instantiate(stonePrefab, newStonePos, Quaternion.identity, environmentRoot);
                }

                if (letter == 'b')
                {
                    Vector3 newBrickPos = new Vector3(column, row, 0f);
                    GameObject newBrick = Instantiate(brickPrefab, newBrickPos, Quaternion.identity, environmentRoot);
                    
                }
                if (letter == '?')
                {
                    Vector3 newQuestionPos = new Vector3(column, row, 0f);
                    GameObject newQuestion = Instantiate(questionBoxPrefab, newQuestionPos, Quaternion.identity, environmentRoot);        
                }
                
                if (letter == 'w')
                {
                    Vector3 newWaterPos = new Vector3(column, row, 0f);
                    GameObject newQuestion = Instantiate(waterPrefab, newWaterPos, Quaternion.identity, environmentRoot);        
                }
                
                if (letter == 'g')
                {
                    Vector3 newGoalPos = new Vector3(column, row, 0f);
                    GameObject NewGoal = Instantiate(goalPrefab, newGoalPos, Quaternion.identity, environmentRoot);
                    
                }
              
           
            

               
            }
            row++;
            //column++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
