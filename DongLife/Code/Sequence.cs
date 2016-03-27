using System;
using System.Collections.Generic;
using DongLife.Scenes;

namespace DongLife.Code
{
    public class SequenceHandler
    {
        public int Stage = 0;
        public Dictionary<int, SequenceEvent> Sequences;

        public SequenceHandler()
        {
            this.Sequences = new Dictionary<int, SequenceEvent>();
        }
        public void RegisterSequence(int stageID, SequenceEvent sequence)
        {
            sequence.SequenceStage = stageID;
            this.Sequences.Add(stageID, sequence);
        }

        public void ProgressStage(int amount = 1)
        {
            Stage += amount;
        }
        public void SetStage(int stage)
        {
            Stage = stage;
        }

        public SequenceEvent GetCurrentSequence()
        {
            return Sequences[Stage];
        }
        public void ExecuteSequence(VNScene scene)
        {
            SequenceEvent current = GetCurrentSequence();
            if (current.SequenceType == SequenceTypes.Message)
            {
                if ((current as SequenceMessage).FocusActor)
                    scene.SetActorFocus((current as SequenceMessage).ActorName);
                VNScene.MessageBox.SetText((current as SequenceMessage).Text);
                ProgressStage();
            }
            else if (current.SequenceType == SequenceTypes.Decision)
            {
                if ((current as SequenceDecision).ChangeActor)
                    scene.SetActorFocus((current as SequenceDecision).ActorName);
                VNScene.MessageBox.SetButtons((current as SequenceDecision).DialogueOptions);
            }
            else if (current.SequenceType == SequenceTypes.StageTransition)
            {
                SetStage((current as SequenceStageTransition).TargetStage);
                ExecuteSequence(scene);
            }
            else if (current.SequenceType == SequenceTypes.SceneTransition)
            {
                scene.Manager.ChangeScene((current as SequenceSceneTransition).TargetScene);
            }
            else if (current.SequenceType == SequenceTypes.Special)
            {
                if (OnSequenceExecution != null)
                    OnSequenceExecution(current.SequenceStage, (current as SequenceSpecial).SequenceID);
            }
        }
        public void ExecuteChoice(VNScene scene, int buttonID)
        {
            SequenceEvent current = GetCurrentSequence();
            if (current.SequenceType == SequenceTypes.Decision)
                (current as SequenceDecision).ExecuteDecision(this, buttonID);
        }

        public bool OnFinalStage()
        {
            return (Stage >= Sequences.Count);
        }

        public delegate void SpecialSequenceDelegate(int stage, string id);
        public event SpecialSequenceDelegate OnSequenceExecution;
    }

    public abstract class SequenceEvent
    {
        public int SequenceStage;
        public SequenceTypes SequenceType;
    }
    public class SequenceMessage : SequenceEvent
    {
        public string Text;
        public string ActorName;
        public bool FocusActor;

        public SequenceMessage(string text)
        {
            this.Text = text;
            this.SequenceType = SequenceTypes.Message;

            this.FocusActor = false;
        }
        public SequenceMessage(string actorName, string text)
        {
            this.Text = text;
            this.ActorName = actorName;
            this.SequenceType = SequenceTypes.Message;

            this.FocusActor = true;
        }
    }
    public class SequenceDecision : SequenceEvent
    {
        public string ActorName;
        public string[] DialogueOptions;
        public bool ChangeActor = false;

        public SequenceDecision()
        {
            this.SequenceType = SequenceTypes.Decision;
        }
        public SequenceDecision(string actorName)
        {
            this.ActorName = actorName;
            this.SequenceType = SequenceTypes.Decision;
            this.ChangeActor = true;
        }
        public SequenceDecision(string actorName, params string[] choices)
        {
            this.ActorName = actorName;
            this.SequenceType = SequenceTypes.Decision;
            this.ChangeActor = true;
            this.DialogueOptions = choices;
        }

        public void ExecuteDecision(object sender, int id)
        {
            if (Choice != null && id >= 0 && id < DialogueOptions.Length)
                Choice(sender, id);
        }
        public void SetDialogueOptions(params string[] options)
        {
            this.DialogueOptions = options;
        }

        public delegate void DecisionChoiceDelegate(object sender, int decisionID);
        public event DecisionChoiceDelegate Choice;
    }
    public class SequenceStageTransition : SequenceEvent
    {
        public int TargetStage;

        public SequenceStageTransition(int targetStage)
        {
            this.TargetStage = targetStage;
            this.SequenceType = SequenceTypes.StageTransition;
        }
    }
    public class SequenceSceneTransition : SequenceEvent
    {
        public string TargetScene;

        public SequenceSceneTransition(string targetScene)
        {
            this.TargetScene = targetScene;
            this.SequenceType = SequenceTypes.SceneTransition;
        }
    }
    public class SequenceSpecial : SequenceEvent
    {
        public string SequenceID;
        public SequenceSpecial(string sequenceID)
        {
            this.SequenceID = sequenceID;
            this.SequenceType = SequenceTypes.Special;
        }        
    }

    public enum SequenceTypes { Message, Decision, StageTransition, SceneTransition, Special }
}
