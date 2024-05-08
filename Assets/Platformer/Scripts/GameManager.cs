using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI coinsText;
    public GameObject backgroundTemplateObject;
    public TextMeshProUGUI levelText;
    public AudioClip brickBreakClip;
    public AudioClip coinClip;
    public AudioMixerGroup brickBreakGroup;
    public AudioMixerGroup coinGroup;

    private AudioSource brickBreakSource;
    private AudioSource coinSource;
    
    public GameObject brickPiecePrefab;
    
    public Camera mainCamera;


    private int initialTime = 400;
    private float elapsedTime = 0f;
    private int points = 0;
    private int coinAmount = 0;
    private int questionMarkValue = 200;
    public float cameraSpeed = 5f; 

    

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimerText();
        UpdatePointsText();
        UpdateCoinsText();
        

        SpriteRenderer spriteRenderer = backgroundTemplateObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            string spriteName = spriteRenderer.sprite.name;
            string levelValue = Regex.Match(spriteName, @"\d-\d").Value;

            levelText.text = $"World\n {levelValue}";
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on the Background Template Object!");
        }
        
        // handle audio
        
        brickBreakSource = gameObject.AddComponent<AudioSource>();
        brickBreakSource.clip = brickBreakClip;
        brickBreakSource.outputAudioMixerGroup = brickBreakGroup;

        coinSource = gameObject.AddComponent<AudioSource>();
        coinSource.clip = coinClip;
        coinSource.outputAudioMixerGroup = coinGroup;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       /* int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timerStr = $"Time \n {intTime}";
        timerText.text = timerStr;*/
       elapsedTime += Time.deltaTime;
       UpdateTimerText();
       HandleCameraMovement();
       
       if (Input.GetMouseButtonDown(0)) // left mouse click
       {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit, 100f))
           {
               GameObject clickedObject = hit.collider.gameObject;

               if (clickedObject.name.Contains("Brick"))
               {
                   AddPoints(100);
                   Destroy(clickedObject);
                   SpawnBrickPieces(hit.point);
                   AudioManager.Instance.PlayBrickBreak();

               }
               else if (clickedObject.name.Contains("Question"))
               {
                   AddCoin(questionMarkValue);
                   // Todo: change texture to solid brown 
                   AudioManager.Instance.PlayCoin();

               }
           }
       }
    }
    
    private void HandleCameraMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 cameraMove = new Vector3(horizontalInput * cameraSpeed * Time.deltaTime, 0, 0);
        mainCamera.transform.Translate(cameraMove, Space.World);
    }
    
    private void UpdateTimerText()
    {
        int remainingTime = initialTime - (int)elapsedTime;
        timerText.text = $"Time \n {remainingTime}";
    }
    
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        pointsText.text = $"MARIO \n {points:000000}";
    }

    public void AddCoin(int coinsToAdd)
    {
        coinAmount += coinsToAdd;
        Debug.Log($"Adding coin. New coin amount: {coinAmount}");
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        Debug.Log($"Updating coin text to: x {coinAmount:00}");
        coinsText.text = $"x {coinAmount:00}";
    }
    
    private void SpawnBrickPieces(Vector3 spawnPosition)
    {
        Vector3[] directions = {
            new Vector3(-1, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(-1, -1, 0),
            new Vector3(1, -1, 0)
        };

        foreach (var direction in directions)
        {
            GameObject piece = Instantiate(brickPiecePrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            rb.AddForce(direction * Random.Range(5f, 10f), ForceMode.Impulse);
            Destroy(piece, 2f); // Destroy the piece after 2 seconds
        }
        
      
    }
    
    
}
