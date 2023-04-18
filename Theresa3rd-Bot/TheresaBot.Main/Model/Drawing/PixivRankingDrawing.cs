﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheresaBot.Main.Helper;
using TheresaBot.Main.Model.Pixiv;

namespace TheresaBot.Main.Model.Drawing
{
    public class PixivRankingDrawing : BasePixivDrawing
    {
        public PixivRankingDetail RankingDetail { get; set; }
        public override string PixivId => RankingDetail.WorkInfo.PixivId;
        public override string ImageHttpUrl => RankingDetail.RankingContent.url;
        public override string ImageSavePath { get; protected set; }

        public PixivRankingDrawing(PixivRankingDetail rankingDetail)
        {
            this.RankingDetail = rankingDetail;
            this.ImageSavePath = rankingDetail.RankingContent.url.GetPreviewImgSaveName(PixivId);
        }

    }
}
