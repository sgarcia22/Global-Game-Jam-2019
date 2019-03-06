using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;


public class MainLogic : MonoBehaviour
{


    private static MainLogic current = null;

    public static MainLogic Instance
    {
        get
        {
            return current;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        /* code to catch next action
        
        persistChoices

        var nextThing = persistChoices.getNextAction(INTCHOICE); //code this
        switch (nextThing.item1)
        {
            case "question":
            //CALL QUESTION TEXT WRITING HERE
            question text = nextThing.item2
            case "minigame":
            minigame name = nextThing.item2
            //CALL SCENE WITH MINIGAME HERE
            case "end":
                //persistChoices.get
        };*/



        //LEAVES -> adds points

        if (current != null && current != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}


public class ChoiceItem
{
    public readonly int refID;
    public readonly List<int> children = new List<int>();
    public readonly string choiceType;  //question, minigame or leaf
    public readonly string relatedText; //question text/minigame name/points to


    public ChoiceItem(int ID, List<int> linkTo, string selectionType, string text)
    {
        refID = ID;
        children = linkTo;
        choiceType = selectionType;
        relatedText = text;
    }



}

public class ChoiceManager
{
    public readonly Dictionary<int, ChoiceItem> choiceTracker = new Dictionary<int, ChoiceItem>();
    private readonly Dictionary<string, int> endScenes = new Dictionary<string, int>();  //counter to which scene to trigger
    private int currentID;
    private static int TOP_LEVEL_COUNT = 600;


    public ChoiceManager()
    {
        endScenes.Add("solitary", 0);
        endScenes.Add("group", 0);
        endScenes.Add("adopt", 0);
        currentID = 0;


        

        choiceTracker.Add(90, new ChoiceItem(90, new List<int> { }, "leaf", "solitary"));
        choiceTracker.Add(91, new ChoiceItem(91, new List<int> { }, "leaf", "group"));
        choiceTracker.Add(92, new ChoiceItem(92, new List<int> { }, "leaf", "adopt"));

        //choiceTracker.Add(1, new ChoiceItem(1, new List<int> { 111, 11 }, "question", "I am the first question. Answer 0 will get you points and then to the second question and answer 1 will get you to my child question"));
       // choiceTracker.Add(11, new ChoiceItem(11, new List<int> { 90, 111 }, "question", "I am a grandchild question to question 1. answer 0 and there will be points and go to question 2, answer 1 and you will get my grandchild question"));
        //choiceTracker.Add(111, new ChoiceItem(111, new List<int> { 90, 91 }, "question", "I am the GREAT grandchild question. Answer 0 or 1 and you get points and then get to play a minigame"));
        //choiceTracker.Add(2, new ChoiceItem(2, new List<int> { 90, 91 }, "minigame", "ShootingWispsMinigame"));
        //choiceTracker.Add(3, new ChoiceItem(3, new List<int> { 90, 91 }, "question", "Questions after wisps"));

      choiceTracker.Add(1, new ChoiceItem(1, new List<int> { 10, 11 }, "question", "Now, I've seen your condition only once before. We need to get to the root of the corruption before it spreads and you shutdown for good. Can you recall any discordant memory fragments?"));
      choiceTracker.Add(10, new ChoiceItem(10, new List<int> { 100, 91 }, "question", " Huh, well then there must be some other source of corruption. .. Do you remember anything at all?"));
      choiceTracker.Add(100, new ChoiceItem(100, new List<int> { 92, 90 }, "question", "This is worse than I thought. I'll need to trigger your discordance mechanism manually. Are you ready?"));
      choiceTracker.Add(11,  new ChoiceItem(11, new List<int> { 110, 90 }, "question", "Okay, that may just be the source of the corruption. We should dig a little deeper...  I wonder what caused the discordance?"));
      choiceTracker.Add(110,  new ChoiceItem(110, new List<int> { 90, 92 }, "question", "...was it some event?"));
      choiceTracker.Add(2,  new ChoiceItem(2, new List<int> { 90, 91, 92 }, "minigame", "ShootingWispsMinigame"));
      choiceTracker.Add(3, new ChoiceItem(3, new List<int> { 90, 31 }, "question", "You had quite the episode there, your memory corruption seems to be clearing itself. e should continue. Do you recall anybody important to you?"));
      choiceTracker.Add(31, new ChoiceItem(31, new List<int> { 92, 91 }, "question", "Interesting, was it many people?"));
      choiceTracker.Add(4, new ChoiceItem(4, new List<int> { 10, 11 }, "minigame", "WalkingToFogMinigame"));
      choiceTracker.Add(600, new ChoiceItem(31, new List<int> { 92, 91 }, "minigame", ""));
      choiceTracker.Add(60, new ChoiceItem(60, new List<int> { 91, 90 }, "question", "Would you feel comfortable alone?"));
      choiceTracker.Add(61, new ChoiceItem(61, new List<int> { 91, 92 }, "question", "Do you still feel like you don't belong anywhere?"));
      choiceTracker.Add(62, new ChoiceItem(62, new List<int> { 90, 92 }, "question", "Do you still feel like you don't belong anywhere?"));


    }
    
    int needTieBreaker()
    {   //returns 0 if no tie breaker needed
        int s = endScenes["solitary"];
        int g = endScenes["group"];
        int a = endScenes["adopt"];

        if (s == g && s >= a) return 60;  //HARD CODED id for first tie breaker question
        if (s == a && s > g) return 62; //hard coded question id
        if (g == a && g > s) return 61;

        return 0;  // no tie breaker question
    }

    public (string, string) getNextAction(int choiceNum)
    //returns action type & action name/specifics
    {
        int tie;
        ChoiceItem nextChild;
        string [] ending = new string[]{"solitary", "group", "adopt"};
        int endIndex = 0;
        for (int i = 1; i < 3; i++)
        {
            Debug.Log("STUFFFFF");
            Debug.Log(choiceTracker[i].relatedText);
        }

        if (currentID == 0)
        {
            currentID += 1;
            nextChild = choiceTracker[currentID];
            return (nextChild.choiceType, nextChild.relatedText);

        }
        if (currentID >= TOP_LEVEL_COUNT)  //reached the end, check for tie
        {
            tie = needTieBreaker();
            if (tie == 0)
            {
                if (endScenes[ending[0]] >= endScenes[ending[1]] && endScenes[ending[0]] >= endScenes[ending[2]]) endIndex = 0;
                if (endScenes[ending[1]] >= endScenes[ending[2]] && endScenes[ending[1]] >= endScenes[ending[0]]) endIndex = 1;
                if (endScenes[ending[2]] >= endScenes[ending[0]] && endScenes[ending[2]] >= endScenes[ending[1]]) endIndex = 2;

                currentID = 0;
                for (int i = 0; i < 3; i++) endScenes[ending[i]] = 0;
                return ("minigame", ending[endIndex]);
            }
            else return (choiceTracker[tie].choiceType, choiceTracker[tie].relatedText);
        }


        if (choiceTracker[currentID].children.Count == 0)
        {
            while (currentID >= 10) currentID /= 10;
            currentID += 1;
        }
        nextChild = choiceTracker[choiceTracker[currentID].children[choiceNum]];
        //   }
        if (nextChild.choiceType == "leaf")   //check for a leaf and advance to next question
        {
            endScenes[nextChild.relatedText] += 1; //increment counter
            
            //get first digit
            while (currentID >= 10) currentID /= 10;
            currentID += 1;
            Debug.Log("Inside Leaf");
            Debug.Log(currentID);
            Debug.Log(choiceTracker[currentID].relatedText);

           

            nextChild = choiceTracker[currentID];
        }



        currentID = nextChild.refID;
        return (nextChild.choiceType, nextChild.relatedText);
        
    }


    public (int, int, int, int ) getPersistInfo()
    {
        return (currentID, endScenes["solitary"], endScenes["group"], endScenes["adopt"]);
    }

    public void setPersist(int item1, int item2, int item3, int item4)
    {

        currentID = item1;
        endScenes["solitary"] = item2;
        endScenes["group"] = item3;
        endScenes["adopt"] = item4;
    }



}




