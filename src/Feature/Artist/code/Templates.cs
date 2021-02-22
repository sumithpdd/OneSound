using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace OneSound.Feature.Artists
{
  public struct Templates
  {
    public static class Artist
    {
      public static class Fields
      {
        public static readonly ID ArtistName = new ID("{0B9D42C9-0704-45D3-AC29-0C4E65616E0A}");
        public static readonly ID ArtistTitle = new ID("{E5F6B2FB-4CCF-43D5-8238-7A247EBFF403}");
        public static readonly ID ArtistImage = new ID("{15F42F66-9EC6-44E1-8B65-39A61AE704CC}");
        public static readonly ID ArtistDescription = new ID("{D628FB26-37BD-4E8C-B8DE-99C78023B349}");
        public static readonly ID ArtistTwitterUrl = new ID("{DB84959B-20DF-4371-A5D3-77A221B3E7BC}");
        public static readonly ID ArtistLinkedInUrl = new ID("{B5A3C8E7-F756-4072-87DA-DCB716C0AA0E}");
        public static readonly ID ArtistWebsiteUrl = new ID("{9FB095EA-E113-4A5F-9F31-68DAF0C646AD}");
      }
    }

    public static class LatestAlbums
    {
      public static class Fields
      {
        public static readonly ID LatestAlbumsTitle = new ID("{136A0B0F-7DB7-41B4-9352-83F016CF2AB2}");
        public static readonly ID LatestAlbumsText = new ID("{004AD12F-26B7-4DE2-84D4-B44CFF562C7F}");
         public static readonly ID LatestAlbumsInformationText = new ID("{9735EFCA-5391-4258-959E-AFD144C0A3D3}");
        public static readonly ID LatestAlbumsList = new ID("{6F2F3C31-C65D-4F8E-A516-2477A8BF8F96}");

      }
    }
    public static class Event
    {
      public static class Fields
      {
        public static readonly ID EventName = new ID("{987747E5-0E68-485F-9BE9-01DA36FAB624}");
        public static readonly ID EventDate = new ID("{F5167D16-A419-43D9-8A18-CE16AB1E3C5D}");
        public static readonly ID IsFeaturedEvent = new ID("{BBCEA4F9-AAD6-45ED-B7E6-02E6A6AF8FCC}");
        public static readonly ID EventArtists = new ID("{3E305C26-AB5C-4DBA-9396-1E4D1FDF4A05}");
      }
    }
  }
}