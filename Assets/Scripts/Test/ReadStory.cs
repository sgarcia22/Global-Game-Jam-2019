using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System;


public class ReadStory : MonoBehaviour
{
    private string path = "Assets/Resources/story.txt";
    public List<string> parts = new List<string>();
    public int index = 0;
    public int solitude = 0;
    public int group = 0;
    public int adoption = 0;

    private static ReadStory current = null;

    public static ReadStory Instance
    {
        get
        {
            return current;
        }
    }

    public int getIndex ()
    {
        return index;
    }

    public void setIndex(int input)
    {
        index = input;
    }


    void Awake ()
    {
        try
        {
            string line;
            StreamReader theReader = new StreamReader(path, Encoding.Default);
            using (theReader)
            {
                do
                {
                    line = theReader.ReadLine();
                    if (line != null)
                        parts.Add(line);

                } while (line != null);
            }
        } catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return;
        }
        //for (int i = 0; i < parts.Count; ++i)
        //    Debug.Log(parts[i]);

        //Dont Destroy
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

}
