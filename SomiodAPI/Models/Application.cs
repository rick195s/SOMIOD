﻿using System;

namespace SomiodAPI
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
        public string Res_type { get; set; } = "application";
    }
}