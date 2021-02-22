using OneSound.Feature.Events.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSound.Feature.Events.Helpers
{
  public class EventHelpers
  {
    public List<Event> PopulateEventList(MultilistField eventListField)
    {
      Item[] eventItems = eventListField.GetItems();
      List<Event> eventList = new List<Event>();
      if (eventItems != null && eventItems.Count() > 0)
      {
        foreach (Item ev in eventItems)
        {
          Event e = new Event();
          Item eventItem = Sitecore.Context.Database.GetItem(ev.ID);
          e.EventName = eventItem.Fields[Templates.Event.Fields.EventName.ToString()].Value;
          CheckboxField isFeaturedEventField = eventItem.Fields[Templates.Event.Fields.IsFeaturedEvent];
          e.IsFeaturedEvent = isFeaturedEventField.Checked;
          DateField eventDateField = eventItem.Fields[Templates.Event.Fields.EventDate];
          e.EventDate = eventDateField.DateTime.ToLocalTime();
          e.EventDateString = eventDateField.DateTime.ToLocalTime().ToString("f");
          e.EventTimeString = eventDateField.DateTime.ToLocalTime().ToString("t");

          //TreelistEx
          MultilistField eventArtistsField = eventItem.Fields[Templates.Event.Fields.EventArtists];
          Item[] eventArtistItems = eventArtistsField.GetItems();
          List<Artist> eventArtistList = new List<Artist>();
          if (eventArtistItems != null && eventArtistItems.Count() > 0)
          {
            foreach (Item sp in eventArtistItems)
            {
              Artist artist = new Artist();
              Item artistItem = Sitecore.Context.Database.GetItem(sp.ID);
              artist.ArtistName = artistItem.Fields[Templates.Artist.Fields.ArtistName.ToString()].Value;
              ImageField artistImage = artistItem.Fields[Templates.Artist.Fields.ArtistImage];
              artist.ArtistImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(artistImage.MediaItem);
              artist.ArtistImageAlt = artistImage.Alt;

              eventArtistList.Add(artist);
            }

            e.EventArtists = eventArtistList;
          }

          eventList.Add(e);
        }
      }

      return eventList;
    }

    public Artist GetFeaturedArtist(Item item)
    {
      Artist featuredArtist = new Artist();

      Item featuredArtistItem = Sitecore.Context.Database.GetItem(item.ID);
      if (featuredArtistItem != null)
      {
        LinkField featuredArtistTwitterLink = featuredArtistItem.Fields[Templates.Artist.Fields.ArtistTwitterUrl];
        featuredArtist.ArtistTwitterUrl = featuredArtistTwitterLink.Url;
        LinkField featuredArtistLinkedInLink = featuredArtistItem.Fields[Templates.Artist.Fields.ArtistLinkedInUrl];
        featuredArtist.ArtistLinkedInUrl = featuredArtistLinkedInLink.Url;
        LinkField featuredArtistWebsiteLink = featuredArtistItem.Fields[Templates.Artist.Fields.ArtistWebsiteUrl];
        featuredArtist.ArtistWebsiteUrl = featuredArtistWebsiteLink.Url;
      }

      return featuredArtist;
    }
  }
}
