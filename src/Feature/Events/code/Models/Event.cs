using System;
using System.Collections.Generic;

namespace OneSound.Feature.Events.Models
{
  public class Event
  {
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public string EventDateString { get; set; }
    public string EventTimeString { get; set; }
    public bool IsFeaturedEvent { get; set; }
    public List<Artist> EventArtists { get; set; }
  }
}