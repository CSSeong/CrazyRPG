using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialoguelist = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[]{'\n'});

        for(int i = 1; i < data.Length;)
        {
            string[] rows = data[i].Split(new char[]{','});

            Dialogue dialogue = new Dialogue();

            dialogue.name = rows[1];
            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(rows[2]);
                if (++i < data.Length)
                {
                    rows = data[i].Split(new char[]{','});
                }
                else
                {
                    break;
                }
            }
            while (rows[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            
            dialoguelist.Add(dialogue);
            
        }
        return dialoguelist.ToArray();
    }

}
