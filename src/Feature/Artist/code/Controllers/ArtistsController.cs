using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Diagnostics;
using OneSound.Feature.Artists.Models;
using OneSound.Feature.Artists.Helpers;
using System.Xml;

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

      //Event Links - DropTree Field with Child Items
      //ReferenceField artistRoot = item.Fields[Templates.LatestAlbums.Fields.LatestAlbumsList];
      List<Artist> artistAlbumList = new List<Artist>();

      MultilistField latestAlbumsListField = item.Fields[Templates.LatestAlbums.Fields.LatestAlbumsList];
      Item[] latestAlbumArtistItems = latestAlbumsListField.GetItems();

      //foreach (Item i in artistRoot.TargetItem.Children)
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

          XmlDocument doc = new XmlDocument();
          //doc.LoadXml("<image stylelabs-content-id=\"29865\" thumbnailsrc=\"https://cmp-connector-400-r147.stylelabsqa.com/api/gateway/29865/thumbnail\" src=\"https://cmp-connector-400-r147.stylelabsqa.com/api/public/content/19c8d2004811433d931cc13c1d0b138a?v=638d2b04\" mediaid=\"\" stylelabs-content-type=\"Image\" alt=\"can-of-coke.jpg\" height=\"879\" width=\"1200\" />");
          doc.LoadXml(artistImage.Value);

          XmlElement root = doc.DocumentElement;

          string src = root.Attributes["src"] !=null? root.Attributes["src"].Value: string.Empty;

          if (!string.IsNullOrEmpty(src))
          {
            artist.ArtistImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(artistImage.MediaItem);
          }
          else
          {
            artist.ArtistImageUrl = src;

          }

          artist.ArtistImageAlt = artistImage.Alt;

          artistAlbumList.Add(artist);
        }
      }

      //Featured Artists List - TreeList Field

      //MultilistField latestAlbumsListField = item.Fields[Templates.LatestAlbums.Fields.LatestAlbumsList];
      //Item[] latestAlbumArtistItems = latestAlbumsListField.GetItems();
      // if (latestAlbumArtistItems != null && latestAlbumArtistItems.Count() > 0)
      //{
      //  foreach (Item i in latestAlbumArtistItems)
      //  {
      //    Artist artist = new Artist();
      //    Item artistItem = Sitecore.Context.Database.GetItem(i.ID);
      //    artist.ArtistName = artistItem.Fields[Templates.Artist.Fields.ArtistName.ToString()].Value;
      //    artist.ArtistTitle = artistItem.Fields[Templates.Artist.Fields.ArtistTitle.ToString()].Value;
      //    artist.ArtistDescription = artistItem.Fields[Templates.Artist.Fields.ArtistDescription.ToString()].Value;
      //    LinkField artistTwitterLink = artistItem.Fields[Templates.Artist.Fields.ArtistTwitterUrl];
      //    artist.ArtistTwitterUrl = artistTwitterLink.Url;
      //    LinkField artistLinkedInLink = artistItem.Fields[Templates.Artist.Fields.ArtistLinkedInUrl];
      //    artist.ArtistLinkedInUrl = artistLinkedInLink.Url;
      //    LinkField artistWebsiteLink = artistItem.Fields[Templates.Artist.Fields.ArtistWebsiteUrl];
      //    artist.ArtistWebsiteUrl = artistWebsiteLink.Url;
      //    ImageField artistImage = artistItem.Fields[Templates.Artist.Fields.ArtistImage];
      //    artist.ArtistImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(artistImage.MediaItem);
      //    artist.ArtistImageAlt = artistImage.Alt;

      //    artistAlbumList.Add(artist);
      //  }
      //}

      latestAlbums.LatestAlbumsList = artistAlbumList;

      return View(latestAlbums);
    }
     
     
  }
}