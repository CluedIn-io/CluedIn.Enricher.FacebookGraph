// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacebookGraphOrganizationVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FacebookGraphOrganizationVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Vocabularies
{
    /// <summary>The facebook graph organization vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FacebookGraphOrganizationVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookGraphOrganizationVocabulary"/> class.
        /// </summary>
        public FacebookGraphOrganizationVocabulary()
        {
            this.VocabularyName = "Facebook Graph Organization";
            this.KeyPrefix      = "facebookGraph.organization";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Organization;

            this.ImageUrl            = this.Add(new VocabularyKey("imageUrl",                   VocabularyKeyDataType.Uri));
            this.About               = this.Add(new VocabularyKey("about"));
            this.Description         = this.Add(new VocabularyKey("description"));
            this.Category            = this.Add(new VocabularyKey("category"));
            this.CategoryList        = this.Add(new VocabularyKey("categoryList",                                               VocabularyKeyVisibility.Hidden));
            this.CategoryListId      = this.Add(new VocabularyKey("categoryListId",             VocabularyKeyDataType.Number,   VocabularyKeyVisibility.Hidden));
            this.CompanyOverview     = this.Add(new VocabularyKey("companyOverview"));
            this.Impressum           = this.Add(new VocabularyKey("impressum"));
            this.Mission             = this.Add(new VocabularyKey("mission"));
            this.DisplaySubText      = this.Add(new VocabularyKey("displaySubText",                                             VocabularyKeyVisibility.Hidden));
            this.Products            = this.Add(new VocabularyKey("products"));

            this.Founded             = this.Add(new VocabularyKey("founded"));

            this.Username            = this.Add(new VocabularyKey("username"));
            this.GlobalBrandPageName = this.Add(new VocabularyKey("globalBrandPageName"));
            this.Link                = this.Add(new VocabularyKey("link"));
            this.Phone               = this.Add(new VocabularyKey("phone"));

            this.PlaceType           = this.Add(new VocabularyKey("placeType",                                                  VocabularyKeyVisibility.Hidden));
            this.Website             = this.Add(new VocabularyKey("website"));

            this.Cover               = this.Add(new VocabularyKey("cover",                      VocabularyKeyDataType.Uri));
            
            this.OverallStarRating   = this.Add(new VocabularyKey("overallStarRating"));
            this.Engagement          = this.Add(new VocabularyKey("engagement"));
            this.FanCount            = this.Add(new VocabularyKey("fanCount"));

            this.Emails              = this.Add(new VocabularyKey("emails"));

            this.IsAlwaysOpen        = this.Add(new VocabularyKey("isAlwaysOpen",               VocabularyKeyDataType.Boolean));
            this.IsPermanentlyClosed = this.Add(new VocabularyKey("isPermanentlyClosed",        VocabularyKeyDataType.Boolean));
            this.IsCommunityPage     = this.Add(new VocabularyKey("isCommunityPage",            VocabularyKeyDataType.Boolean));

            this.Street              = this.Add(new VocabularyKey("street"));
            this.City                = this.Add(new VocabularyKey("city"));
            this.Zip                 = this.Add(new VocabularyKey("zip"));
            this.Country             = this.Add(new VocabularyKey("country"));

            this.Latitude            = this.Add(new VocabularyKey("latitude",                   VocabularyKeyDataType.GeographyCoordinates));
            this.Longitude           = this.Add(new VocabularyKey("longitude",                  VocabularyKeyDataType.GeographyCoordinates));


            this.SocialFacebook      = this.Add(new VocabularyKey("socialFacebook"));
            this.Bio                 = this.Add(new VocabularyKey("bio"));
            this.Birthday            = this.Add(new VocabularyKey("birthday"));
            this.SingleLineAddress   = this.Add(new VocabularyKey("singleLineAddress"));

            this.AddMapping(this.SingleLineAddress, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Address);
            this.AddMapping(this.Street, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressStreetName);
            this.AddMapping(this.City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
            this.AddMapping(this.Zip, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
            this.AddMapping(this.Country, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCountryCode);

            this.AddMapping(this.Emails, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.ContactEmail);
            this.AddMapping(this.Phone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.PhoneNumber);
            this.AddMapping(this.Website, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);
            this.AddMapping(this.Founded, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.FoundingDate);

            this.AddMapping(this.Link, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Facebook);
            this.AddMapping(this.SocialFacebook, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Facebook);
        }

        public VocabularyKey ImageUrl { get; protected set; }
        public VocabularyKey Description { get; set; }
        public VocabularyKey About { get; set; }
        public VocabularyKey Category { get; set; }
        public VocabularyKey CompanyOverview { get; set; }
        public VocabularyKey DisplaySubText { get; set; }
        public VocabularyKey Founded { get; set; }
        public VocabularyKey GlobalBrandPageName { get; set; }
        public VocabularyKey Impressum { get; set; }
        public VocabularyKey Link { get; set; }
        public VocabularyKey Mission { get; set; }
        public VocabularyKey Phone { get; set; }
        public VocabularyKey PlaceType { get; set; }
        public VocabularyKey Products { get; set; }
        public VocabularyKey Username { get; set; }
        public VocabularyKey Website { get; set; }
        public VocabularyKey CategoryList { get; set; }
        public VocabularyKey Cover { get; set; }
        public VocabularyKey Engagement { get; set; }
        public VocabularyKey Emails { get; set; }
        public VocabularyKey FanCount { get; set; }
        public VocabularyKey IsAlwaysOpen { get; set; }
        public VocabularyKey IsCommunityPage { get; set; }
        public VocabularyKey IsPermanentlyClosed { get; set; }
        public VocabularyKey OverallStarRating { get; set; }
        public VocabularyKey CategoryListId { get; set; }
        public VocabularyKey City { get; set; }
        public VocabularyKey Country { get; set; }
        public VocabularyKey Street { get; set; }
        public VocabularyKey Zip { get; set; }
        public VocabularyKey Latitude { get; set; }
        public VocabularyKey Longitude { get; set; }

        public VocabularyKey SocialFacebook { get; set; }

        public VocabularyKey Bio { get; set; }

        public VocabularyKey Birthday { get; set; }

        public VocabularyKey SingleLineAddress { get; set; }
    }
}
