﻿using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace s4t.Erp.Graos.Data.EntityConfig.Nucleo
{
    public class MotoristaConfig : EntityTypeConfiguration<Motorista>
    {
        public MotoristaConfig()
        {
            ToTable("Cadastro");
        }
    }
}