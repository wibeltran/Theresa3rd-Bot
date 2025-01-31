﻿using TheresaBot.Main.Common;

namespace TheresaBot.Main.Helper
{
    public static class PermissionsHelper
    {
        /// <summary>
        /// 判断是否存在其中一个群需要显示AI图
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public static bool IsShowAISetu(this List<long> groupIds)
        {
            return groupIds.Any(o => o.IsShowAISetu());
        }

        /// <summary>
        /// 判断某一个群是否需要显示AI图
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool IsShowAISetu(this long groupId)
        {
            if (BotConfig.PermissionsConfig?.SetuShowAIGroups is null) return true;
            return BotConfig.PermissionsConfig.SetuShowAIGroups.Contains(groupId);
        }

        /// <summary>
        /// 判断某一个群是否可以显示图片
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="isR18Img"></param>
        /// <returns></returns>
        public static bool IsShowSetuImg(this long groupId, bool isR18Img)
        {
            List<long> ShowImgGroups = BotConfig.PermissionsConfig?.SetuShowImgGroups ?? new();
            if (ShowImgGroups.Contains(groupId) == false) return false;
            if (isR18Img && groupId.IsShowR18SetuImg() == false) return false;
            return true;
        }

        /// <summary>
        /// 判断是否存在其中一个群可以显示R18图片
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public static bool IsShowR18SetuImg(this List<long> groupIds)
        {
            return groupIds.Any(o => o.IsShowR18SetuImg());
        }

        /// <summary>
        /// 判断某一个群是否可以显示R18图片
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool IsShowR18SetuImg(this long groupId)
        {
            List<long> ShowR18Groups = BotConfig.PermissionsConfig?.SetuShowR18Groups ?? new();
            List<long> ShowR18ImgGroups = BotConfig.PermissionsConfig?.SetuShowR18ImgGroups ?? new();
            if (ShowR18Groups.Contains(groupId) == false) return false;
            if (ShowR18ImgGroups.Contains(groupId) == false) return false;
            return true;
        }

        /// <summary>
        /// 判断是否存在其中一个群可以显示R18内容
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public static bool IsShowR18Setu(this List<long> groupIds)
        {
            return groupIds.Any(o => o.IsShowR18Setu());
        }

        /// <summary>
        /// 判断某一个群是否可以显示R18内容
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool IsShowR18Setu(this long groupId)
        {
            if (BotConfig.PermissionsConfig?.SetuShowR18Groups is null) return false;
            return BotConfig.PermissionsConfig.SetuShowR18Groups.Contains(groupId);
        }

        /// <summary>
        /// 判断某一个群是否可以显示R18的Saucenao的搜索结果
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool IsShowR18Saucenao(this long groupId)
        {
            if (BotConfig.PermissionsConfig?.SaucenaoR18Groups is null) return false;
            return BotConfig.PermissionsConfig.SaucenaoR18Groups.Contains(groupId);
        }

        /// <summary>
        /// 判断某一个成员是否被设置为超级管理员
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static bool IsSuperManager(this long memberId)
        {
            if (BotConfig.PermissionsConfig?.SuperManagers is null) return false;
            return BotConfig.PermissionsConfig.SuperManagers.Contains(memberId);
        }

    }
}
