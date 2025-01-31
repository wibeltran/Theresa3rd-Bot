﻿using TheresaBot.Main.Helper;
using TheresaBot.Main.Type;

namespace TheresaBot.Main.Model.Config
{
    public class PixivConfig : BaseConfig
    {
        public bool FreeProxy { get; private set; }
        public string HttpProxy { get; private set; }
        public string ImgProxy { get; private set; }
        public int ImgShowMaximum { get; private set; } = 1;
        public int TagShowMaximum { get; private set; } = 5;
        public int UrlShowMaximum { get; private set; } = 3;
        public string ImgSize { get; private set; } = "thumb";
        public ResendType ImgResend { get; private set; } = ResendType.WithoutImg;
        public float R18ImgBlur { get; private set; } = 10;
        public string OriginUrlProxy { get; private set; }
        public bool SendImgBehind { get; private set; }
        public int ImgRetryTimes { get; private set; }
        public int ErrRetryTimes { get; private set; }
        public int CookieExpire { get; private set; }
        public string CookieExpireMsg { get; private set; }
        public string Template { get; private set; }
        public double GeneralTarget { get; set; } = 1.0;
        public double AITarget { get; set; } = 0.5;
        public double R18Target { get; set; } = 1.2;

        public override PixivConfig FormatConfig()
        {
            this.ImgProxy = StringHelper.FormatHttpUrl(ImgProxy);
            this.HttpProxy = StringHelper.FormatHttpUrl(HttpProxy, false);
            this.OriginUrlProxy = StringHelper.FormatHttpUrl(OriginUrlProxy);
            if (this.R18ImgBlur < 5) this.R18ImgBlur = 5;
            if (this.R18ImgBlur > 100) this.R18ImgBlur = 100;
            return this;
        }

    }
}
