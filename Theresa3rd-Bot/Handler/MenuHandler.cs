﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Theresa3rd_Bot.BotPlatform.Base.Command;
using Theresa3rd_Bot.Common;
using Theresa3rd_Bot.Model.Content;
using Theresa3rd_Bot.Type;
using Theresa3rd_Bot.Util;

namespace Theresa3rd_Bot.Handler
{
    public class MenuHandler : BaseHandler
    {
        public async Task sendMenuAsync(GroupCommand groupCommand)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(BotConfig.MenuConfig?.Template) == false)
                {
                    List<ChatContent> templateList = BotConfig.MenuConfig.Template.SplitToChainAsync(SendTarget.Group);
                    await BotCommand.ReplyGroupMessageWithAtAsync(command.Args, templateList);
                    return;
                }

                await command.SendGroupMessageAsync(command.Args, getMemberMenu());

                if (command.MemberId.IsSuperManager())
                {
                    await Task.Delay(1000);
                    await command.SendGroupMessageAsync(command.Args, getManagerMenu());
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "菜单发送失败");
                ReportHelper.SendError(ex, "菜单发送失败");
            }
        }

        private string getMemberMenu()
        {
            string prefix = BotConfig.GeneralConfig.Prefix;
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine($"目前实现的功能如下：");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SetuConfig.Pixiv.Command}[标签/pid]?】 从pixiv中搜索一张涩图");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SetuConfig.Lolicon.Command}[标签]?】 从Lolicon中搜索一张涩图");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SetuConfig.Lolisuki.Command}[标签]?】 从Lolisuki中搜索一张涩图");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SaucenaoConfig.Command}[图片]?】 尝试用Saucenao查找来源，并返回原图等信息");
            menuBuilder.AppendLine($"使用实例请参考：https://github.com/GardenHamster/Theresa3rd-Bot/blob/main/Menu.md");
            return menuBuilder.ToString();
        }

        private string getManagerMenu()
        {
            string prefix = BotConfig.GeneralConfig.Prefix;
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine($"超级管理员的功能如下：");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.Mihoyo.AddCommand}】 订阅米游社用户");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.Mihoyo.RmCommand}】 退订米游社用户");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.PixivUser.AddCommand}】 订阅P站画师");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.PixivUser.SyncCommand}】 订阅所有P站已关注的画师");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.PixivUser.RmCommand}】 退订P站画师");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.PixivTag.AddCommand}】 订阅P站标签");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.SubscribeConfig.PixivTag.RmCommand}】 退订P站标签");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.ManageConfig.DisableMemberCommand}】 拉黑一个成员，不处理该成员的指令");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.ManageConfig.EnableMemberCommand}】 解禁一个成员，允许该成员使用指令");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.ManageConfig.DisableTagCommand}】 禁止搜索一个pixiv标签");
            menuBuilder.AppendLine($"【{prefix}{BotConfig.ManageConfig.EnableTagCommand}】 允许搜索一个pixiv标签");
            return menuBuilder.ToString();
        }




    }
}
