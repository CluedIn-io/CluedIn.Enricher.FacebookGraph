// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacebookGraphExternalSearchProvider.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FacebookGraphExternalSearchProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.ExternalSearch;
using CluedIn.Crawling.Helpers;
using CluedIn.ExternalSearch.Filters;
using CluedIn.ExternalSearch.Providers.FacebookGraph.Model;
using CluedIn.ExternalSearch.Providers.FacebookGraph.Vocabularies;
using RestSharp;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph
{
    /// <summary>The facebook graph external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class FacebookGraphExternalSearchProvider : ExternalSearchProviderBase
    {
        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/
        public FacebookGraphExternalSearchProvider()
            : base(Constants.ExternalSearchProviders.FacebookGraphId, EntityType.Organization)
        {
            var nameBasedTokenProvider = new NameBasedTokenProvider("FacebookGraph");

            if (nameBasedTokenProvider.ApiToken != null)
                this.TokenProvider = new RoundRobinTokenProvider(nameBasedTokenProvider.ApiToken.Split(',', ';'));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookGraphExternalSearchProvider" /> class.
        /// </summary>
        public FacebookGraphExternalSearchProvider([NotNull] IExternalSearchTokenProvider tokenProvider)
            : base(Constants.ExternalSearchProviders.FacebookGraphId, EntityType.Organization)
        {
            this.TokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <inheritdoc/>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<FacebookResponse>(this).ToList();

            Func<string, bool> nameFilter = value => OrganizationFilters.NameFilter(context, value) || existingResults.Any(r => string.Equals(r.Data.name, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType       = request.EntityMetaData.EntityType;
            var organizationName = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName, new HashSet<string>());
            var facebookUrl      = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Facebook, new HashSet<string>());

            if (!string.IsNullOrEmpty(request.EntityMetaData.Name))
                organizationName.Add(request.EntityMetaData.Name);
            if (!string.IsNullOrEmpty(request.EntityMetaData.DisplayName))
                organizationName.Add(request.EntityMetaData.DisplayName);

            var namesFromUrls = new HashSet<string>();

            foreach (var possibleUrl in facebookUrl)
            {
                if (Uri.IsWellFormedUriString(possibleUrl, UriKind.Absolute))
                {
                    if (Uri.TryCreate(possibleUrl, UriKind.Absolute, out var facebook))
                    {
                        var lastSegment = facebook.Segments.Last();
                        lastSegment = Regex.Replace(lastSegment, "/+$", string.Empty);

                        if (!string.IsNullOrEmpty(lastSegment))
                            namesFromUrls.Add(lastSegment);
                    }
                }
                else
                {
                    namesFromUrls.Add(possibleUrl);
                }
            }

            if (namesFromUrls.Any())
            {
                var values = organizationName.Where(n => n != null);

                foreach (var value in values.Where(v => !nameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
            else if (organizationName.Any())
            {
                var values = organizationName.Where(n => n != null)
                                             .GetOrganizationNameVariants()
                                             .Select(NameNormalization.Normalize)
                                             .GetFacebookNameVariants()
                                             .ToHashSet();

                foreach (var value in values.Where(v => !nameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var name = query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();
            
            if (string.IsNullOrEmpty(name))
                yield break;

            if (string.IsNullOrEmpty(this.TokenProvider.ApiToken))
                throw new InvalidOperationException("FacebookGraph ApiToken have not been configured");

            name = HttpUtility.UrlEncode(name);

            var client = new RestClient("https://graph.facebook.com/v3.2/");

            var request = new RestRequest(string.Format("{0}?access_token={1}&fields=about,name,affiliation,app_id,app_links,artists_we_like,attire,awards,band_interests,band_members,best_page,bio,birthday,booking_agent,built,category,category_list,contact_address,cover,culinary_team,current_location,description,description_html,directed_by,display_subtext,emails,engagement,fan_count,featured_video,features,food_styles,founded,general_info,general_manager,genre,global_brand_page_name,global_brand_root_id,hometown,hours,impressum,influences,is_always_open,is_community_page,is_permanently_closed,link,location,mission,overall_star_rating,parent_page,parking,payment_options,personal_info,personal_interests,phone,place_type,press_contact,products,public_transit,username,voip_info,website,is_chain,has_whatsapp_number,whatsapp_number,price_range,privacy_info_url,id,rating_count,store_code,store_location_descriptor,store_number,studio,name_with_location_descriptor,picture{{url}},verification_status,company_overview,is_owned", 
                                                        name, 
                                                        this.TokenProvider.ApiToken), 
                                          Method.GET);

            var response = client.ExecuteTaskAsync<FacebookResponse>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    yield return new ExternalSearchQueryResult<FacebookResponse>(query, response.Data);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                yield break;
            else if (response.ErrorException != null)
                throw new AggregateException(response.ErrorException.Message, response.ErrorException);
            else
                throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode + "; Content: " + response.Content);
        }

        /// <inheritdoc/>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<FacebookResponse>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);

            this.PopulateMetadata(clue.Data.EntityData, resultItem);
            this.DownloadPreviewImage(context, resultItem.Data.picture?.data?.url, clue);

            return new[] { clue };
        }

        /// <inheritdoc/>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<FacebookResponse>();
            return this.CreateMetadata(resultItem);
        }

        /// <inheritdoc/>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return this.DownloadPreviewImageBlob<FacebookResponse>(context, result, r => r.Data.picture?.data?.url);
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<FacebookResponse> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<FacebookResponse> resultItem)
        {
            return new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Data.id);
        }

        /// <summary>Gets the code origin.</summary>
        /// <returns>The code origin.</returns>
        private CodeOrigin GetCodeOrigin()
        {
            return CodeOrigin.CluedIn.CreateSpecific("facebookGraph");
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<FacebookResponse> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType       = EntityType.Organization;
            metadata.Name             = resultItem.Data.name;
            metadata.Description      = resultItem.Data.description;
            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);

            metadata.Properties[FacebookGraphVocabulary.Organization.About]               = resultItem.Data.about;
            metadata.Properties[FacebookGraphVocabulary.Organization.Category]            = resultItem.Data.category;
            metadata.Properties[FacebookGraphVocabulary.Organization.CompanyOverview]     = resultItem.Data.company_overview;
            metadata.Properties[FacebookGraphVocabulary.Organization.DisplaySubText]      = resultItem.Data.display_subtext;
            metadata.Properties[FacebookGraphVocabulary.Organization.Founded]             = resultItem.Data.founded;
            metadata.Properties[FacebookGraphVocabulary.Organization.GlobalBrandPageName] = resultItem.Data.global_brand_page_name;
            metadata.Properties[FacebookGraphVocabulary.Organization.Impressum]           = resultItem.Data.impressum;
            metadata.Properties[FacebookGraphVocabulary.Organization.Link]                = resultItem.Data.link;
            metadata.Properties[FacebookGraphVocabulary.Organization.Mission]             = resultItem.Data.mission;
            metadata.Properties[FacebookGraphVocabulary.Organization.Phone]               = resultItem.Data.phone;
            metadata.Properties[FacebookGraphVocabulary.Organization.PlaceType]           = resultItem.Data.place_type;
            metadata.Properties[FacebookGraphVocabulary.Organization.Products]            = resultItem.Data.products;
            metadata.Properties[FacebookGraphVocabulary.Organization.Username]            = resultItem.Data.username;
            metadata.Properties[FacebookGraphVocabulary.Organization.Website]             = resultItem.Data.website;
            metadata.Properties[FacebookGraphVocabulary.Organization.CategoryList]        = resultItem.Data.category_list.PrintIfAvailable(v => v.Select(s => s.name), v => string.Join(",", v));
            metadata.Properties[FacebookGraphVocabulary.Organization.CategoryListId]      = resultItem.Data.category_list.PrintIfAvailable(v => v.Select(s => s.id), v => string.Join(",", v));
            metadata.Properties[FacebookGraphVocabulary.Organization.Cover]               = resultItem.Data.cover.PrintIfAvailable(v => v.source);
            metadata.Properties[FacebookGraphVocabulary.Organization.Engagement]          = resultItem.Data.engagement.PrintIfAvailable(v => v.count);
            metadata.Properties[FacebookGraphVocabulary.Organization.Emails]              = resultItem.Data.emails.PrintIfAvailable(v => v, v => string.Join(",", v));
            metadata.Properties[FacebookGraphVocabulary.Organization.FanCount]            = resultItem.Data.fan_count.PrintIfAvailable();
            metadata.Properties[FacebookGraphVocabulary.Organization.IsAlwaysOpen]        = resultItem.Data.is_always_open.PrintIfAvailable();
            metadata.Properties[FacebookGraphVocabulary.Organization.IsCommunityPage]     = resultItem.Data.is_community_page.PrintIfAvailable();
            metadata.Properties[FacebookGraphVocabulary.Organization.IsPermanentlyClosed] = resultItem.Data.is_permanently_closed.PrintIfAvailable();

            metadata.Properties[FacebookGraphVocabulary.Organization.City]                = resultItem.Data.location.PrintIfAvailable(v => v.city);
            metadata.Properties[FacebookGraphVocabulary.Organization.Country]             = resultItem.Data.location.PrintIfAvailable(v => v.country);
            metadata.Properties[FacebookGraphVocabulary.Organization.Street]              = resultItem.Data.location.PrintIfAvailable(v => v.street);
            metadata.Properties[FacebookGraphVocabulary.Organization.Zip]                 = resultItem.Data.location.PrintIfAvailable(v => v.zip);
            metadata.Properties[FacebookGraphVocabulary.Organization.Latitude]            = resultItem.Data.location.PrintIfAvailable(v => v.latitude);
            metadata.Properties[FacebookGraphVocabulary.Organization.Longitude]           = resultItem.Data.location.PrintIfAvailable(v => v.longitude);

            metadata.Properties[FacebookGraphVocabulary.Organization.OverallStarRating]   = resultItem.Data.overall_star_rating.PrintIfAvailable();
        }
    }
}
