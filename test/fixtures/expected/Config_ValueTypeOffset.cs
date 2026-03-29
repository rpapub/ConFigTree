#nullable enable

using System;
using System.Data;
using System.Collections.Generic;

namespace Cpmf.Config
{
    /// <summary>Root configuration object.</summary>
    public class CodedConfig
    {
        public AssetsConfig Assets { get; set; } = new();
        public override string ToString() =>
            $"CodedConfig {{ Assets={Assets} }}";

        public IReadOnlyList<IOrchestratorAsset> GetAllAssets() =>
            new IOrchestratorAsset[] { Assets.QueueName, Assets.MaxItems, Assets.StrictMode, Assets.GenericValue };

        public static CodedConfig Load(Dictionary<string, DataTable> tables)
        {
            var cfg = new CodedConfig();
            if (tables.TryGetValue("Assets", out var t_Assets)) cfg.Assets = AssetsConfig.FromDataTable(t_Assets);
            return cfg;
        }
    }

    public class AssetsConfig
    {
        /// <summary>Queue name.</summary>
        public OrchestratorAsset<string> QueueName { get; set; } = new();
        /// <summary>Upper bound.</summary>
        public OrchestratorAsset<int> MaxItems { get; set; } = new();
        /// <summary>Enable strict.</summary>
        public OrchestratorAsset<bool> StrictMode { get; set; } = new();
        /// <summary>Untyped fallback.</summary>
        public OrchestratorAsset<object> GenericValue { get; set; } = new();

        public static AssetsConfig FromDataTable(DataTable dt)
        {
            var cfg = new AssetsConfig();
            foreach (DataRow row in dt.Rows)
            {
                var key   = row[0]?.ToString()?.Trim();
                var value = row[1]?.ToString()?.Trim() ?? "";
                switch (key)
                {
                    case "QueueName":
                        cfg.QueueName.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.QueueName.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "MaxItems":
                        cfg.MaxItems.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.MaxItems.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "StrictMode":
                        cfg.StrictMode.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.StrictMode.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "GenericValue":
                        cfg.GenericValue.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.GenericValue.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                }
            }
            return cfg;
        }

        public override string ToString() =>
            $"AssetsConfig {{ QueueName={QueueName}, MaxItems={MaxItems}, StrictMode={StrictMode}, GenericValue={GenericValue} }}";
    }

    public interface IOrchestratorAsset
    {
        string AssetName { get; }
        string Folder { get; }
        object? ValueAsObject { get; set; }
    }

    public class OrchestratorAsset<T> : IOrchestratorAsset
    {
        public string AssetName { get; set; } = "";
        public string Folder    { get; set; } = "";
        public T?     Value     { get; set; }
        object? IOrchestratorAsset.ValueAsObject
        {
            get => Value;
            set => Value = value is T v ? v : default;
        }
    }
}