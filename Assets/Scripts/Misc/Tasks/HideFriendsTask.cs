using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HideFriendsTask : Task
{
    public List<FriendModel> allFriends { get; private set; } = new List<FriendModel>();
    public int friendsNotHidden { get; private set; } = 0;

    public override void CalculateTaskStatus()
    {
        base.CalculateTaskStatus();

        friendsNotHidden = 0;
        foreach (var friendModel in allFriends)
        {
            if (!friendModel.isHidden)
            {
                friendsNotHidden += 1;
            }
        }
    }

    public void AddFriend(FriendModel friendModel)
    {
        allFriends.Add(friendModel);
    }
}
