﻿using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes
{
    public class AuthorShipDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int AuthorShipHyperLinkId { get; set; }
        public AuthorShipHyperLink? AuthorShipHyperLink { get; set; }
    }
}
