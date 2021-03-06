﻿using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using GameDataParser.Crypto.Common;
using GameDataParser.Parsers;
using Maple2Storage.Types.Metadata;

namespace GameDataParser.Files.Export
{
    public static class MapMetadataExport
    {
        public static void Export(List<PackFileEntry> files, MemoryMappedFile memFile)
        {
            // Parse and save some item data from xml file
            List<MapEntityMetadata> entities = MapEntityParser.Parse(memFile, files);
            MapEntityParser.Write(entities);
        }
    }
}
