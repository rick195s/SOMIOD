﻿using System;
using System.Collections.Generic;

namespace SomiodAPI
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
        public int Parent { get; set; }
        public string Res_type { get; set; } = "module";
        public List<Data> data { get; set; } = new List<Data>();
    }
}