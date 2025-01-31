﻿using TheresaBot.Main.Common;
using TheresaBot.Main.Handler;
using TheresaBot.Main.Helper;
using TheresaBot.Main.Reporter;
using TheresaBot.Main.Session;

namespace TheresaBot.Main.Timers
{
    internal static class MysUserTimer
    {
        private static BaseSession Session;
        private static BaseReporter Reporter;
        private static System.Timers.Timer SystemTimer;

        public static void init(BaseSession session, BaseReporter reporter)
        {
            Session = session;
            Reporter = reporter;
            SystemTimer = new System.Timers.Timer();
            SystemTimer.Interval = BotConfig.SubscribeConfig.Miyoushe.ScanInterval * 1000;
            SystemTimer.AutoReset = true;
            SystemTimer.Elapsed += new System.Timers.ElapsedEventHandler(HandlerMethod);
            SystemTimer.Enabled = true;
        }

        private static void HandlerMethod(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SystemTimer.Enabled = false;
                new MiyousheHandler(Session, Reporter).HandleSubscribeAsync().Wait();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "MysUserTimer异常");
                Reporter.SendError(ex, "MysUserTimer异常").Wait();
            }
            finally
            {
                SystemTimer.Enabled = true;
            }
        }





    }
}
