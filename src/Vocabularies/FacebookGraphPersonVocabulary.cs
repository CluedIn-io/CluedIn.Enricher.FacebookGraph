// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitOrganizationVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitOrganizationVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Vocabularies
{
    /// <summary>The clear bit organization vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FacebookGraphPersonVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookGraphPersonVocabulary"/> class.
        /// </summary>
        public FacebookGraphPersonVocabulary()
        {
            this.VocabularyName = "Facebook Graph Person";
            this.KeyPrefix = "facebookGraph.person";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Person;

            this.About             = this.Add(new VocabularyKey("about"));
            this.Bio               = this.Add(new VocabularyKey("bio"));
            this.Birthday          = this.Add(new VocabularyKey("birthday"));
            this.Category          = this.Add(new VocabularyKey("category"));
            this.CompanyOverview   = this.Add(new VocabularyKey("companyOverview"));
            this.Description       = this.Add(new VocabularyKey("description"));
            this.Emails            = this.Add(new VocabularyKey("emails"));
            this.Founded           = this.Add(new VocabularyKey("founded"));
            this.Mission           = this.Add(new VocabularyKey("mission"));
            this.Phone             = this.Add(new VocabularyKey("phone"));
            this.SingleLineAddress = this.Add(new VocabularyKey("singleLineAddress"));
            this.Website           = this.Add(new VocabularyKey("website"));
        }


        public VocabularyKey About { get; set; }

        public VocabularyKey Bio { get; set; }

        public VocabularyKey Birthday { get; set; }

        public VocabularyKey Category { get; set; }

        public VocabularyKey CompanyOverview { get; set; }

        public VocabularyKey Description { get; set; }

        public VocabularyKey Emails { get; set; }

        public VocabularyKey Founded { get; set; }

        public VocabularyKey Mission { get; set; }

        public VocabularyKey Phone { get; set; }

        public VocabularyKey SingleLineAddress { get; set; }

        public VocabularyKey Website { get; set; }
    }
}
