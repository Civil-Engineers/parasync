using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Ink.Runtime.Object introSeen;
    public Ink.Runtime.Object juniorSeen;

    public void SaveVariablesFromStory(Story story)
    {

        this.introSeen = story.variablesState.GetVariableWithName("introSeen");
        this.juniorSeen = story.variablesState.GetVariableWithName("juniorSeen");
    }

    public void LoadVariablesToStory(ref Story story)
    {
        if (introSeen != null)
        {
            story.variablesState.SetGlobal("introSeen", introSeen);
            story.variablesState.SetGlobal("juniorSeen", juniorSeen);
        }
    }
}
