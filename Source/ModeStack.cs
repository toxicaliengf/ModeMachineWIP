﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ModeMachine
{
    [System.Serializable]
    public sealed class ModeStack
    {
        List<Mode> _Modes = new List<Mode>();
        public ReadOnlyCollection<Mode> Modes { get { return _Modes.AsReadOnly(); } }
        public IModeStack owner { get; private set; }

        internal void ValidateOwner(IModeStack _owner)
        {
            owner = _owner;
        }

        public List<Mode> GetModes(params ChannelID[] channelFilter)
        {
            List<Mode> result = new List<Mode>(_Modes);
            for(int i = result.Count; i > 0;)
            {
                i--;
                for (int n = 0; n < channelFilter.Length; n++)
                {
                    if (!result[i].GetChannel(channelFilter[n]))
                    {
                        result.RemoveAt(i);
                    }
                }
            }
            return result;
        }

        internal void PushMode(Mode mode)
        {
            if (mode.ValidateParentStackType(owner))
            {
                if (_Modes == null)
                    _Modes = new List<Mode>();

                if (_Modes.Contains(mode))
                {
                    //mode is already on stack. Position it on top
                    if (_Modes.IndexOf(mode) == 0)
                        return;
                    _Modes.Remove(mode);
                }

                mode.InitializeIfNeeded();

                mode.ParentStack = owner;
                _Modes.Insert(0, mode);

                mode.WasPushed?.Invoke();

                //mode was successfully pushed
                for (int i = 1; i < _Modes.Count; i++)
                {
                    if (_Modes[i] != null)
                        _Modes[i].StackWasChanged?.Invoke();
                }
            }
        }

        internal void RemoveMode(Mode mode)
        {
            if(_Modes != null && _Modes.Contains(mode))
            {
                _Modes.Remove(mode);
                mode.ParentStack = null;
                mode.WasRemoved?.Invoke();

                //mode was successfully pushed
                for (int i = 0; i < _Modes.Count; i++)
                {
                    if(_Modes[i] != null)
                        _Modes[i].StackWasChanged?.Invoke();
                }
            }
        }
    }

    public interface IModeStack //wrapper to allow "mix-in" functionality
    {
        ModeStack ModeStack { get; }
    }

    public static class IModeStackExtension //these extensions aren't required, they just make it more intuitive to work with stacks
    {
        public static void PushMode(this IModeStack iStack, Mode newMode)
        {
            iStack.ModeStack.ValidateOwner(iStack);
            iStack.ModeStack.PushMode(newMode);
        }

        public static void RemoveMode(this IModeStack iStack, Mode mode)
        {
            iStack.ModeStack.ValidateOwner(iStack);
            iStack.ModeStack.RemoveMode(mode);
        }

        public static List<Mode> GetModes(this IModeStack iStack, params ChannelID[] channelFilter)
        {
            return iStack.ModeStack.GetModes(channelFilter);
        }
    }
}