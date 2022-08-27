using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class SuggestionAllDTO
    {
      public Dictionary<Guid, int> Suggested { get; set; }
    }
}
