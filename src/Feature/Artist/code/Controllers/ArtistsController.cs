using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Diagnostics;
using OneSound.Feature.Artists.Models;
using OneSound.Feature.Artists.Helpers;

namespace OneSound.Feature.Artists.Controllers
{
    public class ArtistsController : Controller
  {
    public ActionResult LatestAlbums()
    {
      if (Sitecore.Context.Item == null)
      {
        return null;
      }

      var dataSourceId = Sitecore.Context.Item.ID.ToString();
      Assert.IsNotNullOrEmpty(dataSourceId, "dataSourceId is null or empty");
      var item = Sitecore.Context.Database.GetItem(dataSourceId);
      if (item == null)
      {
        return null;
      }

      LatestAlbums latestAlbums = new LatestAlbums();

       
      //Featured Artists List - TreeList Field
      MultilistField latestAlbumsListField = item.Fields[Templates.LatestAlbums.Fields.LatestAlbumsList];
      Item[] latestAlbumArtistItems = latestAlbumsListField.GetItems();
      List<Artist> artistAlbumList = new List<Artist>();
      if (latestAlbumArtistItems != null && latestAlbumArtistItems.Count() > 0)
      {
        foreach (Item i in latestAlbumArtistItems)
        {
          Artist artist = new Artist();
          Item artistItem = Sitecore.Context.Database.GetItem(i.ID);
          artist.ArtistName = artistItem.Fields[Templates.Artist.Fields.ArtistName.ToString()].Value;
          artist.ArtistTitle = artistItem.Fields[Templates.Artist.Fields.ArtistTitle.ToString()].Value;
          artist.ArtistDescription = artistItem.Fields[Templates.Artist.Fields.ArtistDescription.ToString()].Value;
          LinkField artistTwitterLink = artistItem.Fields[Templates.Artist.Fields.ArtistTwitterUrl];
          artist.ArtistTwitterUrl = artistTwitterLink.Url;
          LinkField artistLinkedInLink = artistItem.Fields[Templates.Artist.Fields.ArtistLinkedInUrl];
          artist.ArtistLinkedInUrl = artistLinkedInLink.Url;
          LinkField artistWebsiteLink = artistItem.Fields[Templates.Artist.Fields.ArtistWebsiteUrl];
          artist.ArtistWebsiteUrl = artistWebsiteLink.Url;
          ImageField artistImage = artistItem.Fields[Templates.Artist.Fields.ArtistImage];
          artist.ArtistImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(artistImage.MediaItem);
          artist.ArtistImageAlt = artistImage.Alt;

          artistAlbumList.Add(artist);
        }
      }

      latestAlbums.LatestAlbumsList = artistAlbumList;

      return View(latestAlbums);
    }
     
     
  }
}