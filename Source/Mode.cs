using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace ModeMachine
{
    
    public class TestStack : IModeStack
    {
        public ModeStack ModeStack => throw new NotImplementedException();
    }

    public class TestMode : Mode<TestStack>
    {
        
    }

    public abstract class Mode : MonoBehaviour
    {
        //constructor is internal to prevent user from inheriting directly from ModeBase
        internal Mode(){ }
        internal IModeStack ParentStack;//user should not be able to change ParentStack manually

        //events
        internal Action WasPushed,
            WasRemoved,
            StackWasChanged;

        //state
        private bool Initialized = false;
        private bool applicationQuitting = false;
        
        //subscribe to delegates
        internal void InitializeIfNeeded()
        {
            if (Initialized)
                return;
            Initialized = true;
            Application.quitting += DetectQuit;
            WasPushed += OnPush;
            WasRemoved += OnRemove;
            StackWasChanged += OnStackChanged;
            ModeDelegateSubscriptionManager.Instance.RegisterMode(this);
        }

        internal void CleanupDelegates()
        {
            Application.quitting -= DetectQuit;
            WasPushed -= OnPush;
            WasRemoved -= OnRemove;
            StackWasChanged -= OnStackChanged;
        }

        //need to know if application is quitting so we don't call stack changed events from ondestroy
        void DetectQuit()
        {
            applicationQuitting = true;
            Application.quitting -= DetectQuit;
        }

        protected virtual void OnDestroy()
        {
            CleanupDelegates();
            if (ParentStack != null && !applicationQuitting)
                ParentStack.RemoveMode(this);
        }

        protected virtual void OnPush(){}
        protected virtual void OnRemove() { }
        protected virtual void OnStackChanged() { }
        
        //bookkeeping functionality
        internal abstract bool ValidateParentStackType(IModeStack newStack);
    }

    public abstract class Mode<T> : Mode where T : class, IModeStack
    {
        //this gives a type-correct reference to the stack the mode is on
        new public T ParentStack { get { return base.ParentStack as T; } }

        internal override bool ValidateParentStackType(IModeStack newStack)
        {
            if (newStack as T == null)
            {
                Debug.LogErrorFormat("Trying to push {0} mode to the wrong type of stack. Was {1}, should be {2}", gameObject.name, newStack.GetType().ToString(), typeof(T).ToString());
                return false;
            }
            return true;
        }
    }
}