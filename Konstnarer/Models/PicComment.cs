﻿using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{
    public class PicComment
    {
        [Key]
        public int Id { get; set; }
        public int PictureId { get; set; }
        public Guid UserId { get; set; }//den som ger commentaren
        [StringLength(250)]
        public string Comment { get; set; }
        public DateTime Date { get; set; }

    }
}
