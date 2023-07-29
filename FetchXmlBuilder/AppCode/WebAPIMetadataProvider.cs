﻿using MarkMpn.FetchXmlToWebAPI;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;

namespace Rappen.XTB.FetchXmlBuilder.AppCode
{
    internal class WebAPIMetadataProvider : IMetadataProvider
    {
        private FetchXmlBuilder fetchXmlBuilder;

        public WebAPIMetadataProvider(FetchXmlBuilder fetchXmlBuilder)
        {
            this.fetchXmlBuilder = fetchXmlBuilder;
        }

        public bool IsConnected => fetchXmlBuilder.Service != null;

        public EntityMetadata GetEntity(string logicalName)
        {
            if (fetchXmlBuilder.NeedToLoadEntity(logicalName))
            {
                fetchXmlBuilder.LoadEntityDetails(logicalName, null, false);
            }

            if (!(fetchXmlBuilder.GetEntity(logicalName) is EntityMetadata metadata))
            {
                throw new Exception($"No metadata for entity: {logicalName}");
            }

            return metadata;
        }

        public EntityMetadata GetEntity(int otc)
        {
            var metadata = fetchXmlBuilder.entities.SingleOrDefault(e => e.ObjectTypeCode == otc);

            if (metadata == null)
            {
                throw new Exception($"No metadata for entity: {otc}");
            }

            return metadata;
        }
    }
}