using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModeMachine;

/*
public class TestStack : IModeStack
{
    public ModeStack ModeStack => throw new NotImplementedException();

    void Asdf()
    {
        this.PushMode(new TestMode());
    }

    public void DoThing()
    { }
}

public class TestMode : Mode<TestStack>
{

    void Asdfasdf()
    {
        SetChannel(UserChannels.InputFocus, true);
        SetChannel(UserChannels.TimeController, true);
        ParentStack.DoThing();
        GetDepth(UserChannels.InputFocus, UserChannels.TimeController);
    }
}

public static class UserChannels
{
    public static readonly ChannelID InputFocus = new ChannelID();
    public static readonly ChannelID TimeController = new ChannelID();
}*/