﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassPack.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }

        public override string? ToString()
        {
            return Title;
        }
    }
}
