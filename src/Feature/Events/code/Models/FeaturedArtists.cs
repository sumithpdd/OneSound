using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSound.Feature.Events.Models
{
  public class FeaturedArtists
  {
    public Artist FeaturedArtist { get; set; }
    public List<Artist> FeaturedArtistsList { get; set; }
  }
}