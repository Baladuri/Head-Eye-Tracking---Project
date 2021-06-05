﻿using UnityEngine;

public class ColliderHandleHard : MonoBehaviour
{
    public Global.GameObjectPattern selectedPatternSet;

    private void registerSelectedObject() {
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>();
        
        EyeOnlyEasyRunner runnerEasyInstance = GameObject
            .Find("GameRunner").
            GetComponent<EyeOnlyEasyRunner>();

        Familiarization runnerTrialInstance = GameObject
            .Find("GameRunner").
            GetComponent<Familiarization>();

        if (runnerInstance != null && 
            !runnerInstance.trialDone && 
            Global.currentState != TrialState.Head)
        {
            runnerInstance.selectedPatternSet = selectedPatternSet;
        }

        if (runnerEasyInstance != null && 
            !runnerEasyInstance.trialDone && 
            Global.currentState != TrialState.Head)
        {
            runnerEasyInstance.selectedPatternSet = selectedPatternSet;
        }

        if (runnerTrialInstance != null)
        {
            runnerTrialInstance.didEyeSelect = true;
        }
    }

    private void deRegisterSelectedObject() {
        // get runner instance
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>();

        EyeOnlyEasyRunner runnerEasyInstance = GameObject
            .Find("GameRunner").
            GetComponent<EyeOnlyEasyRunner>();
        
        Familiarization runnerTrialInstance = GameObject
            .Find("GameRunner").
            GetComponent<Familiarization>();
        
        // change background
        if (runnerInstance != null && 
            !runnerInstance.trialDone && 
            Global.currentState != TrialState.Head)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite 
                =  runnerInstance.white;
            
            runnerInstance.selectedPatternSet = null;
        }

        if (runnerEasyInstance != null && 
            !runnerEasyInstance.trialDone && 
            Global.currentState != TrialState.Head)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite 
                =  runnerEasyInstance.white;
            
            runnerEasyInstance.selectedPatternSet = null;
        }

        if (runnerTrialInstance != null)
        {
            runnerTrialInstance.didEyeSelect = null;
        }

        // observing handle in HeadEye case
        if (Global.currentState == TrialState.HeadEye) 
        { 
            GameObject
                .Find("headCursor")
                .GetComponent<HeadHandler>()
                .isObserving = false;
            
            GameObject
                .Find("headCursor")
                .GetComponent<HeadHandler>()
                .stateSequence
                .Clear();
        }
    }

    private void registerHeadSelectedObject() {
        // with condition 3, the collider of Head system is not required 
        // but still need a Head tracker object
        switch (Global.currentState)
        {
            case TrialState.Eye:
                return;
            case TrialState.Head:
                break;
            case TrialState.HeadEye:
                return;
            case TrialState.Order:
                break;
        }

        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>();
        
        EyeOnlyEasyRunner runnerEasyInstance = GameObject
            .Find("GameRunner").
            GetComponent<EyeOnlyEasyRunner>();
        
        Familiarization runnerTrialInstance = GameObject
            .Find("GameRunner").
            GetComponent<Familiarization>();

        if (runnerInstance != null && 
            runnerInstance.selectedPatternSet == selectedPatternSet) 
        {
            runnerInstance.headSelectedPatternSet = selectedPatternSet;
        }
        
        if (runnerEasyInstance != null && 
            runnerEasyInstance.selectedPatternSet == selectedPatternSet) 
        {
            runnerEasyInstance.headSelectedPatternSet = selectedPatternSet;
        }

        if (runnerTrialInstance != null)
        {
            runnerTrialInstance.didHeadSelect = true;
        }
    }

    private void deRegisterHeadSelectedObject() {
        // with condition 3, the collider of Head system is not required 
        // but still need a Head tracker object
        switch (Global.currentState)
        {
            case TrialState.Eye:
                return;
            case TrialState.Head:
                break;
            case TrialState.HeadEye:
                return;
            case TrialState.Order:
                break;
        }

        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>();
        
        EyeOnlyEasyRunner runnerEasyInstance = GameObject
            .Find("GameRunner").
            GetComponent<EyeOnlyEasyRunner>();
        
        Familiarization runnerTrialInstance = GameObject
            .Find("GameRunner").
            GetComponent<Familiarization>();

        if (runnerInstance != null && 
            runnerInstance.selectedPatternSet == selectedPatternSet) 
        {
            if (Global.currentState == TrialState.Head && 
                !runnerInstance.trialDone)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite 
                    = runnerInstance.white;
            }
            runnerInstance.headSelectedPatternSet = null;
        }
        
        if (runnerEasyInstance != null && 
            runnerEasyInstance.selectedPatternSet == selectedPatternSet) 
        {
            if (Global.currentState == TrialState.Head && 
                !runnerEasyInstance.trialDone)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite 
                    = runnerEasyInstance.white;
            }
            runnerEasyInstance.headSelectedPatternSet = null;
        }

        if (runnerTrialInstance != null)
        {
            runnerTrialInstance.didHeadSelect = null;
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("eyeCursor")) 
        {
            registerSelectedObject();
        }
        if (other.gameObject.name.Equals("headCursor")) 
        {
            registerHeadSelectedObject();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("eyeCursor")) 
        {
            deRegisterSelectedObject();
        }
        if (other.gameObject.name.Equals("headCursor")) 
        {
            deRegisterHeadSelectedObject();
        }
    }
}
