﻿using Microsoft.Extensions.Configuration;
using Nacos.AspNetCore.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;

namespace TinyFx.Configuration
{
    public class NacosSection : NacosAspNetOptions
    {
        public bool Enabled { get; set; }

        public void Bind(IConfiguration configuration)
        {
            configuration?.Bind(this);
        }
    }
}
