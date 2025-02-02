﻿using Streetcode.DAL.Entities.InfoBlocks;

namespace Streetcode.BLL.Dto.InfoBlocks.Articles
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public InfoBlock? InfoBlock { get; set; }
    }
}
