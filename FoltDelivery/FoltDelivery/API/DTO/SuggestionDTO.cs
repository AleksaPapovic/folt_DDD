﻿using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class SuggestionDTO
    {
        public List<ProductDTO> SuggestedProducts { get; set; }

        public SuggestionDTO()
        {
            SuggestedProducts = new List<ProductDTO>();
        }
    }
}
