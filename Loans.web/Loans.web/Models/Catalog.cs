﻿using Loans.BL.BaseDtos;

namespace Loans.web.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public int TypeCatalogId { get; set; }
        public string Name { get; set; }
    }
}
