using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.
	
	public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.
		public float turnDelay = 0.1f;							//Delay between each Player turn.
		public int playerHealthPoints = 100;						//Starting value for Player health.
		public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
		[HideInInspector] public bool playersTurn = true;		//Boolean to check if it's players turn, hidden in inspector but public.
		
		
		private Text levelText;									//Text to display current level number.
		private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.
		private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
		private int level = 1;									//Current level number, expressed in game as "Day 1".
		private List<Enemy> enemies;							//List of all Enemy units, used to issue them move commands.
		private bool enemiesMoving;								//Boolean to check if enemies are moving.
		private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
        private int randomInt = 0;                              // an int that is set to a random number on startup

        // create an array of names
        private string[] rooms = new string[22];
        private string[] adjectives = new string[50];




        //Awake is always called before any Start functions
        void Awake()
		{
            // define all the arrays
            // define all values in the room array
            adjectives[0] = "Goddam Soft";
            adjectives[1] = "Dusty";
            adjectives[2] = "Bare";
            adjectives[3] = "Cellar";
            adjectives[4] = "Colonial";
            adjectives[5] = "European";
            adjectives[6] = "Bleak";
            adjectives[7] = "Smokey";
            adjectives[8] = "Vengeful";
            adjectives[9] = "Windowless";
            adjectives[10] = "Thick-walled";
            adjectives[11] = "Hot";
            adjectives[12] = "Noisy";
            adjectives[13] = "Dark";
            adjectives[14] = "Cheerful";
            adjectives[15] = "Punished";
            adjectives[16] = "Empty";
            adjectives[17] = "Damp";
            adjectives[18] = "Moist";
            adjectives[19] = "Revolting";
            adjectives[20] = "Common";
            adjectives[21] = "Ranch";
            adjectives[22] = "Readied";
            adjectives[23] = "Uncommon";
            adjectives[24] = "Theoretical";
            adjectives[25] = "Infested";
            adjectives[26] = "Smelly";
            adjectives[27] = "Memorable";
            adjectives[28] = "Spicey";
            adjectives[29] = "Spinning";
            adjectives[30] = "Cold";
            adjectives[31] = "Deadly";
            adjectives[32] = "Dicey";
            adjectives[33] = "Ancient";
            adjectives[34] = "Old";
            adjectives[35] = "Post Modern";
            adjectives[36] = "Slimey";
            adjectives[37] = "Crime Ridden";
            adjectives[38] = "Spooky";
            adjectives[39] = "Scary";
            adjectives[40] = "Sleepy";
            adjectives[41] = "Raunchy";
            adjectives[42] = "Unencumbered";
            adjectives[43] = "Comfortable";
            adjectives[44] = "Unoccupied";
            adjectives[45] = "Doomed";
            adjectives[46] = "Glorious";
            adjectives[47] = "Cursed";
            adjectives[48] = "Dank";
            adjectives[49] = "Diry";

            // define all values in the adjectives array
            rooms[0] = "Attic";
            rooms[1] = "Ballroom";
            rooms[2] = "Box Room";
            rooms[3] = "Cellar";
            rooms[4] = "Cloakroom";
            rooms[5] = "Conservatory";
            rooms[6] = "Dining Room";
            rooms[7] = "Drawing Room";
            rooms[8] = "Games Room";
            rooms[9] = "Hall";
            rooms[10] = "Landing";
            rooms[11] = "Larder";
            rooms[12] = "Library";
            rooms[13] = "Music Room";
            rooms[14] = "Office";
            rooms[15] = "Pantry";
            rooms[16] = "Parlour";
            rooms[17] = "Living Room";
            rooms[18] = "Spare Room";
            rooms[19] = "Guest Room";
            rooms[20] = "Toilet";
            rooms[21] = "Utility Room";

            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);	
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
			
			//Assign enemies to a new List of Enemy objects.
			enemies = new List<Enemy>();
			
			//Get a component reference to the attached BoardManager script
			boardScript = GetComponent<BoardManager>();
			
			//Call the InitGame function to initialize the first level 
			InitGame();
		}

        //this is called only once, and the paramter tell it to be called only after the scene was loaded
        //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static public void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //This is called each time a scene is loaded.
        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            instance.level++;
            instance.InitGame();
        }

		
		//Initializes the game for each level.
		void InitGame()
		{
			//While doingSetup is true the player can't move, prevent player from moving while title card is up.
			doingSetup = true;
			
			//Get a reference to our image LevelImage by finding it by name.
			levelImage = GameObject.Find("LevelImage");
			
			//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
			levelText = GameObject.Find("LevelText").GetComponent<Text>();

            //set the value of the random number
            randomInt = Random.Range(0, 11);
            Debug.Log(randomInt);

            //Set the text of levelText to a random name made by getting a string from rooms[] and adding a random number of adjectives.
            // no adjectives
            if (randomInt == 0)
			    levelText.text = rooms[Random.Range(0,21)];
            // one adjective
            else if (randomInt < 3)
                levelText.text = adjectives[Random.Range(0, 49)] + " " + rooms[Random.Range(0, 21)];
            // two adjectives
            else if (randomInt < 7)
                levelText.text = adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + " " + rooms[Random.Range(0, 21)];
            // three adjectives
            else if (randomInt < 10)
                levelText.text = adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + " " + rooms[Random.Range(0, 21)];
            // four adjectives
            else
                levelText.text = adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + ", " + adjectives[Random.Range(0, 49)] + " " + rooms[Random.Range(0, 21)];
            //Set levelImage to active blocking player's view of the game board during setup.
            levelImage.SetActive(true);
			
			//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
			Invoke("HideLevelImage", levelStartDelay);
			
			//Clear any Enemy objects in our List to prepare for next level.
			enemies.Clear();
			
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			boardScript.SetupScene(level);
			
		}
		
		
		//Hides black image used between levels
		void HideLevelImage()
		{
			//Disable the levelImage gameObject.
			levelImage.SetActive(false);
			
			//Set doingSetup to false allowing player to move again.
			doingSetup = false;
		}
		
		//Update is called every frame.
		void Update()
		{
			//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
			if(playersTurn || enemiesMoving || doingSetup)
				
				//If any of these are true, return and do not start MoveEnemies.
				return;
			
			//Start moving enemies.
			StartCoroutine (MoveEnemies ());
		}
		
		//Call this to add the passed in Enemy to the List of Enemy objects.
		public void AddEnemyToList(Enemy script)
		{
			//Add Enemy to List enemies.
			enemies.Add(script);
		}
		
		
		//GameOver is called when the player reaches 0 health points
		public void GameOver()
		{
			//Set levelText to display number of levels passed and game over message
			levelText.text = "After " + level + " days, you starved.";
			
			//Enable black background image gameObject.
			levelImage.SetActive(true);
			
			//Disable this GameManager.
			enabled = false;
		}
		
		//Coroutine to move enemies in sequence.
		IEnumerator MoveEnemies()
		{
			//While enemiesMoving is true player is unable to move.
			enemiesMoving = true;
			
			//Wait for turnDelay seconds, defaults to .1 (100 ms).
			yield return new WaitForSeconds(turnDelay);
			
			//If there are no enemies spawned (IE in first level):
			if (enemies.Count == 0) 
			{
				//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
				yield return new WaitForSeconds(turnDelay);
			}
			
			//Loop through List of Enemy objects.
			for (int i = 0; i < enemies.Count; i++)
			{
				//Call the MoveEnemy function of Enemy at index i in the enemies List.
				enemies[i].MoveEnemy ();
				
				//Wait for Enemy's moveTime before moving next Enemy, 
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			//Once Enemies are done moving, set playersTurn to true so player can move.
			playersTurn = true;
			
			//Enemies are done moving, set enemiesMoving to false.
			enemiesMoving = false;
		}
	}
}

