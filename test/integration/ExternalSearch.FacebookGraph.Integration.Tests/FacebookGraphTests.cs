// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacebookGraphTests.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the facebook graph tests class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Messages.Processing;
using CluedIn.ExternalSearch.Providers.FacebookGraph;
using CluedIn.Testing.Base.Context;
using CluedIn.Testing.Base.ExternalSearch;
using Moq;
using Xunit;

namespace ExternalSearch.FacebookGraph.Integration.Tests
{
    public class FacebookGraphTests : BaseExternalSearchTest<FacebookGraphExternalSearchProvider>
    {
        [Fact(Skip = "Skipped until app access token is able to make more requests pr hour")]
        public void Test()
        {
            // App Token
            var dummyTokenProvider = new DummyTokenProvider("1563070893926610|a0VBRWRXOkl4qx84YrTuMfhBTMA");
            object[] parameters = { dummyTokenProvider };
            
            this.testContext = new TestContext();
            IEntityMetadata entityMetadata = new EntityMetadataPart()
                {
                    Name        = "Sitecore",
                    EntityType  = EntityType.Organization
                };

            this.Setup(parameters, entityMetadata);

            // Assert
            this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

            Assert.NotEmpty(this.clues);
            Assert.NotNull(this.clues.First().Decompress().Data.EntityData.PreviewImage);
        }
    }
}
