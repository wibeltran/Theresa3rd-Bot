﻿using TheresaBot.Main.Model.Content;
using TheresaBot.Main.Result;
using TheresaBot.Main.Type;

namespace TheresaBot.Main.Session
{
    public abstract class BaseSession
    {
        public abstract PlatformType PlatformType { get; }

        public abstract Task<BaseResult> SendGroupMessageAsync(long groupId, string message);

        public abstract Task<BaseResult> SendGroupMessageAsync(long groupId, List<BaseContent> contents, List<long> atMembers = null, bool isAtAll = false);

        public abstract Task<BaseResult> SendGroupMessageWithAtAsync(long groupId, long memberId, string message);

        public abstract Task<BaseResult> SendGroupMessageWithAtAsync(long groupId, long memberId, List<BaseContent> contents);

        public abstract Task<BaseResult> SendGroupMessageWithQuoteAsync(long groupId, long memberId, long quoteMsgId, string message);

        public abstract Task<BaseResult> SendGroupMessageWithQuoteAsync(long groupId, long memberId, long quoteMsgId, List<BaseContent> contents);

        public abstract Task<BaseResult> SendGroupMergeAsync(long groupId, List<BaseContent[]> contentLists);

        public abstract Task<BaseResult> SendFriendMessageAsync(long memberId, string message);

        public abstract Task<BaseResult> SendFriendMessageAsync(long memberId, List<BaseContent> contents);

        public abstract Task<BaseResult> SendTempMessageAsync(long groupId, long memberId, string message);

        public abstract Task<BaseResult> SendTempMessageAsync(long groupId, long memberId, List<BaseContent> contents);

        public abstract Task RevokeGroupMessageAsync(long groupId, long messageId);

    }
}
