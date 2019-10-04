// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Vocabularies
{
    /// <summary>The clear bit vocabulary.</summary>
    public static class FacebookGraphVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="FacebookGraphVocabulary" /> class.
        /// </summary>
        static FacebookGraphVocabulary()
        {
            Person = new FacebookGraphPersonVocabulary();
            Organization = new FacebookGraphOrganizationVocabulary();
        }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        public static FacebookGraphPersonVocabulary Person { get; private set; }
        public static FacebookGraphOrganizationVocabulary Organization { get; private set; }
    }
}