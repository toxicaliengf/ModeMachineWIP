using System.Collections.Generic;
using UnityEngine;

namespace ModeMachine
{
    public abstract class Mode<T> : Mode where T : class, IModeStack
    {
        //this gives a type-correct reference to the stack the mode is on//sdfflaksdjhfalskjdhfasldkjfh
        new public T ParentStack { get { return base.ParentStack as T; } }

        public sealed override int GetDepth()
        {
            if (ParentStack == null)
                return -1;
            return ParentStack.ModeStack.Modes.IndexOf(this);
        }
        public sealed override int GetDepth(params ChannelID[] channelFilter)
        {
            if (channelFilter.Length == 0)
                return GetDepth();
            if (ParentStack == null)
                return -1;

            //create a list of all modes that have the correct channels (plus this mode)
            List<Mode> modes = new List<Mode>(ParentStack.ModeStack.Modes);
            for (int i = modes.Count; i > 0;)
            {
                i--;
                if (modes[i] == this)
                    continue;

                for (int n = 0; n < channelFilter.Length; n++)
                {
                    if (!modes[i].GetChannel(channelFilter[n]))
                    {
                        modes.RemoveAt(i);
                    }
                }
            }
            return modes.IndexOf(this);
        }

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
