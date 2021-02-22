using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using OneSound.Feature.News.Models;

namespace OneSound.Feature.News.Controllers
{
    public class NewsController : Controller
  {
    public ActionResult NewsDetails()
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

      NewsDetail newsDetail = new NewsDetail();

       
      // Artists List - TreeList Field
      MultilistField newsArtistsListField = item.Fields[Templates.News.Fields.NewsArtists];
      Item[] newsArtistsItems = newsArtistsListField.GetItems();
      List<Artist> newsArtistsList = new List<Artist>();
      if (newsArtistsItems != null && newsArtistsItems.Count() > 0)
      {
        foreach (Item i in newsArtistsItems)
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

          newsArtistsList.Add(artist);
        }
      }

      newsDetail.NewsArtistList = newsArtistsList;

      return View(newsDetail);
    }
     
     
  }
}