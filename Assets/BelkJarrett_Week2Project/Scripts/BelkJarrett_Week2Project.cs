using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BelkJarrett_Week2Project : MonoBehaviour
{
    Text screen;
    Page[] book;

    [SerializeField]
    string prevHeading;
    [SerializeField]
    string currHeading;
    [SerializeField]
    string nextHeading = "wake up";
   
    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.Find("MainText");

        if (go)
        {
            screen = go.GetComponent<Text>();

            if (!screen)
            {
                Debug.LogError("Text Component was not found on MainText");
            }
        }
        else
        {
            Debug.LogError("MainText not found");
        }

        //screen.text = "Hello Word";

        BindBook();
    }    
    
        // Update is called once per frame
    void Update()
    {
        HandleInput();
        RenderStory();
    }

    void BindBook()
    {
        book = new Page[]
        {
            new Page("wake up", "<b><i>(this is: wake up)</i></b>\n\nYou awaken in a sweltering room that smells of dust and dry rot. " +
                "To your left, you see a woven basket, about three feet in front" +
                " you see a deep steel bowl filled with what looks like milk," +
                " and 10 feet beyond that is a heavy iron door you assume is locked. " +
                "Will you check the [W]oven Basket, [B]owl of Milk, or [D]oor?"),

            new Page("Woven Basket", "<b><i>(this is: Woven Basket)</i></b>\nYou walk over to examine the woven basket, " +
                "which stands almost 3 feet tall. " +
                "You see that the lid on the basket has a handle, but when you try to pull up," +
                " it seems to be tied down." +
                "You find ties on four sides of the basket holding the lid on. " +
                "You undo the ties and find the basket oddly empty." +
                " Press [X] to return to the previous step."),

            new Page("Door", "<b><i>(this is: Door)</i></b>\nWhile you think it should be easy enough to waltz through the iron door. " +
                "When you come upon it, you realize it is much heavier than originally thought. " +
                "You put all your weight into it to find it only groans and will not budge. " +
                "It seems you will need a key to unlock the door before it moves anymore." +
                " Press [X] to return to the previous step."),

            new Page("Bowl of Milk", "<b><i>(this is: Bowl of Milk)</i></b>\nYou look at the bowl of creamy liquid before you, " +
                "unsure of its contents. " +
                "You tap the bowl with your foot and see that the liquid moves with the same viscosity as milk would. " +
                "You are quite parched, do you [P]ick up the bowl or press [X] to return."),

            new Page("Pick Up", "<b><i>(this is: Pick Up)</i></b>\nYou pick up the bowl and bring it closer to sniff the contents." +
                " The smell of the liquid is mild, though there is a twinge" +
                " of something odd to it that you cannot place your finger on." +
                "You slosh the liquid a bit and notice something shifting at the bottom of the bowl. " +
                "Do you [E]mpty out the contents or [S]wallow the liquid? Or press [X] to return to the previous step."),

            //win!
            new Page("Empty", "<b><i>(this is: Empty)</i></b>\nYou pour out all the contents onto the dusty floor, " +
                "including what seems to be some maggots and a wrought iron key." +
                " One that looks to fit in the keyhole of the heavy door. " +
                "You use the key to unlatch the door and budge it <color=lime>enough to escape.</color>"),

            //lose!
            new Page("Swallow", "(this is: Swallow)\nYou decide your thirst could help to kill two birds" +
                " with one stone and begin to drink the liquid. " +
                "The liquid isn’t milk at all and has a severe acrid taste, you swallow some more anyway, " +
                "feeling chunks of items floating on your tongue. " +
                "You spit out the bits and upon further inspection see they are wriggling. " +
                "You toss the bowl away from you as you feel the need to wretch." +
                "Standing up you see a key landing next to the tossed bowl and as you go to reach for it, " +
                 "you fall forward, <color=red>mind hazing into unconsciousness.</color>"),
        };
    }

    void RenderStory()
    {
        if (!string.IsNullOrEmpty(nextHeading))
        {
            for (int i = 0; i < book.Length; i++)
            {
                if (nextHeading == book[i].Heading)
                {
                    prevHeading = currHeading;
                    currHeading = nextHeading;
                    nextHeading = "";

                    screen.text = book[i].Body;
                    Debug.Log(book[i].Body);

                    return;
                }
            }

            Debug.LogWarning("Heading not found: \"" + nextHeading + "\""); 
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currHeading == "wake up")
            {
                nextHeading = "Woven Basket";
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currHeading == "wake up" || currHeading == "Pick Up" || currHeading == "Drink")
            {
                nextHeading = "Door";
            }
            else if (currHeading == "Pick Up")
            {
                nextHeading = "Drink";
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (currHeading == "wake up")
            {
                nextHeading = "Bowl of Milk";
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (currHeading == "Bowl of Milk" || currHeading == "Pick Up")
            {
                nextHeading = "Pick Up";
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currHeading == "Pick Up")
            {
                nextHeading = "Swallow";
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (currHeading == "Pick Up")
            {
                nextHeading = "Empty";
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            nextHeading = prevHeading;
        }
    }
}
