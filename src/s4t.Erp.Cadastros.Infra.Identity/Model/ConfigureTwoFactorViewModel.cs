﻿using System.Collections.Generic;

namespace s4t.Erp.Cadastros.Infra.Identity.Model
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}