﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorHyperLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? URL { get; set; }
    }
}
